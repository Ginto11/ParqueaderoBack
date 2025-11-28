namespace Parqueadero_Back.Dtos
{
    public class PostReserva
    {
        public int VehiculoId { get; set; }

        public int CupoId { get; set; }

        public DateTime FechaIngresoEstipulada { get; set; }
    }
}
