using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parqueadero_Back.Services;

namespace Parqueadero_Back.Controllers
{
    [Route("api/transacciones")]
    [ApiController]
    public class TransaccionController : ControllerBase
    {

        private readonly TransaccionService transaccionService;

        public TransaccionController(TransaccionService transaccionService)
        {
            this.transaccionService = transaccionService;
        }

        [HttpGet]
        public async Task<ActionResult> GetIngresosDiarios()
        {
            try
            {
                var total = await transaccionService.ObtenerIngresosDiarios();

                return RespuestasService.Ok(total);
            }catch(Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }
    }
}
