using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeterinariaRegistro.Migrations
{
    public partial class MigracionInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoMascota",
                columns: table => new
                {
                    IdTipo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreTipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMascota", x => x.IdTipo);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "RazaMascota",
                columns: table => new
                {
                    IdRaza = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreRaza = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTipoMascota = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RazaMascota", x => x.IdRaza);
                    table.ForeignKey(
                        name: "FK_RazaMascota_TipoMascota_IdTipoMascota",
                        column: x => x.IdTipoMascota,
                        principalTable: "TipoMascota",
                        principalColumn: "IdTipo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mascota",
                columns: table => new
                {
                    IdMascota = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreMascota = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombrePropietario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    estado = table.Column<bool>(type: "bit", nullable: false),
                    color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagenMascota = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdRazaMascota = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mascota", x => x.IdMascota);
                    table.ForeignKey(
                        name: "FK_Mascota_RazaMascota_IdRazaMascota",
                        column: x => x.IdRazaMascota,
                        principalTable: "RazaMascota",
                        principalColumn: "IdRaza",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TipoMascota",
                columns: new[] { "IdTipo", "NombreTipo" },
                values: new object[] { 1, "Perro" });

            migrationBuilder.InsertData(
                table: "TipoMascota",
                columns: new[] { "IdTipo", "NombreTipo" },
                values: new object[] { 2, "Gato" });

            migrationBuilder.InsertData(
                table: "TipoMascota",
                columns: new[] { "IdTipo", "NombreTipo" },
                values: new object[] { 3, "Pájaro" });

            migrationBuilder.InsertData(
                table: "RazaMascota",
                columns: new[] { "IdRaza", "IdTipoMascota", "NombreRaza" },
                values: new object[,]
                {
                    { 1, 1, "Labrador" },
                    { 2, 1, "Bulldog" },
                    { 3, 2, "Siamés" },
                    { 4, 2, "Persa" },
                    { 5, 3, "Canario" },
                    { 6, 3, "Periquito" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mascota_IdRazaMascota",
                table: "Mascota",
                column: "IdRazaMascota");

            migrationBuilder.CreateIndex(
                name: "IX_RazaMascota_IdTipoMascota",
                table: "RazaMascota",
                column: "IdTipoMascota");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mascota");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "RazaMascota");

            migrationBuilder.DropTable(
                name: "TipoMascota");
        }
    }
}
