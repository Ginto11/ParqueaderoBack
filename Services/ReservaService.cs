using Microsoft.EntityFrameworkCore;
using Parqueadero_Back.Database;
using Parqueadero_Back.Dtos;
using Parqueadero_Back.Interfaces;
using Parqueadero_Back.Models;
using Parqueadero_Back.Servicios;

namespace Parqueadero_Back.Services
{
    public class ReservaService : IRepository<Reserva>
    {
        private readonly AppDbContext context;
        public ReservaService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task Actualizar(Reserva entidad)
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

        public async Task<Reserva?> Buscar(int id)
        {
            try
            {
                return await context.Reserva
                    .Include(reserva => reserva.Cupo)
                    .Include(reserva => reserva.Vehiculo)
                    .FirstOrDefaultAsync(reserva => reserva.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Eliminar(Reserva entidad)
        {
            context.Remove(entidad);
            await context.SaveChangesAsync();
        }

        public async Task Insertar(Reserva entidad)
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

        public async Task<IEnumerable<Reserva>> ObtenerTodos()
        {
            try
            {
                return await context.Reserva
                    .Include(reserva => reserva.Cupo)
                    .Include(reserva => reserva.Vehiculo)
                    .OrderByDescending(reserva => reserva.Id)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Reserva>> ObtenerPorIdDeUsuario(int id)
        {
            try
            {
                return await context.Reserva
                    .Include(reserva => reserva.Vehiculo)
                    .Where(reserva => reserva.Vehiculo!.Usuario!.Id == id)
                    .OrderByDescending(reserva => reserva.Id)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Reserva>> ObtenerReservasMasRecientesPorIdDeUsuario(int id)
        {
            try
            {
                return await context.Reserva
                    .Include(reserva => reserva.Vehiculo)
                    .Where(reserva => reserva.Vehiculo!.Usuario!.Id == id)
                    .OrderByDescending(reserva => reserva.Id)
                    .Take(3)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Reserva?>> ObtenerReservasActivasPorUsuario(int id)
        {
            try
            {
                return await context.Reserva
                    .Include(reserva => reserva.Cupo)
                    .Where(reserva => reserva.Vehiculo!.Usuario!.Id == id && reserva.Estado == true && reserva.Costo == null)
                    .OrderByDescending(reserva => reserva.Id)
                    .Take(5)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<IEnumerable<Reserva?>> ObtenerTodasLasReservasActivas()
        {
            try
            {
                return await context.Reserva
                    .Where(reserva => reserva.EstadoDescripcion == "Activa")
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Object>> ObtenerUltimasReservas()
        {
            try
            {
                return await context.Reserva
                    .Include(reserva => reserva.Vehiculo)
                    .Include(reserva => reserva.Vehiculo!.Usuario)
                    .Select(reserva => new {
                        Id = reserva.Id,
                        NombreUsuario = reserva.Vehiculo!.Usuario!.NombreCompleto,
                        Placa = reserva.Vehiculo.Placa,
                        FechaReserva = reserva.FechaReserva,
                        EstadoDescripcion = reserva.EstadoDescripcion
                    })
                    .Take(7)
                    .OrderByDescending(reserva => reserva.Id)
                    .ToListAsync();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Reserva> BuscarReservaPorCupoId(int id)
        {
            try
            {
                return await context.Reserva
                    .Include(reserva => reserva.Vehiculo)
                    .FirstAsync(reserva => reserva.CupoId == id && reserva.EstadoDescripcion == "Activa")!;

            }
            catch (Exception)
            {
                throw;
            }
        }
        
    }
}
