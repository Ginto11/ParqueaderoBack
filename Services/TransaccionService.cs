using Microsoft.EntityFrameworkCore;
using Parqueadero_Back.Database;
using Parqueadero_Back.Dtos;
using Parqueadero_Back.Interfaces;
using Parqueadero_Back.Models;

namespace Parqueadero_Back.Services
{
    public class TransaccionService : IRepository<Transaccion>
    {

        private readonly AppDbContext context;

        public TransaccionService(AppDbContext context)
        {
            this.context = context;
        }

        public Task Actualizar(Transaccion entidad)
        {
            throw new NotImplementedException();
        }

        public Task<Transaccion?> Buscar(int id)
        {
            throw new NotImplementedException();
        }

        public Task Eliminar(Transaccion entidad)
        {
            throw new NotImplementedException();
        }

        public async Task Insertar(Transaccion entidad)
        {
            try
            {
                context.Transaccion.Add(entidad);

                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<IEnumerable<Transaccion>> ObtenerTodos()
        {
            throw new NotImplementedException();
        }

        public async Task<IngresoDto> ObtenerIngresosDiarios()
        {
            try
            {
                var hoy = DateTime.Today;

                var costo = await context.Transaccion
                    .Where(transaccion => transaccion.FechaHora.Date == hoy && transaccion.TipoTransaccionId == 1)
                    .Select(transaccion => (decimal)transaccion.Monto)
                    .SumAsync();

                return new IngresoDto
                {
                    Costo = costo
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
