﻿namespace Parqueadero_Back.Models
{
    public class Reserva
    {
        public int Id { get; set; }

        public int VehiculoId { get; set; }

        public int CupoId { get; set; }

        public DateTime FechaReserva { get; set; }
        
        public DateTime FechaIngreso { get; set; }

        public DateTime? FechaSalida { get; set; }

        public double? Costo { get; set; }

        public double? Duracion { get; set; }

        public bool Estado { get; set; }

        public Vehiculo? Vehiculo { get; set; }

        public Cupo? Cupo { get; set; }
    }
} 
