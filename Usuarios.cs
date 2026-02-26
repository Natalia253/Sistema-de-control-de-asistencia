using CapaNegocio;
using CapaNegocio.Usuarios;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Formularios
{
    // Clase parcial para el formulario de gestión de usuarios
    public partial class Usuarios : Form
    {
        // Servicio para manejar la lógica de negocio relacionada con los usuarios
        private readonly UsuariosService _service = new UsuariosService();

        // Constructor del formulario de Usuarios
        public Usuarios()
        {
            InitializeComponent();
            DisenarDataGridView(); // Configura el diseño del DataGridView
            this.Shown += async (s, e) => await LlenarUsuarios(); // Carga los usuarios al mostrar el formulario
        }

        // Método para llenar el DataGridView con la lista de usuarios
        private async Task LlenarUsuarios()
        {
            DataTable dt = await _service.ListarUsuariosAsync(); // Obtiene la lista de usuarios
            dGVUsuarios.DataSource = dt; // Asigna los datos al DataGridView
            dGVUsuarios.ClearSelection(); // Limpia la selección inicial
        }

        // Evento que se ejecuta al hacer doble clic en una celda del DataGridView
        private void dGVUsuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Verifica que la fila sea válida

            var row = dGVUsuarios.Rows[e.RowIndex];

            // Llena los campos de texto con los datos de la fila seleccionada
            txtUserID.Text = row.Cells["UserID"].Value?.ToString() ?? "";
            txtNombre.Text = row.Cells["Nombre"].Value?.ToString() ?? "";
            txtDepartamento.Text = row.Cells["Departamento"].Value?.ToString() ?? "";
        }

        // Evento para guardar los cambios realizados en un usuario
        private async void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            btnGuardarCambios.Enabled = false; // Desactiva el botón mientras se guarda
            Cursor = Cursors.WaitCursor; // Cambia el cursor a "espera"

            try
            {
                // Actualiza el usuario con los datos ingresados
                await _service.ActualizarUsuarioAsync(
                    txtUserID.Text,
                    txtNombre.Text,
                    txtDepartamento.Text);

                MessageBox.Show("Usuario actualizado correctamente.", "OK"); // Muestra un mensaje de éxito
                await LlenarUsuarios(); // Recarga la lista de usuarios
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.Message, "Aviso"); // Muestra un mensaje de error
            }
            finally
            {
                btnGuardarCambios.Enabled = true; // Reactiva el botón
                Cursor = Cursors.Default; // Restaura el cursor
            }
        }

        // Configura el diseño del DataGridView
        private void DisenarDataGridView()
        {
            dGVUsuarios.AllowUserToAddRows = false; // Desactiva la opción de agregar filas
            dGVUsuarios.AllowUserToDeleteRows = false; // Desactiva la opción de eliminar filas
            dGVUsuarios.ReadOnly = true; // Hace que las celdas sean de solo lectura
            dGVUsuarios.MultiSelect = false; // Desactiva la selección múltiple
            dGVUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Selección por fila completa

            dGVUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Ajusta las columnas al ancho del control
            dGVUsuarios.RowTemplate.Height = 35; // Altura de las filas
            dGVUsuarios.BackgroundColor = Color.White; // Color de fondo
            dGVUsuarios.BorderStyle = BorderStyle.None; // Sin bordes
            dGVUsuarios.RowHeadersVisible = false; // Oculta los encabezados de fila

            dGVUsuarios.EnableHeadersVisualStyles = false; // Desactiva los estilos visuales predeterminados
            dGVUsuarios.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(12, 61, 92); // Color de fondo del encabezado
            dGVUsuarios.ColumnHeadersDefaultCellStyle.ForeColor = Color.White; // Color del texto del encabezado
            dGVUsuarios.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI Semibold", 10F, FontStyle.Bold); // Fuente del encabezado
        }
    }
}