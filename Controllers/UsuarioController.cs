using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parqueadero_Back.Dtos;
using Parqueadero_Back.Models;
using Parqueadero_Back.Services;
using Parqueadero_Back.Utilities;

namespace Parqueadero_Back.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService usuarioService;
        private readonly Utilidad utilidad;
        public UsuarioController(UsuarioService usuarioService, Utilidad utilidad)
        {
            this.usuarioService = usuarioService;
            this.utilidad = utilidad;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> Get()
        {
            try
            {
                var usuarios = await usuarioService.ObtenerTodos();

                return Ok(usuarios);
            }
            catch (Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<Usuario?>> Post(UsuarioLogin usuario)
        {
            try
            {
                var usuarioEncontrado = await usuarioService.BuscarPorUsuarioYContrasena(usuario.Identificador, usuario.Contrasena);

                if (usuarioEncontrado is null)
                    return RespuestasService.NotFound("No hay registro con esos datos.");

                return RespuestasService.Ok(usuarioEncontrado);

            }catch(Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Usuario>> Get(int id)
        {
            try
            {
                var usuario = await usuarioService.Buscar(id);

                if (usuario is null)
                    return RespuestasService.NotFound($"Usuario con ID = {id}, no encontrado.");

                return Ok(usuario);
            }catch(Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(Usuario usuario)
        {
            try
            {
                var identificadorExiste = await usuarioService.BuscarPorIdentificador(usuario.IdentificadorUsuario);

                if (identificadorExiste is not null)
                    return RespuestasService.Conflict($"El usuario ({usuario.IdentificadorUsuario}), ya existe. Utilice otro por favor.");


                usuario.RolId = 2;
                usuario.Contrasena = utilidad.Encriptar(usuario.Contrasena);

                await usuarioService.Insertar(usuario);

                return RespuestasService.Created();

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
                var usuario = await usuarioService.Buscar(id);

                if (usuario is null)
                    return NotFound();

                await usuarioService.Eliminar(usuario);

                return RespuestasService.NoContent();

            }catch(Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Put(int id, Usuario usuario)
        {
            try
            {
                var usuarioEncontrado = await usuarioService.Buscar(id);

                if (usuarioEncontrado is null)
                    return RespuestasService.NotFound($"Usuario con ID = {id}, no encontrado.");

                usuarioEncontrado.NombreCompleto = usuario.NombreCompleto;
                usuarioEncontrado.IdentificadorUsuario = usuario.IdentificadorUsuario;
                usuarioEncontrado.Contrasena = utilidad.Encriptar(usuario.Contrasena);
                usuarioEncontrado.NumeroTelefono = usuario.NumeroTelefono;
                usuarioEncontrado.RolId = usuario.RolId;

                await usuarioService.Actualizar(usuarioEncontrado);

                return RespuestasService.NoContent();

            }catch(Exception error)
            {
                return RespuestasService.ServerError(error.Message);
            }
        }
    }
}
