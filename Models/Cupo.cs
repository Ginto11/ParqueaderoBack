namespace Parqueadero_Back.Models
{
    public class Cupo
    {
        public int Id {  get; set; }

        public bool Estado { get; set; } = false;

        public required string EstadoDescripcion { get; set; }

    }
}
