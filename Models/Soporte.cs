namespace Parqueadero_Back.Models
{
    public class Soporte
    {
        public int Id { get; set; }
        public required string Asunto { get; set; }

        public required string Descripcion { get; set; }

        public int UsuarioId { get; set; }

        public Usuario? Usuario { get; set; }
    }
}
