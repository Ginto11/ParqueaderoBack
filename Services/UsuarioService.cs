using Microsoft.EntityFrameworkCore;
using Parqueadero_Back.Database;
using Parqueadero_Back.Interfaces;
using Parqueadero_Back.Models;
using Parqueadero_Back.Utilities;

namespace Parqueadero_Back.Services
{
    public class UsuarioService : IRepository<Usuario>
    {

        private readonly AppDbContext _context;
        private readonly Utilidad utilidad;

        public UsuarioService(AppDbContext context, Utilidad utilidad) 
        {
            this._context = context;
            this.utilidad = utilidad;
        }

        public async Task Actualizar(Usuario entidad)
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

        public async Task<Usuario?> Buscar(int id)
        {
            try
            {
                return await _context.Usuario.Include(usuario => usuario.Rol).FirstOrDefaultAsync(usuario => usuario.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Eliminar(Usuario entidad)
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

        public async Task Insertar(Usuario entidad)
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

        public async Task<IEnumerable<Usuario>> ObtenerTodos()
        {
            try
            {
                return await _context.Usuario.Include(usuario => usuario.Rol).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Usuario?> BuscarPorIdentificador(string identificador)
        {
            try
            {
                return await _context.Usuario.FirstOrDefaultAsync(usuario => usuario.IdentificadorUsuario == identificador);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Usuario?> BuscarPorUsuarioYContrasena(string identificador, string contrasena)
        {
            try
            {
                return await _context.Usuario
                    .Include(usuario => usuario.Rol)
                    .FirstOrDefaultAsync(usuario => usuario.IdentificadorUsuario == identificador && usuario.Contrasena == utilidad.Encriptar(contrasena));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
