using CapaDatos;
using System.Data;
using System.Threading.Tasks;

namespace CapaNegocio.Asistencia
{
    public class AsistenciasService
    {
        // Repositorio para acceder a los datos de asistencias
        private readonly AsistenciasRepository _repo = new AsistenciasRepository();

        // Método para cargar las asistencias del día de hoy de forma asíncrona
        public Task<DataTable> CargarHoyAsync()
        {
            return _repo.ObtenerAsistenciasDeHoyAsync();
        }
    }
}