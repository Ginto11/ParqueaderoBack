using Microsoft.EntityFrameworkCore;
using Parqueadero_Back.Database;
using Parqueadero_Back.Interfaces;
using Parqueadero_Back.Models;

namespace Parqueadero_Back.Services
{
    public class MetodoPagoService : IRepository<MetodoPago>
    {
        private readonly AppDbContext context;

        public MetodoPagoService(AppDbContext context)
        {
            this.context = context;
        }

        public Task Actualizar(MetodoPago entidad)
        {
            throw new NotImplementedException();
        }

        public async Task<MetodoPago?> Buscar(int id)
        {
            try
            {
                return await context.MetodoPago
                    .FirstOrDefaultAsync(metodo => metodo.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task Eliminar(MetodoPago entidad)
        {
            throw new NotImplementedException();
        }

        public Task Insertar(MetodoPago entidad)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MetodoPago>> ObtenerTodos()
        {
            throw new NotImplementedException();
        }
    }
}
