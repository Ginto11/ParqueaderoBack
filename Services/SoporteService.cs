using Microsoft.EntityFrameworkCore;
using Parqueadero_Back.Database;
using Parqueadero_Back.Interfaces;
using Parqueadero_Back.Models;

namespace Parqueadero_Back.Services
{
    public class SoporteService : IRepository<Soporte>
    {

        private readonly AppDbContext context;

        public SoporteService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task Actualizar(Soporte entidad)
        {
            try
            {
                context.Update(entidad);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Soporte?> Buscar(int id)
        {
            try
            {
                return await context.Soporte
                    .Include(soporte => soporte.Usuario)
                    .FirstOrDefaultAsync(soporte => soporte.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Eliminar(Soporte entidad)
        {
            try
            {
                context.Remove(entidad);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Insertar(Soporte entidad)
        {
            try
            {
                context.Add(entidad);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Soporte>> ObtenerTodos()
        {
            try
            {
                return await context.Soporte.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
