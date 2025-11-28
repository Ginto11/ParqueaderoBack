using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parqueadero_Back.Models;
using Parqueadero_Back.Services;
using Parqueadero_Back.Servicios;

namespace Parqueadero_Back.Controllers
{
    [Route("api/cupos")]
    [ApiController]
    public class CupoController : ControllerBase
    {
        private readonly CupoService cupoService;
        private readonly ReservaService reservaService;
        private readonly TransaccionService transaccionService;
        public CupoController(CupoService cupoService, ReservaService reservaService, TransaccionService transaccionService)
        {
            this.cupoService = cupoService;
            this.transaccionService = transaccionService;
            this.reservaService = reservaService;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Cupo cupo)
        {
            try
            {
                await cupoService.Insertar(cupo);

                return RespuestasService.Created();

            }catch(Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Cupo>> Get(int id)
        {
            try
            {
                var cupo = await cupoService.Buscar(id);

                if (cupo is null)
                    return RespuestasService.NotFound($"Cupo con ID = {id}, no encontrado.");

                return Ok(cupo);

            }
            catch (Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cupo>>> Get()
        {
            try
            {
                var cupos = await cupoService.ObtenerTodos();

                return Ok(cupos);

            }
            catch (Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }



        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Put(int id, Cupo cupo)
        {
            try
            {
                var cupoEncontrado = await cupoService.Buscar(id);

                if (cupoEncontrado is null)
                    return RespuestasService.NotFound($"Cupo con ID = {id}, no encontrado.");

                await cupoService.Actualizar(cupoEncontrado);

                return RespuestasService.NoContent();

            }
            catch (Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }

        [HttpGet]
        [Route("disponibles")]
        public async Task<ActionResult> GetCuposDisponibles()
        {
            try
            {
                var cupos = await cupoService.ObtenerCuposDisponibles();

                return RespuestasService.Ok(cupos);
            }catch(Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }

        [HttpGet]
        [Route("ocupados")]
        public async Task<ActionResult> GetCuposOcupados()
        {
            try
            {
                var cuposOcupados = await cupoService.ObtenerCuposOcupados();

                return RespuestasService.Ok(cuposOcupados);

            }catch(Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var cupo = await cupoService.Buscar(id);

                if (cupo is null)
                    return RespuestasService.NotFound($"Cupo con ID = {id}, no encontrado.");

                await cupoService.Eliminar(cupo);

                return RespuestasService.NoContent();

            }catch(Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }

        [HttpGet]
        [Route("cantidad-vehiculos")]
        public async Task<ActionResult> GetCantidadVehiculos()
        {
            try
            {
                var cantidadVehiculos = await cupoService.ObtenerVehiculosTotalesDelParqueadero();

                return RespuestasService.Ok(cantidadVehiculos);

            }
            catch (Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }

        [HttpGet]
        [Route("salida-vehiculo/{cupoId}")]
        public async Task<ActionResult> SalidaVehiculo(int cupoId)
        {
            try
            {
                var reserva = await reservaService.BuscarReservaPorCupoId(cupoId);

                if (reserva is null)
                    return RespuestasService.NotFound($"Reserva con CupoId = {cupoId}, no encontrada.");

                var cupo = await cupoService.Buscar(cupoId);

                if (cupo is null)
                    return RespuestasService.NotFound($"Cupo con Id = {cupoId}, no encontrado.");


                var costoCalculado = ((DateTime.Now - reserva.FechaIngresoReal!).Value.TotalMinutes / 60) * 3500;

                reserva.EstadoDescripcion = "Finalizada";
                reserva.Duracion = ((DateTime.Now - reserva.FechaIngresoReal!).Value.TotalMinutes / 60);
                reserva.FechaSalida = DateTime.Now;
                reserva.Costo = costoCalculado; 


                cupo.Estado = false;
                cupo.EstadoDescripcion = "Disponible";

                var transaccion = new Transaccion
                {
                    MetodoPagoId = 1, //EFECTIVO
                    TipoTransaccionId = 1, //INGRESO
                    Descripcion = $"Salida vehículo placa {reserva.Vehiculo!.Placa}",
                    FechaHora = DateTime.Now,
                    Monto = (decimal)costoCalculado,
                    ReservaId = reserva.Id,
                };

                await cupoService.Actualizar(cupo);
                await reservaService.Actualizar(reserva);
                await transaccionService.Insertar(transaccion);


                return RespuestasService.Ok("Salida exitosa");

            }
            catch (Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }

        [HttpGet]
        [Route("vehiculo-placa/{placa}")]
        public async Task<ActionResult> BuscarCupoPorPlacaVehiculo(string placa)
        {
            try
            {
                var cupo = await cupoService.BuscarCupoPorPlaca(placa);

                if (cupo is null)
                    return RespuestasService.NotFound($"Vehiculo con placa = {placa}, no encontrado.");

                return RespuestasService.Ok(cupo);
            }
            catch (Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        } 

        
    }
}
