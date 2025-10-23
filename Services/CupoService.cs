using Microsoft.EntityFrameworkCore;
using Parqueadero_Back.Database;
using Parqueadero_Back.Interfaces;
using Parqueadero_Back.Models;

namespace Parqueadero_Back.Servicios
{
    public class CupoService : IRepository<Cupo>
    {

        private readonly AppDbContext _context;

        public CupoService(AppDbContext context)
        {
            this._context = context;
        }

        public async Task Actualizar(Cupo entidad)
        {
            try
            {
                _context.Update(entidad);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Cupo?> Buscar(int id)
        {
            try
            {
                return await _context.Cupo
                    .FirstOrDefaultAsync(cupo => cupo.Id == id);  
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Eliminar(Cupo entidad)
        {
            try
            {
                _context.Remove(entidad);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Insertar(Cupo entidad)
        {
            try
            {
                _context.Add(entidad);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<IEnumerable<Cupo>> ObtenerTodos()
        {
            try
            {
                return await _context.Cupo
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Cupo>> ObtenerCuposDisponibles()
        {
            try
            {
                return await _context.Cupo
                    .Where(cupo => cupo.Estado == false)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
