using Microsoft.EntityFrameworkCore;
using Parqueadero_Back.Models;
using Parqueadero_Back.Utilities;

namespace Parqueadero_Back.Database
{
    public class AppDbContext : DbContext
    {

        private readonly Utilidad utilidad;

        public AppDbContext(DbContextOptions<AppDbContext> opciones, Utilidad utilidad) : base(opciones)
        {
            this.utilidad = utilidad;
        }

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Rol> Rol { get; set; }

        public DbSet<Vehiculo> Vehiculo { get; set; }

        public DbSet<Reserva> Reserva { get; set; }

        public DbSet<Cupo> Cupo { get; set; }

        public DbSet<Soporte> Soporte { get; set; }

        public DbSet<TipoTransaccion> TipoTransaccion { get; set; }

        public DbSet<MetodoPago> MetodoPago { get; set; }

        public DbSet<Transaccion> Transaccion { get; set; }


        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);

            model.Entity<Transaccion>()
                .Property(transaccion => transaccion.Monto)
                .HasPrecision(18, 2);


            model.Entity<Reserva>()
                .ToTable(table => table.HasCheckConstraint("CK_Reserva_EstadoDescripcion", "[EstadoDescripcion] IN ('Activa', 'Finalizada', 'Cancelada')"));

            model.Entity<Cupo>()
                .ToTable(table => table.HasCheckConstraint("CH_Cupo_EstadoDescripcion", "[EstadoDescripcion] IN ('Reservado', 'Ocupado', 'Disponible')"));

            model.Entity<Rol>()
                .HasData(
                    new Rol { Id = 1, NombreRol = "Admin" },
                    new Rol { Id = 2, NombreRol = "Cliente" },
                    new Rol { Id = 3, NombreRol = "Operador"}
                );

            model.Entity<TipoTransaccion>()
                .HasData(
                    new TipoTransaccion { Id = 1, Nombre = "Ingreso" },
                    new TipoTransaccion { Id = 2, Nombre = "Egreso" }
                );

            model.Entity<MetodoPago>()
                .HasData(
                    new MetodoPago { Id = 1,  Nombre = "Efectivo" },
                    new MetodoPago { Id = 2, Nombre = "Tarjeta" },
                    new MetodoPago { Id = 3, Nombre = "Transferencia" }
                );

            model.Entity<Usuario>()
                .HasData(
                    new Usuario { Id = 1, NombreCompleto = "Camilo Andres Perez Parra", IdentificadorUsuario = "CamiloAPerezP", Contrasena = utilidad.Encriptar("camilo12345"), RolId = 2 },
                    new Usuario { Id = 2, NombreCompleto = "Jhoana Estefania Cifuentes", IdentificadorUsuario = "JhoanaECifuentes", Contrasena = utilidad.Encriptar("jhoana12345"), RolId = 2 },
                    new Usuario { Id = 3, NombreCompleto = "Nelson Andres Muñoz Salinas", IdentificadorUsuario = "NelsonAMunozS", Contrasena = utilidad.Encriptar("@Nelson11"), RolId = 1 },
                    new Usuario { Id = 4, NombreCompleto = "Silenid Salinas Sarmiento", IdentificadorUsuario = "SilenidSSarmiento", Contrasena = utilidad.Encriptar("silenid12345"), RolId = 2 },
                    new Usuario { Id = 5, NombreCompleto = "Daniela Fernanda Ramirez Ortiz", IdentificadorUsuario = "DanielaFRamirezO", Contrasena = utilidad.Encriptar("daniela12345"), RolId = 2 },
                    new Usuario { Id = 6, NombreCompleto = "Carlos Eduardo Torres Medina", IdentificadorUsuario = "CarlosETorresM", Contrasena = utilidad.Encriptar("carlos12345"), RolId = 2 },
                    new Usuario { Id = 7, NombreCompleto = "Laura Vanessa Pineda Gomez", IdentificadorUsuario = "LauraVPinedaG", Contrasena = utilidad.Encriptar("laura12345"), RolId = 2 },
                    new Usuario { Id = 8, NombreCompleto = "Juan Camilo Rodriguez Silva", IdentificadorUsuario = "JuanCRodriguezS", Contrasena = utilidad.Encriptar("juan12345"), RolId = 2 },
                    new Usuario { Id = 9, NombreCompleto = "Valentina Nicole Rojas Diaz", IdentificadorUsuario = "ValentinaNRojasD", Contrasena = utilidad.Encriptar("valentina12345"), RolId = 2 },
                    new Usuario { Id = 10, NombreCompleto = "David Alejandro Acosta Peña", IdentificadorUsuario = "DavidAAcostaP", Contrasena = utilidad.Encriptar("david12345"), RolId = 2 },
                    new Usuario { Id = 11, NombreCompleto = "Mariana Sofía Castillo Lara", IdentificadorUsuario = "MarianaSCastilloL", Contrasena = utilidad.Encriptar("mariana12345"), RolId = 2 },
                    new Usuario { Id = 12, NombreCompleto = "Sebastian Andres Vega Muñoz", IdentificadorUsuario = "SebastianAVegaM", Contrasena = utilidad.Encriptar("sebastian12345"), RolId = 2 },
                    new Usuario { Id = 13, NombreCompleto = "Nicolas Adrian Herrera Cruz", IdentificadorUsuario = "NicolasAHerreraC", Contrasena = utilidad.Encriptar("nicolas12345"), RolId = 2 },
                    new Usuario { Id = 14, NombreCompleto = "Paula Andrea Mendoza Ruiz", IdentificadorUsuario = "PaulaAMendozaR", Contrasena = utilidad.Encriptar("paula12345"), RolId = 2 }
                );

