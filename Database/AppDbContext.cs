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

        public DbSet<Soporte> Soporte { get; set;}

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);

            model.Entity<Rol>()
                .HasData(
                    new Rol { Id = 1, NombreRol = "Admin" },
                    new Rol { Id = 2, NombreRol = "Cliente" },
                    new Rol { Id = 3, NombreRol = "Operador"}
                );

            model.Entity<Usuario>()
                .HasData(
                    new Usuario { Id = 1, NombreCompleto = "Camilo Andres Perez Parra", IdentificadorUsuario = "CamiloAPerezP", Contrasena = utilidad.Encriptar("camilo12345"), RolId = 2 },
                    new Usuario { Id = 2, NombreCompleto = "Jhoana Estefania Cifuentes", IdentificadorUsuario = "JhoanaECifuentes", Contrasena = utilidad.Encriptar("jhoana12345"), RolId = 2 },
                    new Usuario { Id = 3, NombreCompleto = "Nelson Andres Muñoz Salinas", IdentificadorUsuario = "NelsonAMunozS", Contrasena = utilidad.Encriptar("@Nelson11"), RolId = 1 },
                    new Usuario { Id = 4, NombreCompleto = "Silenid Salinas Sarmiento", IdentificadorUsuario = "SilenidSSarmiento", Contrasena = utilidad.Encriptar("silenid12345"), RolId = 2 }

                );

            model.Entity<Vehiculo>()
                .HasData(
                    new Vehiculo { Id = 1, Placa = "SBX-78F", UsuarioId = 1, Cilindraje = 125 },
                    new Vehiculo { Id = 2, Placa = "ERF-23G", UsuarioId = 2, Cilindraje = 200 },
                    new Vehiculo { Id = 3, Placa = "FGH-66H", UsuarioId = 4, Cilindraje = 400 },
                    new Vehiculo { Id = 4, Placa = "AAE-34F", UsuarioId = 1, Cilindraje = 125 },
                    new Vehiculo { Id = 5, Placa = "GJK-45B", UsuarioId = 2, Cilindraje = 200 },
                    new Vehiculo { Id = 6, Placa = "FCX-66B", UsuarioId = 1, Cilindraje = 250 }

                );

            model.Entity<Cupo>()
                .HasData(
                    new Cupo { Id = 1, Estado = true },
                    new Cupo { Id = 2, Estado = true },
                    new Cupo { Id = 3, Estado = true },
                    new Cupo { Id = 4, Estado = false },
                    new Cupo { Id = 5, Estado = false },
                    new Cupo { Id = 6, Estado = false },
                    new Cupo { Id = 7, Estado = false },
                    new Cupo { Id = 8, Estado = false },
                    new Cupo { Id = 9, Estado = false },
                    new Cupo { Id = 10, Estado = false },
                    new Cupo { Id = 11, Estado = false },
                    new Cupo { Id = 12, Estado = false },
                    new Cupo { Id = 13, Estado = false },
                    new Cupo { Id = 14, Estado = false },
                    new Cupo { Id = 15, Estado = false }
                );


            model.Entity<Reserva>()
                .HasData(
                    new Reserva { Id = 1, VehiculoId = 1, CupoId = 1, FechaIngreso = new DateTime(2025, 10, 10), FechaSalida = null, FechaReserva = new DateTime(2025, 10, 10), Duracion = null, Estado = true },
                    new Reserva { Id = 2, VehiculoId = 2, CupoId = 2, FechaIngreso = new DateTime(2025, 10, 9), FechaSalida = null, FechaReserva = new DateTime(2025, 10, 9), Duracion = null, Estado = true },
                    new Reserva { Id = 3, VehiculoId = 3, CupoId = 3, FechaIngreso = new DateTime(2025, 10, 5), FechaSalida = null, FechaReserva = new DateTime(2025, 10, 5), Duracion = null, Estado = true },
                    new Reserva { Id = 4, VehiculoId = 6, CupoId = 4, FechaIngreso = new DateTime(2025, 10, 19), FechaSalida = new DateTime(2025, 10, 20), FechaReserva = new DateTime(2025, 10, 18), Duracion = 24, Costo = 96000.00, Estado = true}
                );
        }
    }
}
