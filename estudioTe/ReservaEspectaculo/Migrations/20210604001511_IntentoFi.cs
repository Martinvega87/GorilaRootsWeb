using Microsoft.EntityFrameworkCore.Migrations;

namespace ReservaEspectaculo.Migrations
{
    public partial class IntentoFi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ButacasDisponibles",
                table: "Funciones");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ButacasDisponibles",
                table: "Funciones",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
