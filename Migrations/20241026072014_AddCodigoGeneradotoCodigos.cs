using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaDIS.Migrations
{
    public partial class AddCodigoGeneradotoCodigos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodigoGenerado",
                table: "codigo",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoGenerado",
                table: "codigo");
        }
    }
}
