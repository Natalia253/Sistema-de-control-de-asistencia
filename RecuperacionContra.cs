using CapaNegocio.Login;
using System;
using System.Windows.Forms;

namespace CapaPresentacion
{
    // Clase para el formulario de recuperación de contraseña
    public partial class RecuperacionContra : Form
    {
        // Servicio para manejar la lógica de negocio relacionada con el inicio de sesión
        private readonly LoginService _service = new LoginService();

        // Constructor del formulario de recuperación de contraseña
        public RecuperacionContra()
        {
            InitializeComponent();
        }

        // Evento para manejar el clic en el botón "Volver"
        private void btnVolver_Click_1(object sender, EventArgs e)
        {
            // Abre el formulario de inicio de sesión y cierra el actual
            new InicioSesion().Show();
            this.Close();
        }

        // Evento para manejar el clic en el botón "Cambiar"
        private async void btnCambiar_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Obtiene los valores ingresados por el usuario
                string usuario = (txtUsuario.Text ?? "").Trim();
                string nuevaContra = (txtNuevaContra.Text ?? "").Trim();

                // Llama al servicio para cambiar la contraseña
                await _service.CambiarAsync(usuario, nuevaContra);

                // Muestra un mensaje de éxito
                MessageBox.Show("Contraseña actualizada correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Abre el formulario de inicio de sesión y cierra el actual
                new InicioSesion().Show();
                this.Close();
            }
            catch (Exception ex)
            {
                // Manejo de errores durante el cambio de contraseña
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}