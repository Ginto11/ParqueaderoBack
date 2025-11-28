using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Parqueadero_Back.Database.Migrations
{
    /// <inheritdoc />
    public partial class CreacionInicialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cupo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    EstadoDescripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cupo", x => x.Id);
                    table.CheckConstraint("CH_Cupo_EstadoDescripcion", "[EstadoDescripcion] IN ('Reservado', 'Ocupado', 'Disponible')");
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreRol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdentificadorUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroTelefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contrasena = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Rol_RolId",
                        column: x => x.RolId,
                        principalTable: "Rol",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Soporte",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Asunto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Soporte", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Soporte_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vehiculo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Placa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Cilindraje = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehiculo_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reserva",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehiculoId = table.Column<int>(type: "int", nullable: false),
                    CupoId = table.Column<int>(type: "int", nullable: false),
                    FechaReserva = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaIngresoEstipulada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaIngresoReal = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaSalida = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Costo = table.Column<double>(type: "float", nullable: true),
                    Duracion = table.Column<double>(type: "float", nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    EstadoDescripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserva", x => x.Id);
                    table.CheckConstraint("CK_Reserva_EstadoDescripcion", "[EstadoDescripcion] IN ('Activa', 'Finalizada', 'Cancelada')");
                    table.ForeignKey(
                        name: "FK_Reserva_Cupo_CupoId",
                        column: x => x.CupoId,
                        principalTable: "Cupo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reserva_Vehiculo_VehiculoId",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Cupo",
                columns: new[] { "Id", "Estado", "EstadoDescripcion" },
                values: new object[,]
                {
                    { 1, true, "Ocupado" },
                    { 2, true, "Ocupado" },
                    { 3, true, "Ocupado" },
                    { 4, false, "Disponible" },
                    { 5, false, "Disponible" },
                    { 6, false, "Disponible" },
                    { 7, true, "Ocupado" },
                    { 8, false, "Disponible" },
                    { 9, false, "Disponible" },
                    { 10, false, "Disponible" },
                    { 11, true, "Ocupado" },
                    { 12, true, "Ocupado" },
                    { 13, false, "Disponible" },
                    { 14, false, "Disponible" },
                    { 15, false, "Disponible" },
                    { 16, false, "Disponible" },
                    { 17, true, "Reservado" },
                    { 18, false, "Disponible" },
                    { 19, false, "Disponible" },
                    { 20, true, "Ocupado" }
                });

            migrationBuilder.InsertData(
                table: "Rol",
                columns: new[] { "Id", "NombreRol" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Cliente" },
                    { 3, "Operador" }
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "Contrasena", "IdentificadorUsuario", "NombreCompleto", "NumeroTelefono", "RolId" },
                values: new object[,]
                {
                    { 1, "Ow4CnX0NoSHlhq96QYGPFQ==", "CamiloAPerezP", "Camilo Andres Perez Parra", null, 2 },
                    { 2, "eSuwoPwEaN2wXV/NfQt/lA==", "JhoanaECifuentes", "Jhoana Estefania Cifuentes", null, 2 },
                    { 3, "UsPN3lexcl9LyHlIHuw6ew==", "NelsonAMunozS", "Nelson Andres Muñoz Salinas", null, 1 },
                    { 4, "ouj96EN+PovtRy940zJAIA==", "SilenidSSarmiento", "Silenid Salinas Sarmiento", null, 2 },
                    { 5, "FhqsQlGPwXDJgjpPBJ9mHA==", "DanielaFRamirezO", "Daniela Fernanda Ramirez Ortiz", null, 2 },
                    { 6, "S7EAxI4LweQlUKawN7+yMw==", "CarlosETorresM", "Carlos Eduardo Torres Medina", null, 2 },
                    { 7, "BqcM1SAYio4g4wx94cGD8A==", "LauraVPinedaG", "Laura Vanessa Pineda Gomez", null, 2 },
                    { 8, "Ura9j/soWm9XtY6aZeh+mg==", "JuanCRodriguezS", "Juan Camilo Rodriguez Silva", null, 2 },
                    { 9, "/wIwgBJC+x66mtAn9Rtqcg==", "ValentinaNRojasD", "Valentina Nicole Rojas Diaz", null, 2 },
                    { 10, "qRPJ1XEsRadus8k762LyPw==", "DavidAAcostaP", "David Alejandro Acosta Peña", null, 2 },
                    { 11, "BFMmtRWsaydyTpPSN+iRHg==", "MarianaSCastilloL", "Mariana Sofía Castillo Lara", null, 2 },
                    { 12, "iadC0NZMHKxjZX5ekTEOXw==", "SebastianAVegaM", "Sebastian Andres Vega Muñoz", null, 2 },
                    { 13, "zp933pVbM1vUX/UIE+jCEg==", "NicolasAHerreraC", "Nicolas Adrian Herrera Cruz", null, 2 },
                    { 14, "ojD4mP/JBovXB46YzCEIlg==", "PaulaAMendozaR", "Paula Andrea Mendoza Ruiz", null, 2 }
                });

            migrationBuilder.InsertData(
                table: "Vehiculo",
                columns: new[] { "Id", "Cilindraje", "Placa", "UsuarioId" },
                values: new object[,]
                {
                    { 1, 125, "SBX-78F", 1 },
                    { 2, 200, "ERF-23G", 2 },
                    { 3, 400, "FGH-66H", 4 },
                    { 4, 125, "AAE-34F", 1 },
                    { 5, 200, "GJK-45B", 2 },
                    { 6, 250, "FCX-66B", 1 },
                    { 7, 125, "HPL-90C", 5 },
                    { 8, 250, "KSD-81A", 6 },
                    { 9, 200, "ZXC-22D", 6 },
                    { 10, 400, "RTV-14F", 8 },
                    { 11, 125, "POE-76B", 8 },
                    { 12, 250, "BMN-99E", 8 },
                    { 13, 200, "LMQ-45C", 10 },
                    { 14, 400, "QWE-33H", 11 },
                    { 15, 125, "PLM-50G", 11 },
                    { 16, 250, "VBN-07K", 13 }
                });

            migrationBuilder.InsertData(
                table: "Reserva",
                columns: new[] { "Id", "Costo", "CupoId", "Duracion", "Estado", "EstadoDescripcion", "FechaIngresoEstipulada", "FechaIngresoReal", "FechaReserva", "FechaSalida", "VehiculoId" },
                values: new object[,]
                {
                    { 1, null, 1, null, true, "Activa", new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { 2, null, 2, null, true, "Activa", new DateTime(2025, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { 3, null, 3, null, true, "Activa", new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3 },
                    { 4, 96000.0, 4, 24.0, true, "Finalizada", new DateTime(2025, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 },
                    { 5, null, 11, null, true, "Activa", new DateTime(2025, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 14 },
                    { 6, null, 18, null, true, "Cancelada", new DateTime(2025, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 8 },
                    { 7, 168000.0, 9, 48.0, true, "Finalizada", new DateTime(2025, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 8, null, 20, null, true, "Activa", new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 16 },
                    { 9, 336000.0, 13, 96.0, true, "Finalizada", new DateTime(2025, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 11 },
                    { 10, null, 7, null, true, "Activa", new DateTime(2025, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3 },
                    { 11, null, 17, null, true, "Activa", new DateTime(2025, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 9 },
                    { 12, 96000.0, 15, 24.0, true, "Finalizada", new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 10 },
                    { 13, null, 12, null, true, "Activa", new DateTime(2025, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 4 },
                    { 14, 180000.0, 19, 24.0, true, "Finalizada", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 15 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_CupoId",
                table: "Reserva",
                column: "CupoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_VehiculoId",
                table: "Reserva",
                column: "VehiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Soporte_UsuarioId",
                table: "Soporte",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_RolId",
                table: "Usuario",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculo_UsuarioId",
                table: "Vehiculo",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reserva");

            migrationBuilder.DropTable(
                name: "Soporte");

            migrationBuilder.DropTable(
                name: "Cupo");

            migrationBuilder.DropTable(
                name: "Vehiculo");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Rol");
        }
    }
}
