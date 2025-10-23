using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parqueadero_Back.Models;
using Parqueadero_Back.Services;

namespace Parqueadero_Back.Controllers
{
    [Route("api/vehiculos")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        private readonly VehiculoService vehiculoService;
        public VehiculoController(VehiculoService vehiculoService)
        {
            this.vehiculoService = vehiculoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehiculo>>> Get()
        {
            try
            {
                var vehiculos = await vehiculoService.ObtenerTodos();

                return RespuestasService.Ok(vehiculos);
            }
            catch (Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(Vehiculo vehiculo)
        {
            try
            {

                var vehiculoEncontrado = await vehiculoService.BuscarPorPlaca(vehiculo.Placa);

                if (vehiculoEncontrado is not null)
                    return RespuestasService.Conflict($"Ya existe un registro con la placa = ({vehiculo.Placa})");

                await vehiculoService.Insertar(vehiculo);

                return RespuestasService.Created();

            }catch(Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Put(int id, Vehiculo vehiculo)
        {
            try
            {
                var vehiculoEncontrado = await vehiculoService.Buscar(vehiculo.Id);

                if (vehiculoEncontrado is null)
                    return RespuestasService.NotFound($"Vehiculo con ID = {id}, no encontrado.");

                vehiculoEncontrado.Placa = vehiculo.Placa;
                vehiculoEncontrado.Cilindraje = vehiculo.Cilindraje;
                
                await vehiculoService.Actualizar(vehiculo);


                return RespuestasService.NoContent();
            }catch(Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }

        [HttpGet]
        [Route("{placa}")]
        public async Task<ActionResult> Get(string placa)
        {
            try
            {

                var vehiculoEncontrado = await vehiculoService.BuscarPorPlaca(placa);

                if (vehiculoEncontrado is null)
                    return RespuestasService.NotFound($"Vehiculo con Placa = {placa}, no encontrado.");

                return RespuestasService.Ok(vehiculoEncontrado);

            }catch(Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }

        [HttpGet]
        [Route("usuario/{id}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                var vehiculos = await vehiculoService.BuscarPorUsuario(id);

                if (vehiculos is null)
                    return RespuestasService.NotFound("No tienes vehiculos registrados.");

                return RespuestasService.Ok(vehiculos);

            }catch(Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }
    }
}
