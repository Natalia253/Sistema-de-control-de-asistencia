using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CapaPresentacion.Formularios;



namespace CapaPresentacion
{
    // Clase principal del formulario de inicio
    public partial class Form1 : Form
    {
        // Constructor del formulario principal
        public Form1()
        {
            InitializeComponent();

            // Configuraciones iniciales del formulario
            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Maximizar al iniciar
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;

            // Configuración inicial de los botones de maximizar/restaurar
            btnMaximizar.Visible = false;
            btnRestaurar.Visible = true;

            // Cargar el formulario de inicio
            ActivarBoton(btnInicio);
            AbrirFormulario<InterfaceInicio>();
        }

        // Variables para redimensionar el formulario
        private int tolerance = 12;
        private const int WM_NCHITTEST = 0x84;
        private const int HTBOTTOMRIGHT = 17;
        private Rectangle sizeGripRectangle;

        // Manejo de eventos de Windows para redimensionar el formulario
        protected override void WndProc(ref Message m)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                base.WndProc(ref m);
                return;
            }

            if (m.Msg == WM_NCHITTEST)
            {
                base.WndProc(ref m);

                Point hitPoint = this.PointToClient(
                    new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16)
                );

                if (sizeGripRectangle.Contains(hitPoint))
                    m.Result = new IntPtr(HTBOTTOMRIGHT);

                return;
            }

            base.WndProc(ref m);
        }

        // Evento que se ejecuta al cambiar el tamaño del formulario
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (this.WindowState == FormWindowState.Maximized)
            {
                pnlContenedor.Region = null;
                return;
            }

            Region region = new Region(this.ClientRectangle);

            sizeGripRectangle = new Rectangle(
                this.ClientRectangle.Width - tolerance,
                this.ClientRectangle.Height - tolerance,
                tolerance,
                tolerance
            );

            region.Exclude(sizeGripRectangle);
            pnlContenedor.Region = region;

            this.Invalidate();
        }

        // Evento para pintar el área de redimensionamiento
        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.WindowState != FormWindowState.Maximized)
            {
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(244, 244, 244)))
                {
                    e.Graphics.FillRectangle(brush, sizeGripRectangle);
                }

                ControlPaint.DrawSizeGrip(
                    e.Graphics,
                    Color.Transparent,
                    sizeGripRectangle
                );
            }

            base.OnPaint(e);
        }

        // Métodos para mover el formulario
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelBarraTitulo_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0x112, 0xf012, 0);
            }
        }

        // Eventos para los botones de la ventana
        private void btnCerrar_Click(object sender, EventArgs e) => Application.Exit();

        private void btnMinimizar_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Minimized;

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;

            btnMaximizar.Visible = false;
            btnRestaurar.Visible = true;
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;

            btnMaximizar.Visible = true;
            btnRestaurar.Visible = false;
        }

        // Eventos para los botones del menú
        private void btnInicio_Click(object sender, EventArgs e)
        {
            ActivarBoton(btnInicio);
            AbrirFormulario<InterfaceInicio>();
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            ActivarBoton(btnUsuarios);
            AbrirFormulario<Usuarios>();
        }

        private void btnAsistencias_Click(object sender, EventArgs e)
        {
            ActivarBoton(btnAsistencias);
            AbrirFormulario<Asistencias>();
        }

        private void btnincidencias_Click(object sender, EventArgs e)
        {
            ActivarBoton(btnincidencias);
            AbrirFormulario<IncidenciasAsistencia>();
        }

        private void btnCalendario_Click(object sender, EventArgs e)
        {
            ActivarBoton(btnCalendario);
            AbrirFormulario<Feriados>();
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            ActivarBoton(btnReportes);
            AbrirFormulario<Reportes>();
        }

        // Métodos para manejar la activación de botones
        private readonly Color colorActivo = Color.FromArgb(12, 61, 92);
        private readonly Color colorInactivo = Color.FromArgb(37, 54, 75);

        private void ActivarBoton(Button boton)
        {
            ResetearBotones();
            boton.BackColor = colorActivo;
        }

        private void ResetearBotones()
        {
            btnInicio.BackColor = colorInactivo;
            btnUsuarios.BackColor = colorInactivo;
            btnAsistencias.BackColor = colorInactivo;
            btnincidencias.BackColor = colorInactivo;
            btnCalendario.BackColor = colorInactivo;
            btnReportes.BackColor = colorInactivo;
        }

        // Método para abrir formularios dentro del panel principal
        private void AbrirFormulario<MiForm>() where MiForm : Form, new()
        {
            Form formulario = panelFormularios.Controls.OfType<MiForm>().FirstOrDefault();

            if (formulario == null)
            {
                formulario = new MiForm
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Dock = DockStyle.Fill
                };

                panelFormularios.Controls.Add(formulario);
                panelFormularios.Tag = formulario;
                formulario.Show();
                formulario.BringToFront();
            }
            else
            {
                formulario.BringToFront();
            }
        }
    }
}