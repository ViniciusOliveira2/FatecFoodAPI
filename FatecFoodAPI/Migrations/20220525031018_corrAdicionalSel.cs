using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FatecFoodAPI.Migrations
{
    public partial class corrAdicionalSel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdicionaisSelecionados_Adicionais_AdicionalModelId",
                table: "AdicionaisSelecionados");

            migrationBuilder.DropIndex(
                name: "IX_AdicionaisSelecionados_AdicionalModelId",
                table: "AdicionaisSelecionados");

            migrationBuilder.DropColumn(
                name: "AdicionalModelId",
                table: "AdicionaisSelecionados");

            migrationBuilder.CreateIndex(
                name: "IX_AdicionaisSelecionados_AdicionalId",
                table: "AdicionaisSelecionados",
                column: "AdicionalId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdicionaisSelecionados_Adicionais_AdicionalId",
                table: "AdicionaisSelecionados",
                column: "AdicionalId",
                principalTable: "Adicionais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdicionaisSelecionados_Adicionais_AdicionalId",
                table: "AdicionaisSelecionados");

            migrationBuilder.DropIndex(
                name: "IX_AdicionaisSelecionados_AdicionalId",
                table: "AdicionaisSelecionados");

            migrationBuilder.AddColumn<int>(
                name: "AdicionalModelId",
                table: "AdicionaisSelecionados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AdicionaisSelecionados_AdicionalModelId",
                table: "AdicionaisSelecionados",
                column: "AdicionalModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdicionaisSelecionados_Adicionais_AdicionalModelId",
                table: "AdicionaisSelecionados",
                column: "AdicionalModelId",
                principalTable: "Adicionais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
