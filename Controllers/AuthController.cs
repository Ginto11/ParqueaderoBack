using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parqueadero_Back.Dtos;
using Parqueadero_Back.Services;

namespace Parqueadero_Back.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService authService;
        private readonly UsuarioService usuarioService;
        public AuthController(UsuarioService usuarioService, AuthService authService)
        {
            this.authService = authService;
            this.usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<ActionResult> Login(UsuarioLogin usuario)
        {
            try
            {
                var usuarioEncontrado = await usuarioService.BuscarPorUsuarioYContrasena(usuario.Identificador, usuario.Contrasena);

                if (usuarioEncontrado is null)
                    return RespuestasService.InvalidCredentials("Credenciales Incorrectas");

                var token = authService.GenerarToken(usuarioEncontrado);

                return RespuestasService.LoginExitoso(usuarioEncontrado, token);
            }
            catch (Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }
    }
}
