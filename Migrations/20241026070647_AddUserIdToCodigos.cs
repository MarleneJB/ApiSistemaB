using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaDIS.Migrations
{
    public partial class AddUserIdToCodigos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "codigo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "codigo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Monederos",
                columns: table => new
                {
                    MonederoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Saldo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaUltimaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monederos", x => x.MonederoId);
                });

            //migrationBuilder.CreateTable(
                //name: "Transacciones",
              //  columns: table => new
                //{
                  //  TransaccionId = table.Column<int>(type: "int", nullable: false)
                   //     .Annotation("SqlServer:Identity", "1, 1"),
                   // UserId = table.Column<int>(type: "int", nullable: false),
                   // Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    //TipoTransaccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    //Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                //},
                //constraints: table =>
               // {
                  //  table.PrimaryKey("PK_Transacciones", x => x.TransaccionId);
               // });

            migrationBuilder.CreateIndex(
                name: "IX_codigo_UserId",
                table: "codigo",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_codigo_users_UserId",
                table: "codigo",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_codigo_users_UserId",
                table: "codigo");

            migrationBuilder.DropTable(
                name: "Monederos");

            migrationBuilder.DropTable(
                name: "Precios");

            migrationBuilder.DropTable(
                name: "Transacciones");

            migrationBuilder.DropIndex(
                name: "IX_codigo_UserId",
                table: "codigo");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "codigo");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "codigo");
        }
    }
}
