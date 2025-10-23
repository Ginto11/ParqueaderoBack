using Microsoft.AspNetCore.Mvc;
using Parqueadero_Back.Models;
using Parqueadero_Back.Services;

namespace Parqueadero_Back.Controllers
{
    [ApiController]
    [Route("api/soportes")]
    public class SoporteController
    {
        private readonly SoporteService soporteService;

        public SoporteController(SoporteService soporteService)
        {
            this.soporteService = soporteService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var soportes = await soporteService.ObtenerTodos();

                return RespuestasService.Ok(soportes);
            }catch(Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(Soporte soporte)
        {
            try
            {

                await soporteService.Insertar(soporte);

                return RespuestasService.Created();

            }catch(Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }
    }
}
