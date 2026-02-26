using CapaDatos;
using CapaNegocio.Bitacora;
using System;
using System.Data;
using System.Threading.Tasks;

namespace CapaNegocio.Incidencia
{
    public class IncidenciasService
    {
        // Acceso a datos
        private readonly IncidenciasRepository _repo = new IncidenciasRepository();

        // Servicio de bitácora
        private readonly BitacoraService _bitacora = new BitacoraService();

        // Umbrales en minutos
        public const int UMBRAL_TARDANZA_MIN = 60;
        public const int UMBRAL_SALIDA_TEMPRANA_MIN = 60;

        // Carga incidencias pendientes
        public Task<DataTable> CargarPendientesAsync(DateTime desde, DateTime hasta, string search, int? userId)
        {
            return _repo.ObtenerPendientesAsync(
                desde, hasta,
                UMBRAL_TARDANZA_MIN,
                UMBRAL_SALIDA_TEMPRANA_MIN,
                search, userId);
        }

        // Guarda una incidencia y actualiza asistencia
        public async Task GuardarIncidenciaAsync(
            int userId,
            DateTime fecha,
            string tipo,
            string justificacion,
            string tipoJust,
            string codigo)
        {
            try
            {
                // Limpieza de datos
                tipo = (tipo ?? "").Trim();
                justificacion = (justificacion ?? "").Trim();
                tipoJust = (tipoJust ?? "").Trim();
                codigo = (codigo ?? "").Trim();

                // Validaciones básicas
                if (string.IsNullOrWhiteSpace(tipo))
                    throw new Exception("Selecciona el Tipo de incidencia.");

                if (string.IsNullOrWhiteSpace(justificacion))
                    throw new Exception("Escribe una justificación / motivo.");

                // Verifica duplicados
                if (await _repo.ExisteIncidenciaAsync(userId, fecha))
                    throw new Exception("Ya existe una incidencia registrada para este usuario en esa fecha.");

                string codigoDocumentoFinal = null;

                // Validaciones para AUSENCIA
                if (tipo == "AUSENCIA")
                {
                    if (string.IsNullOrWhiteSpace(tipoJust))
                        throw new Exception("Para AUSENCIA selecciona el Tipo de justificación.");

                    if (string.IsNullOrWhiteSpace(codigo))
                    {
                        if (tipoJust == "MÉDICA")
                            throw new Exception("Para AUSENCIA MÉDICA debes ingresar el Código de incapacidad.");
                        else
                            throw new Exception("Para AUSENCIA PERSONAL debes ingresar el Código de auditoría.");
                    }

                    codigoDocumentoFinal = codigo;
                    justificacion = $"[{tipoJust}] {justificacion}".Trim();
                }

                // Determina nuevo estado
                string nuevoEstado = null;

                switch (tipo)
                {
                    case "AUSENCIA":
                        nuevoEstado = "AUSENCIA_JUSTIFICADA";
                        break;

                    case "TARDANZA":
                        nuevoEstado = "TARDANZA_JUSTIFICADA";
                        break;

                    case "SALIDA_TEMPRANA":
                        nuevoEstado = "SALIDA_TEMPRANA_JUSTIFICADA";
                        break;

                    default:
                        nuevoEstado = null;
                        break;
                }

                // Guarda incidencia y actualiza asistencia
                await _repo.GuardarIncidenciaYActualizarAsistenciaAsync(
                    userId, fecha, tipo, justificacion, codigoDocumentoFinal, nuevoEstado);

                // Registra éxito en bitácora
                await _bitacora.LogAsync(
                    "Incidencias",
                    "INSERT",
                    $"UserID={userId} Fecha={fecha:yyyy-MM-dd}",
                    $"Tipo={tipo}",
                    true);
            }
            catch (Exception ex)
            {
                // Registra error en bitácora
                await _bitacora.LogAsync(
                    "Incidencias",
                    "INSERT",
                    $"UserID={userId} Fecha={fecha:yyyy-MM-dd}",
                    "Error al guardar incidencia",
                    false,
                    ex.Message);

                throw;
            }
        }

        // Devuelve primer día del mes actual
        public static DateTime PrimerDiaMesActual()
            => new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

        // Convierte texto a int nullable
        public static int? ParseUserId(string userIdTxt)
        {
            if (string.IsNullOrWhiteSpace(userIdTxt)) return null;
            if (int.TryParse(userIdTxt.Trim(), out int id)) return id;
            throw new Exception("UserID inválido.");
        }
    }
}