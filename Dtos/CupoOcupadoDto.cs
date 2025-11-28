using Parqueadero_Back.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parqueadero_Back.Dtos
{
    [NotMapped]
    public class CupoOcupadoDto
    {   
        public int? Id { get; set; }
        
        public string? Placa { get; set; }

        public int? Duracion { get; set; }

        public string? NombreUsuario { get; set; }

        public DateTime? FechaIngresoEstipulada { get; set; }

        public DateTime? FechaIngresoReal { get; set; }

        public decimal? Costo { get; set; }

        public string? EstadoDescripcion { get; set; }

    }
}
