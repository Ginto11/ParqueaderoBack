using Microsoft.AspNetCore.Mvc;
using Parqueadero_Back.Models;
using Parqueadero_Back.Services;
using Parqueadero_Back.Servicios;

namespace Parqueadero_Back.Controllers
{
    [Route("api/cupos")]
    [ApiController]
    public class CupoController : ControllerBase
    {
        private readonly CupoService _cupoService;
        public CupoController(CupoService cupoService)
        {
            this._cupoService = cupoService;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Cupo cupo)
        {
            try
            {
                await _cupoService.Insertar(cupo);

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
                var cupo = await _cupoService.Buscar(id);

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
                var cupos = await _cupoService.ObtenerTodos();

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
                var cupoEncontrado = await _cupoService.Buscar(id);

                if (cupoEncontrado is null)
                    return RespuestasService.NotFound($"Cupo con ID = {id}, no encontrado.");

                await _cupoService.Actualizar(cupoEncontrado);

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
                var cupos = await _cupoService.ObtenerCuposDisponibles();

                return RespuestasService.Ok(cupos);
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
                var cupo = await _cupoService.Buscar(id);

                if (cupo is null)
                    return RespuestasService.NotFound($"Cupo con ID = {id}, no encontrado.");

                await _cupoService.Eliminar(cupo);

                return RespuestasService.NoContent();

            }catch(Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }
    }
}
