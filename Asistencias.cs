using CapaNegocio.Asistencia;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CapaPresentacion.Formularios
{
    // Formulario para gestionar asistencias
    public partial class Asistencias : Form
    {
        // Servicio para manejar la lógica de asistencias
        private readonly AsistenciasService _service = new AsistenciasService();

        // Constructor del formulario
        public Asistencias()
        {
            InitializeComponent();

            // Eventos para cargar y refrescar asistencias
            this.Load += Asistencias_Load;
            btnRefrescar.Click += btnRefrescar_Click;
        }

        // Evento que se ejecuta al cargar el formulario
        private async void Asistencias_Load(object sender, EventArgs e)
        {
            await CargarHoyAsync(); // Carga las asistencias del día
        }

        // Evento del botón para refrescar las asistencias
        private async void btnRefrescar_Click(object sender, EventArgs e)
        {
            await CargarHoyAsync(); // Refresca las asistencias del día
        }

        // Método para cargar las asistencias del día
        private async Task CargarHoyAsync()
        {
            try
            {
                lblEstado.Text = "Cargando..."; // Actualiza el estado
                btnRefrescar.Enabled = false; // Desactiva el botón mientras carga

                // Obtiene los datos de asistencias del día
                DataTable dt = await _service.CargarHoyAsync();

                // Configura el DataGridView con los datos obtenidos
                dGVHoy.AutoGenerateColumns = true;
                dGVHoy.DataSource = dt;

                // ====== Configuración visual del DataGridView ======

                // Estilo general
                dGVHoy.BackgroundColor = Color.White;
                dGVHoy.DefaultCellStyle.BackColor = Color.White;
                dGVHoy.AlternatingRowsDefaultCellStyle.BackColor = Color.White;

                dGVHoy.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dGVHoy.BorderStyle = BorderStyle.None;
                dGVHoy.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dGVHoy.RowHeadersVisible = false;
                dGVHoy.EnableHeadersVisualStyles = false;

                // Estilo del encabezado
                dGVHoy.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(37, 54, 75);
                dGVHoy.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dGVHoy.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dGVHoy.ColumnHeadersHeight = 42;

                // Estilo de las filas
                dGVHoy.DefaultCellStyle.Font = new Font("Segoe UI", 10);
                dGVHoy.RowTemplate.Height = 38;

                // Estilo de selección
                dGVHoy.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 230, 241);
                dGVHoy.DefaultCellStyle.SelectionForeColor = Color.Black;

                // Centra la columna "MinutosTarde" si existe
                if (dGVHoy.Columns["MinutosTarde"] != null)
                    dGVHoy.Columns["MinutosTarde"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                lblEstado.Text = $"Registros: {dt.Rows.Count}"; // Muestra el total de registros
            }
            catch (Exception ex)
            {
                lblEstado.Text = "Error cargando asistencias."; // Muestra un mensaje de error
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRefrescar.Enabled = true; // Reactiva el botón
            }
        }
    }
}