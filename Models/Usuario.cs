using System.Text.Json.Serialization;

namespace Parqueadero_Back.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public required string NombreCompleto { get; set; }

        public required string IdentificadorUsuario { get; set; }

        public string? NumeroTelefono { get; set; }

        public required string Contrasena { get; set; }

        public int? RolId { get; set; }

        public Rol? Rol { get; set; }


    }
}
