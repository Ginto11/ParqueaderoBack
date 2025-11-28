using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Parqueadero_Back.Database;
using Parqueadero_Back.Dtos;
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
                    .Where(cupo => cupo.EstadoDescripcion == "Disponible")
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<CupoOcupadoDto>> ObtenerCuposOcupados()
        {
            try
            {
                return await _context.Reserva
                    .Include(reserva => reserva.Cupo)
                    .Include(reserva => reserva.Vehiculo)
                    .Include(reserva => reserva.Vehiculo!.Usuario)
                    .Where(reserva => (reserva.Cupo!.EstadoDescripcion == "Reservado" || reserva.Cupo!.EstadoDescripcion == "Ocupado") && reserva.EstadoDescripcion == "Activa")
                    .Select(c => new CupoOcupadoDto
                    {
                        Id = c.CupoId,
                        Placa = c.Vehiculo!.Placa,
                        FechaIngresoEstipulada = c.FechaIngresoEstipulada,
                        FechaIngresoReal = c.FechaIngresoReal,
                        EstadoDescripcion =     c.Cupo!.EstadoDescripcion,
                        NombreUsuario = c.Vehiculo!.Usuario!.NombreCompleto,
                        Duracion = (int)Math.Ceiling((double)EF.Functions.DateDiffMinute(c.FechaIngresoReal, DateTime.Now)!) / 60,
                        Costo = (Math.Ceiling((decimal)EF.Functions.DateDiffMinute(c.FechaIngresoReal, DateTime.Now)!) / 60) * 3500
                    })
                    .OrderByDescending(c => c.Id)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CupoOcupadoDto?> BuscarCupoPorPlaca(string placa)
        {
            try
            {
                return await _context.Reserva
                    .Include(reserva => reserva.Cupo)
                    .Include(reserva => reserva.Vehiculo)
                    .Include(reserva => reserva.Vehiculo!.Usuario)
                    .Select(c => new CupoOcupadoDto
                    {
                        Id = c.CupoId,
                        Placa = c.Vehiculo!.Placa,
                        FechaIngresoEstipulada = c.FechaIngresoEstipulada,
                        FechaIngresoReal = c.FechaIngresoReal,
                        EstadoDescripcion = c.Cupo!.EstadoDescripcion,
                        NombreUsuario = c.Vehiculo!.Usuario!.NombreCompleto,
                        Duracion = (int)Math.Ceiling((double)EF.Functions.DateDiffMinute(c.FechaIngresoReal, DateTime.Now)!) / 60,
                        Costo = (Math.Ceiling((decimal)EF.Functions.DateDiffMinute(c.FechaIngresoReal, DateTime.Now)!) / 60) * 3500
                    })
                    .OrderByDescending(c => c.Id)
                    .FirstOrDefaultAsync(reserva => reserva.Placa == placa);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> ObtenerVehiculosTotalesDelParqueadero()
        {
            try
            {
                var cupos = await _context.Cupo
                    .Where(cupo => cupo.EstadoDescripcion == "Ocupado")
                    .ToListAsync();

                return cupos.Count;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
