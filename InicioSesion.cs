using CapaNegocio.Login;
using System;
using System.Windows.Forms;

namespace CapaPresentacion
{
    // Clase para el formulario de inicio de sesión
    public partial class InicioSesion : Form
    {
        // Servicio para manejar la lógica de negocio relacionada con el inicio de sesión
        private readonly LoginService _service = new LoginService();

        // Constructor del formulario de inicio de sesión
        public InicioSesion()
        {
            InitializeComponent();
        }

        // Evento para manejar el clic en el botón de inicio de sesión
        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                // Intenta iniciar sesión con las credenciales ingresadas
                bool ok = await _service.LoginAsync(txtUsuario.Text, txtPassword.Text);

                if (ok)
                {
                    // Si las credenciales son correctas, abre el formulario principal
                    new Form1().Show();
                    this.Hide();
                }
                else
                {
                    // Si las credenciales son incorrectas, muestra un mensaje de error
                    MessageBox.Show("Usuario o contraseña incorrectos.",
                        "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores durante el inicio de sesión
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Evento para cerrar la aplicación
        private void btnCerrar_Click(object sender, EventArgs e) => Application.Exit();

        // Evento para manejar el clic en el enlace de recuperación de contraseña
        private void lblOlvidoClave_Click(object sender, EventArgs e)
        {
            this.Hide(); // Oculta el formulario actual
            new RecuperacionContra().Show(); // Muestra el formulario de recuperación de contraseña
        }

        // Eventos para mostrar y ocultar la contraseña
        private void btnVerPassword_MouseDown(object sender, MouseEventArgs e) => txtPassword.UseSystemPasswordChar = false;
        private void btnVerPassword_MouseUp(object sender, MouseEventArgs e) => txtPassword.UseSystemPasswordChar = true;
        private void btnVerPassword_MouseLeave(object sender, EventArgs e) => txtPassword.UseSystemPasswordChar = true;
    }
}