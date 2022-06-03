using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FatecFoodAPI.Migrations
{
    public partial class Entregue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Entregue",
                table: "Pedidos",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Entregue",
                table: "Pedidos");
        }
    }
}
