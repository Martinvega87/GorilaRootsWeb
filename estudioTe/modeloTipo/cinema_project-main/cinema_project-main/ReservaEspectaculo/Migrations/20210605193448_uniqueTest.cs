using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReservaEspectaculo.Migrations
{
    public partial class uniqueTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Legajo",
                table: "Personas",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Salas_Numero",
                table: "Salas",
                column: "Numero",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Salas_Numero",
                table: "Salas");

            migrationBuilder.AlterColumn<Guid>(
                name: "Legajo",
                table: "Personas",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
