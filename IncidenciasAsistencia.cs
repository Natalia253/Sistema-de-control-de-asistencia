using CapaNegocio.Incidencia;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Formularios
{
    // Clase parcial para el formulario de Incidencias de Asistencia
    public partial class IncidenciasAsistencia : Form
    {
        // Servicio para manejar la lógica de negocio relacionada con las incidencias
        private readonly IncidenciasService _service = new IncidenciasService();

        // Constructor del formulario de Incidencias de Asistencia
        public IncidenciasAsistencia()
        {
            InitializeComponent();

            // Configuración inicial de los controles
            cboTipoJust.Items.Clear();
            cboTipoJust.Items.Add("MÉDICA");
            cboTipoJust.Items.Add("PERSONAL");
            cboTipoJust.SelectedIndex = -1;

            // Validación para permitir solo números en el filtro de UserID
            txtUserIdFiltro.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                    e.Handled = true;
            };

            // Configuración de eventos para los botones
            btnBuscar.Click += async (s, e) => await CargarPendientesAsync();
            btnLimpiar.Click += async (s, e) =>
            {
                InicializarFiltros();
                await CargarPendientesAsync();
            };

            // Configuración de eventos para los combos y el grid
            cboTipo.SelectedIndexChanged += (s, e) => AjustarCampos();
            cboTipoJust.SelectedIndexChanged += (s, e) => AjustarCampos();
            dGVIncidencias.SelectionChanged += (s, e) => AjustarTipoSegunEstadoSeleccionado();

            // Configuración del botón Guardar
            btnGuardar.Click += async (s, e) =>
            {
                try
                {
                    await GuardarIncidenciaAsync();
                    await CargarPendientesAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            // Diseño del grid y configuración inicial
            DisenarGrid();
            InicializarFiltros();

            // Carga inicial de datos al mostrar el formulario
            Shown += async (s, e) => await CargarPendientesAsync();
        }

        // Inicializa los filtros del formulario
        private void InicializarFiltros()
        {
            dtpDesde.Value = IncidenciasService.PrimerDiaMesActual();
            dtpHasta.Value = DateTime.Today;

            txtBuscar.Clear();
            txtUserIdFiltro.Clear();

            txtCodigoAuditoria.Clear();
            txtJustificacion.Clear();

            cboTipo.SelectedIndex = -1;
            cboTipoJust.SelectedIndex = -1;

            AjustarCampos();
        }

        // Carga las incidencias pendientes en el grid
        private async Task CargarPendientesAsync()
        {
            DateTime desde = dtpDesde.Value.Date;
            DateTime hasta = dtpHasta.Value.Date;

            if (hasta < desde)
            {
                MessageBox.Show("La fecha 'Hasta' no puede ser menor que 'Desde'.", "Fechas",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int? userId = null;
            try { userId = IncidenciasService.ParseUserId(txtUserIdFiltro.Text); }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Filtro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable dt = await _service.CargarPendientesAsync(
                desde, hasta,
                txtBuscar.Text,
                userId);

            dGVIncidencias.DataSource = dt;

            if (dGVIncidencias.Columns.Contains("Fecha"))
                dGVIncidencias.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy";

            // Oculta columnas innecesarias para el usuario
            if (dGVIncidencias.Columns.Contains("EntradaRaw"))
                dGVIncidencias.Columns["EntradaRaw"].Visible = false;
            if (dGVIncidencias.Columns.Contains("SalidaRaw"))
                dGVIncidencias.Columns["SalidaRaw"].Visible = false;

            PintarEstados();

            // Selecciona la primera fila si hay datos
            if (dGVIncidencias.Rows.Count > 0)
            {
                try
                {
                    dGVIncidencias.ClearSelection();
                    dGVIncidencias.Rows[0].Selected = true;
                    dGVIncidencias.CurrentCell = dGVIncidencias.Rows[0].Cells[0];
                }
                catch { }
            }

            AjustarTipoSegunEstadoSeleccionado();
        }

        // Configura el diseño del grid
        private void DisenarGrid()
        {
            dGVIncidencias.AllowUserToAddRows = false;
            dGVIncidencias.AllowUserToDeleteRows = false;
            dGVIncidencias.ReadOnly = true;
            dGVIncidencias.MultiSelect = false;
            dGVIncidencias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dGVIncidencias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dGVIncidencias.RowHeadersVisible = false;
            dGVIncidencias.BackgroundColor = Color.White;
            dGVIncidencias.BorderStyle = BorderStyle.None;

            dGVIncidencias.EnableHeadersVisualStyles = false;
            dGVIncidencias.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dGVIncidencias.ColumnHeadersHeight = 40;

            dGVIncidencias.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(12, 61, 92);
            dGVIncidencias.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dGVIncidencias.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            dGVIncidencias.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            dGVIncidencias.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 235, 255);
            dGVIncidencias.DefaultCellStyle.SelectionForeColor = Color.Black;

            dGVIncidencias.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dGVIncidencias.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        }

        // Cambia el color de las filas según el estado de la incidencia
        private void PintarEstados()
        {
            foreach (DataGridViewRow r in dGVIncidencias.Rows)
            {
                if (r.IsNewRow) continue;

                string estado = Convert.ToString(r.Cells["Estado"].Value) ?? "";

                if (estado == "AUSENTE")
                    r.DefaultCellStyle.BackColor = Color.MistyRose;
                else if (estado == "TARDE")
                    r.DefaultCellStyle.BackColor = Color.LightYellow;
                else if (estado == "INCOMPLETO")
                    r.DefaultCellStyle.BackColor = Color.FromArgb(255, 242, 230);
                else if (estado == "SALIDA_TEMPRANA")
                    r.DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 230);
                else if (r.Index % 2 == 1)
                    r.DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            }
        }

        // Guarda una nueva incidencia
        private async Task GuardarIncidenciaAsync()
        {
            if (dGVIncidencias.CurrentRow == null)
                throw new Exception("Selecciona una fila antes de guardar la incidencia.");

            int userId = Convert.ToInt32(dGVIncidencias.CurrentRow.Cells["UserID"].Value);
            DateTime fecha = Convert.ToDateTime(dGVIncidencias.CurrentRow.Cells["Fecha"].Value).Date;

            string tipo = cboTipo.SelectedItem?.ToString();
            string justificacion = txtJustificacion.Text ?? "";
            string tipoJust = cboTipoJust.SelectedItem?.ToString();
            string codigo = txtCodigoAuditoria.Text ?? "";

            await _service.GuardarIncidenciaAsync(userId, fecha, tipo, justificacion, tipoJust, codigo);

            txtCodigoAuditoria.Clear();
            txtJustificacion.Clear();
            cboTipoJust.SelectedIndex = -1;

            MessageBox.Show("Incidencia guardada correctamente.", "OK",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Ajusta los campos según el estado seleccionado en el grid
        private void AjustarTipoSegunEstadoSeleccionado()
        {
            if (dGVIncidencias.CurrentRow == null || !dGVIncidencias.Columns.Contains("Estado"))
                return;

            string estado = Convert.ToString(dGVIncidencias.CurrentRow.Cells["Estado"].Value) ?? "";

            cboTipo.Items.Clear();

            if (estado == "INCOMPLETO")
            {
                bool tieneEntrada = true;
                bool tieneSalida = true;

                if (dGVIncidencias.Columns.Contains("EntradaRaw"))
                {
                    var vEnt = dGVIncidencias.CurrentRow.Cells["EntradaRaw"].Value;
                    tieneEntrada = !(vEnt == null || vEnt == DBNull.Value || string.IsNullOrWhiteSpace(vEnt.ToString()));
                }

                if (dGVIncidencias.Columns.Contains("SalidaRaw"))
                {
                    var vSal = dGVIncidencias.CurrentRow.Cells["SalidaRaw"].Value;
                    tieneSalida = !(vSal == null || vSal == DBNull.Value || string.IsNullOrWhiteSpace(vSal.ToString()));
                }

                if (!tieneEntrada && tieneSalida)
                    cboTipo.Items.Add("FALTA_ENTRADA");
                else if (tieneEntrada && !tieneSalida)
                    cboTipo.Items.Add("FALTA_SALIDA");
                else
                    cboTipo.Items.Add("AUSENCIA");

                cboTipo.SelectedIndex = 0;
                AjustarCampos();
                return;
            }
            else if (estado == "AUSENTE")
                cboTipo.Items.Add("AUSENCIA");
            else if (estado == "TARDE")
                cboTipo.Items.Add("TARDANZA");
            else if (estado == "SALIDA_TEMPRANA")
                cboTipo.Items.Add("SALIDA_TEMPRANA");
            else
            {
                cboTipo.Items.Add("AUSENCIA");
                cboTipo.Items.Add("FALTA_ENTRADA");
                cboTipo.Items.Add("FALTA_SALIDA");
                cboTipo.Items.Add("TARDANZA");
                cboTipo.Items.Add("SALIDA_TEMPRANA");
            }

            cboTipo.SelectedIndex = (cboTipo.Items.Count > 0) ? 0 : -1;
            AjustarCampos();
        }

        // Ajusta los campos del formulario según el tipo de incidencia seleccionado
        private void AjustarCampos()
        {
            string tipoInc = cboTipo.SelectedItem?.ToString();
            bool esAusencia = (tipoInc == "AUSENCIA");

            cboTipoJust.Enabled = esAusencia;
            if (!esAusencia) cboTipoJust.SelectedIndex = -1;

            txtCodigoAuditoria.Enabled = esAusencia;
            lblCodigoAuditoria.Enabled = esAusencia;
            if (!esAusencia) txtCodigoAuditoria.Clear();

            lblJustificacion.Text = esAusencia ? "Justificación de ausencia:" : "Justificación / Motivo:";

            if (esAusencia)
            {
                string tipoJust = cboTipoJust.SelectedItem?.ToString();

                if (tipoJust == "MÉDICA")
                    lblCodigoAuditoria.Text = "Código de incapacidad (*):";
                else if (tipoJust == "PERSONAL")
                    lblCodigoAuditoria.Text = "Código de auditoría (*):";
                else
                    lblCodigoAuditoria.Text = "Código (*):";
            }
            else
            {
                lblCodigoAuditoria.Text = "Código (*):";
            }
        }
    }
}