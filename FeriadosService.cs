using CapaDatos;
using CapaNegocio.Bitacora;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CapaNegocio.Feriados
{
    // Servicio para gestionar feriados
    public class FeriadosService
    {
        // Repositorio para acceder a los datos de feriados
        private readonly FeriadosRepository _repo = new FeriadosRepository();

        // Servicio para registrar eventos en la bitácora
        private readonly BitacoraService _bitacora = new BitacoraService();

        // Devuelve la lista de feriados para mostrar en el grid
        public Task<DataTable> CargarGridAsync() =>
            _repo.ListarFeriadosAsync();

        // Devuelve la lista de usuarios activos
        public Task<DataTable> CargarUsuariosActivosAsync() =>
            _repo.ListarUsuariosActivosAsync();

        // Devuelve los usuarios asociados a un feriado específico
        public Task<DataTable> UsuariosPorFeriadoAsync(int feriadoId) =>
            _repo.ObtenerUsuariosPorFeriadoAsync(feriadoId);

        // Valida los datos antes de guardar o editar un feriado
        public void Validar(DateTime fecha, string tipo, string tituloOtro,
                            string descripcion, bool aplicaATodos, int[] usuarios)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
                throw new Exception("Escribe una descripción.");

            // Verifica que la descripción solo contenga letras y espacios
            if (!Regex.IsMatch(descripcion, @"^[\p{L}\s]+$"))
                throw new Exception("La descripción solo puede contener letras y espacios.");

            tipo = (tipo ?? "").Trim();

            if (string.IsNullOrWhiteSpace(tipo))
                throw new Exception("Selecciona el tipo.");

            if (tipo == "OTRO" && string.IsNullOrWhiteSpace(tituloOtro))
                throw new Exception("Escribe el título para OTRO.");

            if (!aplicaATodos && (usuarios == null || usuarios.Length == 0))
                throw new Exception("Selecciona al menos un usuario.");
        }

        // Guarda un nuevo feriado en la base de datos
        public async Task GuardarAsync(DateTime fecha, string tipo,
            string tituloOtro, string descripcion,
            bool aplicaATodos, int[] usuarios)
        {
            // Valida los datos antes de guardar
            Validar(fecha, tipo, tituloOtro, descripcion, aplicaATodos, usuarios);

            using (var con = _repo.GetConnection())
            {
                await con.OpenAsync();

                using (var trx = await con.BeginTransactionAsync())
                {
                    try
                    {
                        int aplica = aplicaATodos ? 1 : 0;

                        // Inserta el nuevo feriado y obtiene su ID
                        int newId = await _repo.InsertarFeriadoAsync(
                            fecha, tipo, tituloOtro, descripcion, aplica,
                            con, (MySqlTransaction)trx);

                        // Si no aplica a todos, asocia usuarios específicos al feriado
                        if (aplica == 0)
                        {
                            foreach (var uid in usuarios)
                                await _repo.InsertarUsuarioFeriadoAsync(
                                    newId, uid, con, (MySqlTransaction)trx);
                        }

                        await trx.CommitAsync(); // Confirma la transacción

                        // Registra el evento en la bitácora
                        await _bitacora.LogAsync(
                            "Feriados",
                            "INSERT",
                            $"FeriadoId={newId}",
                            $"Fecha={fecha:yyyy-MM-dd} Tipo={tipo}",
                            true);
                    }
                    catch (Exception ex)
                    {
                        await trx.RollbackAsync(); // Revierte la transacción en caso de error

                        // Registra el error en la bitácora
                        await _bitacora.LogAsync(
                            "Feriados",
                            "INSERT",
                            $"Fecha={fecha:yyyy-MM-dd}",
                            "Error al guardar feriado",
                            false,
                            ex.Message);

                        throw;
                    }
                }
            }
        }

        // Edita un feriado existente en la base de datos
        public async Task EditarAsync(int id, DateTime fecha, string tipo,
            string tituloOtro, string descripcion,
            bool aplicaATodos, int[] usuarios)
        {
            if (id <= 0)
                throw new Exception("Selecciona un registro para editar.");

            // Valida los datos antes de editar
            Validar(fecha, tipo, tituloOtro, descripcion, aplicaATodos, usuarios);

            using (var con = _repo.GetConnection())
            {
                await con.OpenAsync();

                using (var trx = await con.BeginTransactionAsync())
                {
                    try
                    {
                        int aplica = aplicaATodos ? 1 : 0;

                        // Actualiza los datos del feriado
                        await _repo.ActualizarFeriadoAsync(
                            id, fecha, tipo, tituloOtro, descripcion, aplica,
                            con, (MySqlTransaction)trx);

                        // Elimina las asociaciones previas de usuarios con el feriado
                        await _repo.BorrarUsuariosDeFeriadoAsync(
                            id, con, (MySqlTransaction)trx);

                        // Si no aplica a todos, asocia usuarios específicos al feriado
                        if (aplica == 0)
                        {
                            foreach (var uid in usuarios)
                                await _repo.InsertarUsuarioFeriadoAsync(
                                    id, uid, con, (MySqlTransaction)trx);
                        }

                        await trx.CommitAsync(); // Confirma la transacción

                        // Registra el evento en la bitácora
                        await _bitacora.LogAsync(
                            "Feriados",
                            "UPDATE",
                            $"FeriadoId={id}",
                            $"Fecha={fecha:yyyy-MM-dd} Tipo={tipo}",
                            true);
                    }
                    catch (Exception ex)
                    {
                        await trx.RollbackAsync(); // Revierte la transacción en caso de error

                        // Registra el error en la bitácora
                        await _bitacora.LogAsync(
                            "Feriados",
                            "UPDATE",
                            $"FeriadoId={id}",
                            "Error al editar feriado",
                            false,
                            ex.Message);

                        throw;
                    }
                }
            }
        }

        // Desactiva un feriado (lo marca como inactivo)
        public async Task DesactivarAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("El ID del feriado debe ser mayor a 0.", nameof(id));

                // Marca el feriado como inactivo en la base de datos
                await _repo.DesactivarFeriadoAsync(id);

                // Registra el evento en la bitácora
                await _bitacora.LogAsync(
                    "Feriados",
                    "DESACTIVAR",
                    $"FeriadoId={id}",
                    "Feriado desactivado",
                    true);
            }
            catch (ArgumentException ex)
            {
                // Registra errores de validación en la bitácora
                await _bitacora.LogAsync(
                    "Feriados",
                    "DESACTIVAR",
                    $"FeriadoId={id}",
                    "Error de validación al desactivar feriado",
                    false,
                    ex.Message);

                throw;
            }
            catch (Exception ex)
            {
                // Registra errores inesperados en la bitácora
                await _bitacora.LogAsync(
                    "Feriados",
                    "DESACTIVAR",
                    $"FeriadoId={id}",
                    "Error inesperado al desactivar feriado",
                    false,
                    ex.Message);

                throw new Exception("Ocurrió un error al intentar desactivar el feriado. Por favor, intente nuevamente más tarde.", ex);
            }
        }

        // Convierte un DataTable en un conjunto de IDs
        public static HashSet<int> DataTableToSet(DataTable dt, string col)
        {
            var set = new HashSet<int>();

            foreach (DataRow r in dt.Rows)
                set.Add(Convert.ToInt32(r[col]));

            return set;
        }
    }
}