            model.Entity<Vehiculo>()
                .HasData(
                    new Vehiculo { Id = 1, Placa = "SBX-78F", UsuarioId = 1, Cilindraje = 125 },
                    new Vehiculo { Id = 2, Placa = "ERF-23G", UsuarioId = 2, Cilindraje = 200 },
                    new Vehiculo { Id = 3, Placa = "FGH-66H", UsuarioId = 4, Cilindraje = 400 },
                    new Vehiculo { Id = 4, Placa = "AAE-34F", UsuarioId = 1, Cilindraje = 125 },
                    new Vehiculo { Id = 5, Placa = "GJK-45B", UsuarioId = 2, Cilindraje = 200 },
                    new Vehiculo { Id = 6, Placa = "FCX-66B", UsuarioId = 1, Cilindraje = 250 },
                    new Vehiculo { Id = 7, Placa = "HPL-90C", UsuarioId = 5, Cilindraje = 125 },
                    new Vehiculo { Id = 8, Placa = "KSD-81A", UsuarioId = 6, Cilindraje = 250 },
                    new Vehiculo { Id = 9, Placa = "ZXC-22D", UsuarioId = 6, Cilindraje = 200 },
                    new Vehiculo { Id = 10, Placa = "RTV-14F", UsuarioId = 8, Cilindraje = 400 },
                    new Vehiculo { Id = 11, Placa = "POE-76B", UsuarioId = 8, Cilindraje = 125 },
                    new Vehiculo { Id = 12, Placa = "BMN-99E", UsuarioId = 8, Cilindraje = 250 },
                    new Vehiculo { Id = 13, Placa = "LMQ-45C", UsuarioId = 10, Cilindraje = 200 },
                    new Vehiculo { Id = 14, Placa = "QWE-33H", UsuarioId = 11, Cilindraje = 400 },
                    new Vehiculo { Id = 15, Placa = "PLM-50G", UsuarioId = 11, Cilindraje = 125 },
                    new Vehiculo { Id = 16, Placa = "VBN-07K", UsuarioId = 13, Cilindraje = 250 }

                );

            model.Entity<Cupo>()
                .HasData(
                    new Cupo { Id = 1, Estado = true, EstadoDescripcion = "Ocupado" },
                    new Cupo { Id = 2, Estado = true, EstadoDescripcion = "Ocupado" },
                    new Cupo { Id = 3, Estado = true, EstadoDescripcion = "Ocupado" },
                    new Cupo { Id = 4, Estado = false, EstadoDescripcion = "Disponible" },
                    new Cupo { Id = 5, Estado = false, EstadoDescripcion = "Disponible" },
                    new Cupo { Id = 6, Estado = false, EstadoDescripcion = "Disponible" },
                    new Cupo { Id = 7, Estado = true, EstadoDescripcion = "Ocupado" },
                    new Cupo { Id = 8, Estado = false, EstadoDescripcion = "Disponible" },
                    new Cupo { Id = 9, Estado = false, EstadoDescripcion = "Disponible" },
                    new Cupo { Id = 10, Estado = false, EstadoDescripcion = "Disponible" },
                    new Cupo { Id = 11, Estado = true, EstadoDescripcion = "Ocupado" },
                    new Cupo { Id = 12, Estado = true, EstadoDescripcion = "Ocupado" },
                    new Cupo { Id = 13, Estado = false, EstadoDescripcion = "Disponible" },
                    new Cupo { Id = 14, Estado = false, EstadoDescripcion = "Disponible" },
                    new Cupo { Id = 15, Estado = false, EstadoDescripcion = "Disponible" },
                    new Cupo { Id = 16, Estado = false, EstadoDescripcion = "Disponible" },
                    new Cupo { Id = 17, Estado = true, EstadoDescripcion = "Reservado" },
                    new Cupo { Id = 18, Estado = false, EstadoDescripcion = "Disponible" },
                    new Cupo { Id = 19, Estado = false, EstadoDescripcion = "Disponible" },
                    new Cupo { Id = 20, Estado = true, EstadoDescripcion = "Ocupado" }
                );


