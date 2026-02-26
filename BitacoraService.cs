using CapaDatos;
using CapaDatos.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CapaNegocio.Bitacora
{
    public class BitacoraService
    {
        private readonly BitacoraRepository _repo = new BitacoraRepository();

        public Task LogAsync(
            string modulo,
            string accion,
            string referencia,
            string detalle,
            bool exitoso,
            string errorMensaje = null)
        {
            return _repo.RegistrarAsync(modulo, accion, referencia, detalle, exitoso, errorMensaje);
        }

        // (Opcional) para ver logs desde un grid admin
        public Task<List<BitacoraEntry>> ObtenerUltimosAsync(int limite = 200)
            => _repo.ObtenerUltimosAsync(limite);
    }
}