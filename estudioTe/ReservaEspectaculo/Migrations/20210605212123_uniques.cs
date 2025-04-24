using Microsoft.EntityFrameworkCore.Migrations;

namespace ReservaEspectaculo.Migrations
{
    public partial class uniques : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Personas_Legajo",
                table: "Personas",
                column: "Legajo",
                unique: true,
                filter: "[Legajo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Peliculas_Titulo",
                table: "Peliculas",
                column: "Titulo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Generos_Nombre",
                table: "Generos",
                column: "Nombre",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Personas_Legajo",
                table: "Personas");

            migrationBuilder.DropIndex(
                name: "IX_Peliculas_Titulo",
                table: "Peliculas");

            migrationBuilder.DropIndex(
                name: "IX_Generos_Nombre",
                table: "Generos");
        }
    }
}
