using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parqueadero_Back.Dtos;
using Parqueadero_Back.Models;
using Parqueadero_Back.Services;
using Parqueadero_Back.Servicios;

namespace Parqueadero_Back.Controllers
{
    [Route("api/reservas")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly ReservaService reservaService;
        private readonly CupoService cupoService;
        public ReservaController(ReservaService reservaService, CupoService cupoService) 
        {
            this.reservaService = reservaService;
            this.cupoService = cupoService;
        }

        [HttpGet]
        [Route("usuario/{id}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                var reservas = await reservaService.ObtenerPorIdDeUsuario(id);

                if (reservas is null)
                    return RespuestasService.NotFound("El usuario no tiene reservas.");

                return RespuestasService.Ok(reservas);
            }
            catch (Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }

        [HttpGet]
        [Route("recientes/{id}")]
        public async Task<ActionResult> ObtenerReservasMasRecientes(int id)
        {
            try
            {
                var reservas = await reservaService.ObtenerReservasMasRecientesPorIdDeUsuario(id);

                if (reservas is null)
                    return RespuestasService.NotFound("El usuario no tiene reservas.");

                return RespuestasService.Ok(reservas);
            }
            catch (Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }

        [HttpGet]
        [Route("activas/{usuarioId}")]
        public async Task<ActionResult> ObtenerReservasActivasPorUsuario(int usuarioId)
        {
            try
            {
                var reservasActivas = await reservaService.ObtenerReservasActivasPorUsuario(usuarioId);

                if (reservasActivas is null)
                    return RespuestasService.NotFound("El usuario no tiene reservas activas.");

                return RespuestasService.Ok(reservasActivas);
            }catch(Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(PostReserva reservaDto)
        {
            try
            {
                var cupo = await cupoService.Buscar(reservaDto.CupoId);

                if (cupo is null)
                    return RespuestasService.NotFound($"Cupo con ID = {reservaDto.CupoId}, no encontrado.");

                cupo.Estado = true;
                cupo.EstadoDescripcion = "Reservado";

                var reserva = new Reserva
                {
                    VehiculoId = reservaDto.VehiculoId,
                    CupoId = reservaDto.CupoId,
                    FechaReserva = DateTime.Now,
                    FechaIngresoEstipulada = reservaDto.FechaIngresoEstipulada,
                    EstadoDescripcion = "Activa",
                    Estado = true
                };

                await reservaService.Insertar(reserva);
                await cupoService.Actualizar(cupo);

                return RespuestasService.Created();
            }catch(Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Cancel(int id)
        {
            try
            {

                var reserva = await reservaService.Buscar(id);

                if (reserva is null)
                    return RespuestasService.NotFound($"Reserva con ID = {id}, no encontrada.");

                var cupo = await cupoService.Buscar(reserva.CupoId);

                if (cupo is null)
                    return RespuestasService.NotFound($"Cupo con Id = {reserva.CupoId}, no encontrado.");


                cupo.Estado = false;
                cupo.EstadoDescripcion = "Disponible";

                reserva.Estado = false;
                reserva.Costo = 0.00;
                reserva.Duracion = 0;
                reserva.EstadoDescripcion = "Cancelada";

                await reservaService.Actualizar(reserva);
                await cupoService.Actualizar(cupo);

                return RespuestasService.NoContent();

            }catch(Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }

        [HttpGet]
        [Route("activas")]
        public async Task<ActionResult> GetCantidadReservasActivas()
        {
            try
            {

                var reservasActivas = await reservaService.ObtenerTodasLasReservasActivas();

                if (reservaService is null)
                    return RespuestasService.NotFound("No hay reservas activas el dia de hooy.");

                return RespuestasService.Ok(reservasActivas);

            }catch(Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }

        [HttpGet]
        [Route("ultimas-reservas")]
        public async Task<ActionResult> GetUltimasReservas()
        {
            try
            {

                var ultimasReservas = await reservaService.ObtenerUltimasReservas();

                if (reservaService is null)
                    return RespuestasService.NotFound("No hay reservas activas el dia de hooy.");

                return RespuestasService.Ok(ultimasReservas);

            }
            catch (Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }

        [HttpGet]
        [Route("ingresar-vehiculo/{cupoId}")]
        public async Task<ActionResult> IngresarReservaConCupoId(int cupoId)
        {
            try
            {
                var reserva = await reservaService.BuscarReservaPorCupoId(cupoId);

                if (reserva is null)
                    return RespuestasService.NotFound($"Reserva con CupoId = {cupoId}, no encontrada.");

                var cupo = await cupoService.Buscar(cupoId);

                if (cupo is null)
                    return RespuestasService.NotFound($"Cupo con Id = {cupoId}, no encontrado.");

                reserva.FechaIngresoReal = DateTime.Now;
                cupo.EstadoDescripcion = "Ocupado";

                await cupoService.Actualizar(cupo);
                await reservaService.Actualizar(reserva);

                return RespuestasService.Ok("Vehiculo ingresado correctamente");

                
            }catch(Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }
    }
}
