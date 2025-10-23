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
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cupo", x => x.Id);
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
                    FechaIngreso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaSalida = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Costo = table.Column<double>(type: "float", nullable: true),
                    Duracion = table.Column<double>(type: "float", nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserva", x => x.Id);
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
                columns: new[] { "Id", "Estado" },
                values: new object[,]
                {
                    { 1, true },
                    { 2, true },
                    { 3, true },
                    { 4, false },
                    { 5, false },
                    { 6, false },
                    { 7, false },
                    { 8, false },
                    { 9, false },
                    { 10, false },
                    { 11, false },
                    { 12, false },
                    { 13, false },
                    { 14, false },
                    { 15, false }
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
                    { 4, "ouj96EN+PovtRy940zJAIA==", "SilenidSSarmiento", "Silenid Salinas Sarmiento", null, 2 }
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
                    { 6, 250, "FCX-66B", 1 }
                });

            migrationBuilder.InsertData(
                table: "Reserva",
                columns: new[] { "Id", "Costo", "CupoId", "Duracion", "Estado", "FechaIngreso", "FechaReserva", "FechaSalida", "VehiculoId" },
                values: new object[,]
                {
                    { 1, null, 1, null, true, new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { 2, null, 2, null, true, new DateTime(2025, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { 3, null, 3, null, true, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3 },
                    { 4, 96000.0, 4, 24.0, true, new DateTime(2025, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 }
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
