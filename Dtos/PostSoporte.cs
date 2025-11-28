namespace Parqueadero_Back.Dtos
{
    public class PostSoporte
    {
        public required string Asunto { get; set; }
        public required string Descripcion { get; set; }

        public int UsuarioId { get; set; }
    }
}
