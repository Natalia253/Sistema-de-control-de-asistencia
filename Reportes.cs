using CapaNegocio.Reportes;
using ClosedXML.Excel;
using System;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Formularios
{
    // Formulario para gestionar reportes
    public partial class Reportes : Form
    {
        // Lógica de negocio para reportes
        private readonly ReportesBL _bl = new ReportesBL();

        // Configuración de límites para filas
        private const bool USAR_LIMIT = false;
        private const int LIMIT_FILAS = 500;

        // Manejo de cancelación y temporizadores
        private CancellationTokenSource _cts;
        private System.Windows.Forms.Timer _debounceTimer;
        private System.Windows.Forms.Timer _autoRefreshTimer;

        // Cache para cambios en la base de datos
        private DateTime? _lastMaxFecha = null;
        private long? _lastCount = null;

        // Bandera para columnas configuradas
        private bool _columnasConfiguradas = false;

        // Lock para evitar cargas simultáneas
        private readonly SemaphoreSlim _loadLock = new SemaphoreSlim(1, 1);

        // Constructor del formulario
        public Reportes()
        {
            InitializeComponent();

            DiseñarGrid(); // Configura diseño del grid
            InicializarMeses(); // Llena combo box de meses
            InicializarFechas(); // Configura fechas iniciales

            // Temporizador para debounce
            _debounceTimer = new System.Windows.Forms.Timer();
            _debounceTimer.Interval = 300;
            _debounceTimer.Tick += async (s, e) =>
            {
                _debounceTimer.Stop();
                await CargarAsync();
            };

            // Eventos para filtros
            txtBuscar.TextChanged += (s, e) => ProgramarCargar();
            txtUserID.TextChanged += (s, e) => ProgramarCargar();
            txtAnio.TextChanged += (s, e) => ProgramarCargar();
            dtpDesde.ValueChanged += (s, e) => ProgramarCargar();
            dtpHasta.ValueChanged += (s, e) => ProgramarCargar();

            cboMes.SelectedIndexChanged += (s, e) =>
            {
                if (!IsHandleCreated) return;
                ProgramarCargar();
            };

            // Eventos para botones
            btnBuscar.Click += async (s, e) => await CargarAsync();
            btnLimpiar.Click += async (s, e) =>
            {
                InicializarFechas();
                await CargarAsync();
            };

            InicializarAutoRefresh(); // Configura auto-refresh

            Shown += async (s, e) => await CargarAsync(); // Carga inicial
        }

        // Programa la carga de datos con debounce
        private void ProgramarCargar()
        {
            if (!IsHandleCreated) return;
            _debounceTimer.Stop();
            _debounceTimer.Start();
        }

        // Devuelve el primer día del mes actual
        private static DateTime PrimerDiaMesActual()
            => new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

        // Configura fechas iniciales en los controles
        private void InicializarFechas()
        {
            dtpDesde.MaxDate = DateTime.Today;
            dtpHasta.MaxDate = DateTime.Today;

            dtpDesde.Value = PrimerDiaMesActual();
            dtpHasta.Value = DateTime.Today;

            txtBuscar.Clear();
            txtUserID.Clear();
            txtAnio.Clear();

            if (cboMes.Items.Count > 0) cboMes.SelectedIndex = 0;
        }

        // Llena el combo box con los meses
        private void InicializarMeses()
        {
            cboMes.Items.Clear();
            cboMes.Items.Add(new ComboItem("Todos", 0));
            cboMes.Items.Add(new ComboItem("Enero", 1));
            cboMes.Items.Add(new ComboItem("Febrero", 2));
            cboMes.Items.Add(new ComboItem("Marzo", 3));
            cboMes.Items.Add(new ComboItem("Abril", 4));
            cboMes.Items.Add(new ComboItem("Mayo", 5));
            cboMes.Items.Add(new ComboItem("Junio", 6));
            cboMes.Items.Add(new ComboItem("Julio", 7));
            cboMes.Items.Add(new ComboItem("Agosto", 8));
            cboMes.Items.Add(new ComboItem("Septiembre", 9));
            cboMes.Items.Add(new ComboItem("Octubre", 10));
            cboMes.Items.Add(new ComboItem("Noviembre", 11));
            cboMes.Items.Add(new ComboItem("Diciembre", 12));
            cboMes.SelectedIndex = 0;
        }

        // Configura el temporizador para auto-refresh
        private void InicializarAutoRefresh()
        {
            _autoRefreshTimer = new System.Windows.Forms.Timer();
            _autoRefreshTimer.Interval = 10000; // Intervalo en ms

            _autoRefreshTimer.Tick += async (s, e) =>
            {
                if (!IsHandleCreated) return;

                // Evita cargas simultáneas
                if (_loadLock.CurrentCount == 0) return;

                try
                {
                    // Recarga solo si hubo cambios
                    bool huboCambios = await HuboCambiosEnBDAsync();
                    if (!huboCambios) return;

                    await CargarAsync();
                }
                catch
                {
                    // Manejo de errores silencioso
                }
            };

            _autoRefreshTimer.Start();
        }

        // Verifica si hubo cambios en la base de datos
        private async Task<bool> HuboCambiosEnBDAsync()
        {
            int mesValue = 0;
            if (cboMes.SelectedItem is ComboItem m) mesValue = m.Value;

            var estado = await _bl.ObtenerEstadoCambiosAsync(
                dtpDesde.Value.Date,
                dtpHasta.Value.Date,
                txtAnio.Text,
                mesValue,
                _lastMaxFecha,
                _lastCount,
                CancellationToken.None);

            // Actualiza cache con el estado
            _lastMaxFecha = estado.MaxFecha;
            _lastCount = estado.Total;

            return estado.HuboCambios;
        }

        // Carga los datos en el grid
        private async Task CargarAsync()
        {
            if (!IsHandleCreated) return;

            // Evita cargas simultáneas
            if (!await _loadLock.WaitAsync(0)) return;

            try
            {
                Cursor = Cursors.WaitCursor;

                btnBuscar.Enabled = false;
                btnLimpiar.Enabled = false;
                btnExportar.Enabled = false;

                // Nuevo token para la carga
                _cts?.Dispose();
                _cts = new CancellationTokenSource();
                var token = _cts.Token;

                int mesValue = 0;
                if (cboMes.SelectedItem is ComboItem m) mesValue = m.Value;

                DataTable dt = await _bl.ObtenerReporteAsync(
                    dtpDesde.Value.Date,
                    dtpHasta.Value.Date,
                    txtBuscar.Text,
                    txtUserID.Text,
                    txtAnio.Text,
                    mesValue,
                    USAR_LIMIT,
                    LIMIT_FILAS,
                    token);

                token.ThrowIfCancellationRequested();

                dGVReportes.DataSource = dt;

                // Actualiza cache para auto-refresh
                _lastCount = dt.Rows.Count;

                if (dt.Columns.Contains("Fecha") && dt.Rows.Count > 0)
                {
                    DateTime maxFecha = new DateTime(1900, 1, 1);
                    foreach (DataRow r in dt.Rows)
                    {
                        if (r["Fecha"] != DBNull.Value)
                        {
                            DateTime f = Convert.ToDateTime(r["Fecha"]);
                            if (f > maxFecha) maxFecha = f;
                        }
                    }
                    _lastMaxFecha = maxFecha;
                }
                else
                {
                    _lastMaxFecha = null;
                }

                if (!_columnasConfiguradas)
                {
                    AjustarProporcionesColumnas();
                    _columnasConfiguradas = true;
                    dGVReportes.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                }

                if (dGVReportes.Columns.Contains("Fecha"))
                    dGVReportes.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy";

                if (dGVReportes.Columns.Contains("EntradaRaw"))
                    dGVReportes.Columns["EntradaRaw"].DefaultCellStyle.Format = @"hh\:mm";

                if (dGVReportes.Columns.Contains("SalidaRaw"))
                    dGVReportes.Columns["SalidaRaw"].DefaultCellStyle.Format = @"hh\:mm";

                dGVReportes.ClearSelection();
                dGVReportes.Refresh();
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando reportes:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                btnBuscar.Enabled = true;
                btnLimpiar.Enabled = true;
                btnExportar.Enabled = true;

                _loadLock.Release();
            }
        }

        // Configura diseño del grid
        private void DiseñarGrid()
        {
            dGVReportes.ReadOnly = true;
            dGVReportes.MultiSelect = false;
            dGVReportes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dGVReportes.RowHeadersVisible = false;

            dGVReportes.ScrollBars = ScrollBars.Vertical;
            dGVReportes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dGVReportes.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dGVReportes.RowTemplate.Height = 40;
            dGVReportes.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dGVReportes.DefaultCellStyle.Padding = new Padding(6);

            dGVReportes.BackgroundColor = Color.White;
            dGVReportes.BorderStyle = BorderStyle.None;
            dGVReportes.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            dGVReportes.AllowUserToResizeColumns = false;
            dGVReportes.AllowUserToResizeRows = false;
            dGVReportes.AllowUserToOrderColumns = true;

            dGVReportes.RowPrePaint -= dGVReportes_RowPrePaint;
            dGVReportes.RowPrePaint += dGVReportes_RowPrePaint;
        }

        // Ajusta colores de filas según estado
        private void dGVReportes_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var row = dGVReportes.Rows[e.RowIndex];
            if (row.IsNewRow) return;

            string estado = dGVReportes.Columns.Contains("Estado")
                ? Convert.ToString(row.Cells["Estado"].Value) ?? ""
                : "";

            string tipo = dGVReportes.Columns.Contains("Tipo")
                ? Convert.ToString(row.Cells["Tipo"].Value) ?? ""
                : "";

            Color back = Color.White;
            Color fore = Color.Black;

            // Colores según el estado
            if (estado.Equals("FERIADO", StringComparison.OrdinalIgnoreCase) ||
                estado.Equals("FERIADO_NACIONAL", StringComparison.OrdinalIgnoreCase) ||
                estado.Equals("FERIADO_REGIONAL", StringComparison.OrdinalIgnoreCase) ||
                estado.Equals("FERIADO_LOCAL", StringComparison.OrdinalIgnoreCase) ||
                estado.Equals("EVENTO", StringComparison.OrdinalIgnoreCase) ||
                estado.Equals("CIERRE", StringComparison.OrdinalIgnoreCase) ||
                estado.Equals("OTRO", StringComparison.OrdinalIgnoreCase))
            {
                back = Color.White;
                fore = Color.Black;
            }
            else if (estado == "AUSENTE")
            {
                back = Color.FromArgb(220, 53, 69);
                fore = Color.White;
            }
            else if (estado == "INCOMPLETO")
            {
                back = Color.FromArgb(255, 243, 205);
            }
            else if (estado == "TARDE")
            {
                back = Color.FromArgb(255, 193, 7);
            }
            else
            {
                bool faltaEntrada = dGVReportes.Columns.Contains("EntradaRaw") &&
                                    (row.Cells["EntradaRaw"].Value == DBNull.Value
                                     || string.IsNullOrWhiteSpace(row.Cells["EntradaRaw"].Value?.ToString()));

                bool faltaSalida = dGVReportes.Columns.Contains("SalidaRaw") &&
                                   (row.Cells["SalidaRaw"].Value == DBNull.Value
                                    || string.IsNullOrWhiteSpace(row.Cells["SalidaRaw"].Value?.ToString()));

                if (faltaEntrada || faltaSalida)
                    back = Color.FromArgb(209, 236, 241);
            }

            row.DefaultCellStyle.BackColor = back;
            row.DefaultCellStyle.ForeColor = fore;
        }

        // Ajusta proporciones de columnas del grid
        private void AjustarProporcionesColumnas()
        {
            if (dGVReportes.Columns.Count == 0) return;

            foreach (DataGridViewColumn col in dGVReportes.Columns)
                col.MinimumWidth = 60;

            if (dGVReportes.Columns.Contains("UserID"))
                dGVReportes.Columns["UserID"].FillWeight = 40;

            if (dGVReportes.Columns.Contains("Nombre"))
                dGVReportes.Columns["Nombre"].FillWeight = 120;

            if (dGVReportes.Columns.Contains("Departamento"))
                dGVReportes.Columns["Departamento"].FillWeight = 110;

            if (dGVReportes.Columns.Contains("Fecha"))
                dGVReportes.Columns["Fecha"].FillWeight = 70;

            if (dGVReportes.Columns.Contains("EntradaRaw"))
                dGVReportes.Columns["EntradaRaw"].FillWeight = 60;

            if (dGVReportes.Columns.Contains("SalidaRaw"))
                dGVReportes.Columns["SalidaRaw"].FillWeight = 60;

            if (dGVReportes.Columns.Contains("Estado"))
                dGVReportes.Columns["Estado"].FillWeight = 80;

            if (dGVReportes.Columns.Contains("MinutosTarde"))
                dGVReportes.Columns["MinutosTarde"].FillWeight = 60;

            if (dGVReportes.Columns.Contains("Tipo"))
                dGVReportes.Columns["Tipo"].FillWeight = 80;

            if (dGVReportes.Columns.Contains("Justificacion"))
                dGVReportes.Columns["Justificacion"].FillWeight = 150;

            if (dGVReportes.Columns.Contains("CodigoDocumento"))
            {
                var col = dGVReportes.Columns["CodigoDocumento"];
                col.HeaderText = "CódigoDoc";
                col.FillWeight = 90;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dGVReportes.Columns.Contains("HorasTrabajadas"))
            {
                var col = dGVReportes.Columns["HorasTrabajadas"];
                col.HeaderText = "Horas Trabajadas";
                col.FillWeight = 75;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        // Exporta datos del grid a Excel
        private void btnExportar_Click(object sender, EventArgs e)
        {
            ExportarExcelClosedXml();
        }

        private void ExportarExcelClosedXml()
        {
            if (dGVReportes.Rows.Count == 0 || dGVReportes.DataSource == null)
            {
                MessageBox.Show("No hay datos para exportar.", "Exportar",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!(dGVReportes.DataSource is DataTable dt) || dt.Rows.Count == 0)
            {
                MessageBox.Show("La fuente de datos no es un DataTable o está vacía.", "Exportar",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataView dv = dt.DefaultView;
            if (dt.Columns.Contains("UserID") && dt.Columns.Contains("Fecha"))
                dv.Sort = "UserID ASC, Fecha ASC";
            DataTable dtOrdenado = dv.ToTable();

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Excel (*.xlsx)|*.xlsx",
                FileName = $"Reporte_{DateTime.Now:yyyyMMdd_HHmm}.xlsx"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                using (var wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add("Reporte");

                    int totalCols = dtOrdenado.Columns.Count;
                    int row = 1;

                    string rangoTexto = ConstruirRangoTexto();

                    // Títulos
                    CrearTitulo(ws, row++, totalCols, "República de Panamá", 14, true);
                    CrearTitulo(ws, row++, totalCols, "Programa de Inversión de Obras Públicas y Servicios Municipales", 12, true);
                    CrearTitulo(ws, row++, totalCols, "Reporte de Asistencias", 12, true);
                    CrearTitulo(ws, row++, totalCols, $"Rango: {rangoTexto}", 11, false);

                    row++;

                    int startRow = row; // Fila encabezados
                    int startCol = 1;

                    // Encabezados
                    for (int c = 0; c < dtOrdenado.Columns.Count; c++)
                        ws.Cell(startRow, startCol + c).SetValue(dtOrdenado.Columns[c].ColumnName);

                    var headerRange = ws.Range(startRow, startCol, startRow, startCol + totalCols - 1);

                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Font.FontColor = XLColor.White;
                    headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#0C3D5C");
                    headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    headerRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    headerRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                    // Data
                    for (int r = 0; r < dtOrdenado.Rows.Count; r++)
                    {
                        for (int c = 0; c < dtOrdenado.Columns.Count; c++)
                        {
                            var cell = ws.Cell(startRow + 1 + r, startCol + c);
                            object val = dtOrdenado.Rows[r][c];

                            if (dtOrdenado.Columns[c].ColumnName.Equals("Fecha", StringComparison.OrdinalIgnoreCase)
                                && val != null && val != DBNull.Value
                                && DateTime.TryParse(val.ToString(), out DateTime fecha))
                            {
                                cell.Value = fecha.Date;
                            }
                            else
                            {
                                cell.Value = val?.ToString() ?? "";
                            }
                        }
                    }

                    int lastRow = startRow + dtOrdenado.Rows.Count;
                    int lastCol = startCol + totalCols - 1;

                    if (dtOrdenado.Columns.Contains("Fecha"))
                    {
                        int colFecha = dtOrdenado.Columns["Fecha"].Ordinal + 1;
                        ws.Column(colFecha).Style.DateFormat.Format = "dd/MM/yyyy";
                        ws.Column(colFecha).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    }

                    var allRange = ws.Range(startRow, startCol, lastRow, lastCol);
                    allRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    allRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                    ws.Columns().AdjustToContents();

                    // Configuración para imprimir
                    ws.PageSetup.PageOrientation = XLPageOrientation.Landscape;
                    ws.PageSetup.FitToPages(1, 0);
                    ws.PageSetup.CenterHorizontally = true;

                    ws.PageSetup.Margins.Top = 0.5;
                    ws.PageSetup.Margins.Bottom = 0.5;
                    ws.PageSetup.Margins.Left = 0.3;
                    ws.PageSetup.Margins.Right = 0.3;

                    ws.PageSetup.SetRowsToRepeatAtTop(startRow, startRow);

                    ws.PageSetup.PrintAreas.Clear();
                    var printRange = ws.Range(1, 1, lastRow, lastCol);
                    ws.PageSetup.PrintAreas.Add(printRange.RangeAddress.ToString());

                    wb.SaveAs(sfd.FileName);
                }

                MessageBox.Show("Reporte exportado correctamente.", "Exportar",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar:\n" + ex.Message, "Exportar",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Crea un título en la hoja de Excel
        private void CrearTitulo(IXLWorksheet ws, int fila, int totalCols, string texto, int fontSize, bool bold)
        {
            var rng = ws.Range(fila, 1, fila, totalCols);
            rng.Merge();
            rng.Value = texto;
            rng.Style.Font.FontSize = fontSize;
            rng.Style.Font.Bold = bold;
            rng.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rng.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        }

        // Construye el texto del rango de fechas para el reporte
        private string ConstruirRangoTexto()
        {
            bool anioValido = int.TryParse((txtAnio.Text ?? "").Trim(), out int anio)
                              && anio >= 1900
                              && anio <= DateTime.Today.Year;

            int mes = 0;
            if (cboMes.SelectedItem is ComboItem m) mes = m.Value;

            if (anioValido && mes >= 1 && mes <= 12)
                return $"{((ComboItem)cboMes.SelectedItem).Text} {anio}";

            if (anioValido && mes == 0)
                return $"Año {anio}";

            DateTime desde = dtpDesde.Value.Date;
            DateTime hasta = dtpHasta.Value.Date;

            if (desde > DateTime.Today) desde = DateTime.Today;
            if (hasta > DateTime.Today) hasta = DateTime.Today;

            return $"{desde:dd/MM/yyyy} - {hasta:dd/MM/yyyy}";
        }

        // Maneja el cierre del formulario
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _autoRefreshTimer?.Stop();
            _autoRefreshTimer?.Dispose();

            _debounceTimer?.Stop();
            _debounceTimer?.Dispose();

            _cts?.Cancel();
            _cts?.Dispose();

            _loadLock?.Dispose();

            base.OnFormClosing(e);
        }
    }
}