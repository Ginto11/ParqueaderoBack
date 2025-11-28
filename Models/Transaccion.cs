namespace Parqueadero_Back.Models
{
    public class Transaccion
    {
        public int Id { get; set; }

        public required DateTime FechaHora { get; set; }

        public required int TipoTransaccionId { get; set; }

        public TipoTransaccion? TipoTransaccion { get; set; }

        public required string Descripcion { get; set; }

        public required decimal Monto { get; set; }

        public required int MetodoPagoId { get; set; }
        
        public MetodoPago? MetodoPago { get; set; }

        public required int ReservaId { get; set; }

        public Reserva? Reserva { get; set; }
    }
}
