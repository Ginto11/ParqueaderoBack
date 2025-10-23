namespace Parqueadero_Back.Dtos
{
    public class UsuarioLogueadoDto
    {
        public int Id { get; set; }

        public required string NombreCompleto { get; set; }

        public required string IdentificadorUsuario { get; set; }

        public string? NumeroTelefono { get; set; }

        public required string Rol { get; set; }

        public required string Token { get; set; }
    }
}
