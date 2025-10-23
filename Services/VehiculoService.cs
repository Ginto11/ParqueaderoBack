using Microsoft.EntityFrameworkCore;
using Parqueadero_Back.Database;
using Parqueadero_Back.Interfaces;
using Parqueadero_Back.Models;

namespace Parqueadero_Back.Services
{
    public class VehiculoService : IRepository<Vehiculo>
    {
        private readonly AppDbContext context;
        public VehiculoService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task Actualizar(Vehiculo entidad)
        {
            try
            {
                context.Update(entidad);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
        }

        public async Task<Vehiculo?> Buscar(int id)
        {
            try
            {
                return await context.Vehiculo
                    .Include(vehiculo => vehiculo.Usuario)
                    .FirstOrDefaultAsync(vehiculo => vehiculo.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Eliminar(Vehiculo entidad)
        {
            try
            {
                context.Remove(entidad);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
        }

        public async Task Insertar(Vehiculo entidad)
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

        public async Task<IEnumerable<Vehiculo>> ObtenerTodos()
        {
            try
            {
                return await context.Vehiculo
                    .Include(vehiculo => vehiculo.Usuario)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Vehiculo?> BuscarPorPlaca(string placa)
        {
            try
            {
                return await context.Vehiculo
                    .Include(vehiculo => vehiculo.Usuario)
                    .FirstOrDefaultAsync(vehiculo => vehiculo.Placa == placa);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Vehiculo>> BuscarPorUsuario(int id)
        {
            try
            {
                return await context.Vehiculo
                    .Where(vehiculo => vehiculo.UsuarioId == id)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
