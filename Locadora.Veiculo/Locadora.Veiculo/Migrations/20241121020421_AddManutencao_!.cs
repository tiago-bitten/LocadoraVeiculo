using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Locadora.Veiculo.Migrations
{
    /// <inheritdoc />
    public partial class AddManutencao_ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Manutencao_Veiculos_VeiculoId",
                table: "Manutencao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Manutencao",
                table: "Manutencao");

            migrationBuilder.RenameTable(
                name: "Manutencao",
                newName: "Manutencoes");

            migrationBuilder.RenameIndex(
                name: "IX_Manutencao_VeiculoId",
                table: "Manutencoes",
                newName: "IX_Manutencoes_VeiculoId");

            migrationBuilder.AlterColumn<string>(
                name: "VeiculoId",
                table: "Manutencoes",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Manutencoes",
                table: "Manutencoes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Manutencoes_Veiculos_VeiculoId",
                table: "Manutencoes",
                column: "VeiculoId",
                principalTable: "Veiculos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Manutencoes_Veiculos_VeiculoId",
                table: "Manutencoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Manutencoes",
                table: "Manutencoes");

            migrationBuilder.RenameTable(
                name: "Manutencoes",
                newName: "Manutencao");

            migrationBuilder.RenameIndex(
                name: "IX_Manutencoes_VeiculoId",
                table: "Manutencao",
                newName: "IX_Manutencao_VeiculoId");

            migrationBuilder.AlterColumn<string>(
                name: "VeiculoId",
                table: "Manutencao",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Manutencao",
                table: "Manutencao",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Manutencao_Veiculos_VeiculoId",
                table: "Manutencao",
                column: "VeiculoId",
                principalTable: "Veiculos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