            model.Entity<Reserva>()
                .HasData(
                    new Reserva { Id = 1, VehiculoId = 1, CupoId = 1, FechaIngresoEstipulada = new DateTime(2025, 10, 10), FechaIngresoReal = new DateTime(2025, 10, 10),  FechaSalida = null, FechaReserva = new DateTime(2025, 10, 10), Duracion = null, Estado = true, EstadoDescripcion = "Activa" },
                    new Reserva { Id = 2, VehiculoId = 2, CupoId = 2, FechaIngresoEstipulada = new DateTime(2025, 10, 9), FechaIngresoReal = new DateTime(2025, 10, 9), FechaSalida = null, FechaReserva = new DateTime(2025, 10, 9), Duracion = null, Estado = true, EstadoDescripcion = "Activa" },
                    new Reserva { Id = 3, VehiculoId = 3, CupoId = 3, FechaIngresoEstipulada = new DateTime(2025, 10, 5), FechaIngresoReal = new DateTime(2025, 10, 5), FechaSalida = null, FechaReserva = new DateTime(2025, 10, 5), Duracion = null, Estado = true, EstadoDescripcion = "Activa" },
                    new Reserva { Id = 4, VehiculoId = 6, CupoId = 4, FechaIngresoEstipulada = new DateTime(2025, 10, 19), FechaIngresoReal = new DateTime(2025, 10, 19), FechaSalida = new DateTime(2025, 10, 20), FechaReserva = new DateTime(2025, 10, 18), Duracion = 24, Costo = 96000.00, Estado = true, EstadoDescripcion = "Finalizada"},
                    new Reserva { Id = 5, VehiculoId = 14, CupoId = 11, FechaIngresoEstipulada = new DateTime(2025, 11, 14), FechaIngresoReal = new DateTime(2025, 11, 15), FechaSalida = null, FechaReserva = new DateTime(2025, 10, 13), Duracion = null, Costo = null, Estado = true, EstadoDescripcion = "Activa" },
                    new Reserva { Id = 6, VehiculoId = 8, CupoId = 18, FechaIngresoEstipulada = new DateTime(2025, 10, 13), FechaIngresoReal = null, FechaSalida = null, FechaReserva = new DateTime(2025, 10, 12), Duracion = null, Costo = null, Estado = true, EstadoDescripcion = "Cancelada" },
                    new Reserva { Id = 7, VehiculoId = 5, CupoId = 9, FechaIngresoEstipulada = new DateTime(2025, 10, 9), FechaIngresoReal = new DateTime(2025, 10, 9), FechaSalida = new DateTime(2025, 10, 11), FechaReserva = new DateTime(2025, 10, 8), Duracion = 48, Costo = 168000.00, Estado = true, EstadoDescripcion = "Finalizada" },
                    new Reserva { Id = 8, VehiculoId = 16, CupoId = 20, FechaIngresoEstipulada = new DateTime(2025, 10, 6), FechaIngresoReal = new DateTime(2025, 10, 6), FechaSalida = null, FechaReserva = new DateTime(2025, 10, 5), Duracion = null, Costo = null, Estado = true, EstadoDescripcion = "Activa" },
                    new Reserva { Id = 9, VehiculoId = 11, CupoId = 13, FechaIngresoEstipulada = new DateTime(2025, 10, 11), FechaIngresoReal = new DateTime(2025, 10, 12), FechaSalida = new DateTime(2025, 10, 16), FechaReserva = new DateTime(2025, 10, 10), Duracion = 96, Costo = 336000.00, Estado = true, EstadoDescripcion = "Finalizada" },
                    new Reserva { Id = 10, VehiculoId = 3, CupoId = 7, FechaIngresoEstipulada = new DateTime(2025, 10, 4), FechaIngresoReal = new DateTime(2025, 10, 4), FechaSalida = null, FechaReserva = new DateTime(2025, 10, 3), Duracion = null, Costo = null, Estado = true, EstadoDescripcion = "Activa" },
                    new Reserva { Id = 11, VehiculoId = 9, CupoId = 17, FechaIngresoEstipulada = new DateTime(2025, 10, 16), FechaIngresoReal = new DateTime(2025, 10, 16), FechaSalida = null, FechaReserva = new DateTime(2025, 10, 15), Duracion = null, Costo = null, Estado = true, EstadoDescripcion = "Activa" },
                    new Reserva { Id = 12, VehiculoId = 10, CupoId = 15, FechaIngresoEstipulada = new DateTime(2025, 10, 3), FechaIngresoReal = new DateTime(2025, 10, 3), FechaSalida = new DateTime(2025, 10, 4), FechaReserva = new DateTime(2025, 10, 2), Duracion = 24, Costo = 96000.00, Estado = true, EstadoDescripcion = "Finalizada" },
                    new Reserva { Id = 13, VehiculoId = 4, CupoId = 12, FechaIngresoEstipulada = new DateTime(2025, 10, 18), FechaIngresoReal = new DateTime(2025, 10, 18), FechaSalida = null, FechaReserva = new DateTime(2025, 10, 17), Duracion = null, Costo = null, Estado = true, EstadoDescripcion = "Activa" },
                    new Reserva { Id = 14, VehiculoId = 15, CupoId = 19, FechaIngresoEstipulada = new DateTime(2025, 10, 1), FechaIngresoReal = new DateTime(2025, 10, 1), FechaSalida = new DateTime(2025, 10, 2), FechaReserva = new DateTime(2025, 9, 30), Duracion = 24, Costo = 180000.00, Estado = true, EstadoDescripcion = "Finalizada" }
                );
        }
    }
}
