using Microsoft.EntityFrameworkCore.Migrations;

namespace ReservaEspectaculo.Migrations
{
    public partial class descFuncion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Funciones");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Funciones",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
