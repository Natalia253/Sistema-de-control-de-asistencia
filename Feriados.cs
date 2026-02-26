using CapaNegocio.Feriados;
using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Formularios
{
    // Clase parcial para el formulario de Feriados
    public partial class Feriados : Form
    {
        // Servicio para manejar la lógica de negocio relacionada con los feriados
        private readonly FeriadosService _service = new FeriadosService();

        // Identificador del registro seleccionado en el grid
        private int? _idSeleccionado = null;

        // Indicadores de estado para evitar operaciones simultáneas
        private bool _cargandoSeleccion = false;
        private bool _cargandoUsuarios = false;

        // Bandera para detectar si el grid fue tocado por el usuario
        private bool _gridTocadoPorUsuario = false;

        // Constructor del formulario de Feriados
        public Feriados()
        {
            InitializeComponent();

            // Configuración de eventos para mover el formulario
            pnlTop.MouseDown += Top_MouseDown;
            lblTitulo.MouseDown += Top_MouseDown;

            // Configuración inicial de controles
            if (cboTipo.Items.Count > 0) cboTipo.SelectedIndex = 0;
            ToggleOtro();

            chkAplicaATodos.Checked = true;
            grpUsuarios.Enabled = false;

            // Eventos para manejar cambios en los controles
            cboTipo.SelectedIndexChanged += (s, e) => ToggleOtro();

            chkAplicaATodos.CheckedChanged += async (s, e) =>
            {
                if (_cargandoSeleccion) return;

                grpUsuarios.Enabled = !chkAplicaATodos.Checked;

                if (grpUsuarios.Enabled && clbUsuarios.Items.Count == 0)
                    await CargarUsuariosAsync();

                if (chkAplicaATodos.Checked)
                    ClearUsuariosSeleccionados();
            };

            // Configuración de eventos para los botones
            btnGuardar.Click += async (s, e) =>
            {
                try { await GuardarAsync(); }
                catch (Exception ex) { MostrarError(ex); }
            };

            btnEditar.Click += async (s, e) =>
            {
                try { await EditarAsync(); }
                catch (Exception ex) { MostrarError(ex); }
            };

            btnDesactivar.Click += async (s, e) =>
            {
                try { await DesactivarAsync(); }
                catch (Exception ex) { MostrarError(ex); }
            };

            btnLimpiar.Click += async (s, e) =>
            {
                LimpiarForm();
                await CargarGridAsync();
            };

            // Eventos para manejar interacciones con el grid
            dgvFeriados.CellMouseDown += (s, e) =>
            {
                if (e.RowIndex >= 0) _gridTocadoPorUsuario = true;
            };

            dgvFeriados.CellClick += async (s, e) =>
            {
                try { await CargarSeleccionEnFormAsync(); }
                catch (Exception ex) { MostrarError(ex); }
            };

            // Eventos al mostrar el formulario
            Shown += async (s, e) =>
            {
                await CargarUsuariosAsync();
                await CargarGridAsync();
            };
        }

        // Muestra un mensaje de error en un cuadro de diálogo
        private void MostrarError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Métodos para mover el formulario
        [DllImport("user32.dll")]
        private static extern void ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private void Top_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            ReleaseCapture();
            SendMessage(this.Handle, 0xA1, 0x2, 0);
        }

        // Clase interna para representar un usuario en la lista
        private sealed class UsuarioItem
        {
            public int UserID { get; set; }
            public string TextoVisible { get; set; }
        }

        // Carga los usuarios activos en el control de lista
        private async Task CargarUsuariosAsync()
        {
            if (_cargandoUsuarios) return;
            _cargandoUsuarios = true;

            try
            {
                DataTable dt = await _service.CargarUsuariosActivosAsync();

                clbUsuarios.Items.Clear();
                clbUsuarios.DisplayMember = "TextoVisible";
                clbUsuarios.ValueMember = "UserID";

                foreach (DataRow r in dt.Rows)
                {
                    int id = Convert.ToInt32(r["UserID"]);
                    string nombre = r["Nombre"]?.ToString() ?? "";
                    string dep = r["Departamento"]?.ToString() ?? "";

                    clbUsuarios.Items.Add(new UsuarioItem
                    {
                        UserID = id,
                        TextoVisible = $"{id} - {nombre} ({dep})"
                    });
                }

                if (clbUsuarios.Items.Count == 0)
                {
                    MessageBox.Show(
                        "No se cargaron usuarios.\n\n" +
                        "Revisa:\n" +
                        "1) Que la tabla Usuarios tenga registros.\n" +
                        "2) Que Activo = 1.\n" +
                        "3) Que estés conectado a la BD correcta.",
                        "Usuarios",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            finally
            {
                _cargandoUsuarios = false;
            }
        }

        // Obtiene los IDs de los usuarios seleccionados en la lista
        private int[] GetUsuariosSeleccionados()
        {
            var list = new System.Collections.Generic.List<int>();

            foreach (var item in clbUsuarios.CheckedItems)
                if (item is UsuarioItem u)
                    list.Add(u.UserID);

            return list.ToArray();
        }

        // Limpia la selección de usuarios en la lista
        private void ClearUsuariosSeleccionados()
        {
            for (int i = 0; i < clbUsuarios.Items.Count; i++)
                clbUsuarios.SetItemChecked(i, false);
        }

        // Carga los datos en el grid
        private async Task CargarGridAsync()
        {
            _cargandoSeleccion = true;

            try
            {
                dgvFeriados.DataSource = await _service.CargarGridAsync();

                if (dgvFeriados.Columns.Contains("Id"))
                    dgvFeriados.Columns["Id"].Visible = false;

                if (dgvFeriados.Columns.Contains("Fecha"))
                    dgvFeriados.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy";

                dgvFeriados.ClearSelection();
                _idSeleccionado = null;

                _gridTocadoPorUsuario = false;
                LimpiarForm();
            }
            finally
            {
                _cargandoSeleccion = false;
            }
        }

        // Carga la selección actual del grid en el formulario
        private async Task CargarSeleccionEnFormAsync()
        {
            if (_cargandoSeleccion) return;
            if (!_gridTocadoPorUsuario) return;
            if (dgvFeriados.CurrentRow == null) return;

            _cargandoSeleccion = true;
            try
            {
                _idSeleccionado = Convert.ToInt32(dgvFeriados.CurrentRow.Cells["Id"].Value);

                dtpFecha.Value = Convert.ToDateTime(dgvFeriados.CurrentRow.Cells["Fecha"].Value);

                string tipo = dgvFeriados.CurrentRow.Cells["Tipo"].Value?.ToString() ?? "FERIADO_NACIONAL";
                cboTipo.SelectedItem = tipo;

                txtTituloOtro.Text = dgvFeriados.CurrentRow.Cells["TituloOtro"].Value?.ToString() ?? "";
                txtDescripcion.Text = dgvFeriados.CurrentRow.Cells["Descripcion"].Value?.ToString() ?? "";

                ToggleOtro();

                bool aplicaATodos = Convert.ToInt32(dgvFeriados.CurrentRow.Cells["AplicaATodos"].Value) == 1;
                chkAplicaATodos.Checked = aplicaATodos;
                grpUsuarios.Enabled = !aplicaATodos;

                ClearUsuariosSeleccionados();

                if (!aplicaATodos)
                {
                    if (clbUsuarios.Items.Count == 0)
                        await CargarUsuariosAsync();

                    await MarcarUsuariosAsync(_idSeleccionado.Value);
                }
            }
            finally
            {
                _cargandoSeleccion = false;
            }
        }

        // Marca los usuarios asociados a un feriado en la lista
        private async Task MarcarUsuariosAsync(int feriadoId)
        {
            DataTable dt = await _service.UsuariosPorFeriadoAsync(feriadoId);
            var set = FeriadosService.DataTableToSet(dt, "UserID");

            for (int i = 0; i < clbUsuarios.Items.Count; i++)
            {
                if (clbUsuarios.Items[i] is UsuarioItem u)
                    clbUsuarios.SetItemChecked(i, set.Contains(u.UserID));
            }
        }

        // Guarda un nuevo registro de feriado
        private async Task GuardarAsync()
        {
            await _service.GuardarAsync(
                dtpFecha.Value.Date,
                cboTipo.Text,
                txtTituloOtro.Text,
                txtDescripcion.Text,
                chkAplicaATodos.Checked,
                GetUsuariosSeleccionados());

            LimpiarForm();
            await CargarGridAsync();
            MessageBox.Show("Guardado correctamente.");
        }

        // Edita el registro seleccionado
        private async Task EditarAsync()
        {
            if (_idSeleccionado == null)
            {
                MessageBox.Show("Selecciona un registro para editar.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await _service.EditarAsync(
                _idSeleccionado.Value,
                dtpFecha.Value.Date,
                cboTipo.Text,
                txtTituloOtro.Text,
                txtDescripcion.Text,
                chkAplicaATodos.Checked,
                GetUsuariosSeleccionados());

            LimpiarForm();
            await CargarGridAsync();
            MessageBox.Show("Editado correctamente.");
        }

        // Desactiva el registro seleccionado
        private async Task DesactivarAsync()
        {
            if (_idSeleccionado == null)
            {
                MessageBox.Show("Selecciona un registro para desactivar.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("¿Estás seguro de desactivar este registro?",
                "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            await _service.DesactivarAsync(_idSeleccionado.Value);

            LimpiarForm();
            await CargarGridAsync();
            MessageBox.Show("Desactivado correctamente.");
        }

        // Alterna la visibilidad de los controles relacionados con el tipo "OTRO"
        private void ToggleOtro()
        {
            string tipo = cboTipo.SelectedItem?.ToString() ?? "";
            bool esOtro = tipo == "OTRO";

            lblTituloOtro.Visible = esOtro;
            txtTituloOtro.Visible = esOtro;
            txtTituloOtro.Enabled = esOtro;

            if (!esOtro)
                txtTituloOtro.Clear();

            txtDescripcion.Enabled = true;
        }

        // Limpia los controles del formulario
        private void LimpiarForm()
        {
            _idSeleccionado = null;
            dtpFecha.Value = DateTime.Today;
            txtDescripcion.Clear();
            txtTituloOtro.Clear();

            if (cboTipo.Items.Count > 0) cboTipo.SelectedIndex = 0;
            ToggleOtro();

            chkAplicaATodos.Checked = true;
            grpUsuarios.Enabled = false;
            ClearUsuariosSeleccionados();
        }
    }
}