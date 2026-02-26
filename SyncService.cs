using System;
using System.Data;
using System.Threading.Tasks;
using CapaDatos.Inicio;
using CapaNegocio.Bitacora;

namespace CapaNegocio.Inicio
{
    // Servicio para gestionar la sincronización con el dispositivo biométrico
    public class SyncService
    {
        // Repositorio para acceder a los datos de sincronización
        private readonly SyncRepository _repo;

        // Servicio para registrar eventos en la bitácora
        private readonly BitacoraService _bitacora = new BitacoraService();

        // Constructor que inicializa el repositorio con la IP y el puerto del dispositivo
        public SyncService(string ip, int puerto)
        {
            _repo = new SyncRepository(ip, puerto);
        }

        // Método para realizar la sincronización con el dispositivo
        public async Task<(bool Conectado, int ErrorConexion)> SincronizarAsync()
        {
            try
            {
                // Ejecuta la sincronización de manera asíncrona
                var resultado = await Task.Run(() => _repo.SincronizarAsync().GetAwaiter().GetResult());

                // Registra el resultado de la sincronización en la bitácora
                if (resultado.Conectado)
                {
                    await _bitacora.LogAsync(
                        "Sync",
                        "SYNC_OK",
                        null,
                        "Sincronización completada correctamente",
                        true);
                }
                else
                {
                    await _bitacora.LogAsync(
                        "Sync",
                        "SYNC_ERROR",
                        null,
                        $"Error de conexión código={resultado.ErrorConexion}",
                        false);
                }

                return resultado; // Devuelve el resultado de la sincronización
            }
            catch (Exception ex)
            {
                // Registra cualquier excepción ocurrida durante la sincronización
                await _bitacora.LogAsync(
                    "Sync",
                    "SYNC_EXCEPTION",
                    null,
                    "Error inesperado durante sincronización",
                    false,
                    ex.Message);

                throw; // Lanza la excepción para manejarla externamente
            }
        }

        // Método para contar el número de usuarios registrados en el dispositivo
        public Task<int> ContarUsuariosAsync()
            => Task.Run(() => _repo.ContarUsuarios());

        // Método para contar el número de asistencias registradas hoy
        public Task<int> ContarAsistenciaHoyAsync()
            => Task.Run(() => _repo.ContarAsistenciaHoy());

        // Método para obtener los últimos registros de asistencia
        public Task<DataTable> UltimosRegistrosAsync()
            => Task.Run(() => _repo.UltimosRegistros());
    }
}