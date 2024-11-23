using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Locadora.Aluguel.Migrations
{
    /// <inheritdoc />
    public partial class configs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Aluguels_CodigoCliente",
                table: "Aluguels");

            migrationBuilder.DropIndex(
                name: "IX_Aluguels_CodigoVeiculo",
                table: "Aluguels");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Aluguels",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Inativo",
                table: "Aluguels",
                newName: "inativo");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Aluguels",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ValorTotal",
                table: "Aluguels",
                newName: "valor_total");

            migrationBuilder.RenameColumn(
                name: "DataInicio",
                table: "Aluguels",
                newName: "data_inicio");

            migrationBuilder.RenameColumn(
                name: "DataFinal",
                table: "Aluguels",
                newName: "data_final");

            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "Aluguels",
                newName: "data_criacao");

            migrationBuilder.RenameColumn(
                name: "DataAlteracao",
                table: "Aluguels",
                newName: "data_alteracao");

            migrationBuilder.RenameColumn(
                name: "CodigoVeiculo",
                table: "Aluguels",
                newName: "veiculo_id");

            migrationBuilder.RenameColumn(
                name: "CodigoCliente",
                table: "Aluguels",
                newName: "cliente_id");

            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "Aluguels",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "status",
                table: "Aluguels",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "inativo",
                table: "Aluguels",
                newName: "Inativo");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Aluguels",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "veiculo_id",
                table: "Aluguels",
                newName: "CodigoVeiculo");

            migrationBuilder.RenameColumn(
                name: "valor_total",
                table: "Aluguels",
                newName: "ValorTotal");

            migrationBuilder.RenameColumn(
                name: "data_inicio",
                table: "Aluguels",
                newName: "DataInicio");

            migrationBuilder.RenameColumn(
                name: "data_final",
                table: "Aluguels",
                newName: "DataFinal");

            migrationBuilder.RenameColumn(
                name: "data_criacao",
                table: "Aluguels",
                newName: "DataCriacao");

            migrationBuilder.RenameColumn(
                name: "data_alteracao",
                table: "Aluguels",
                newName: "DataAlteracao");

            migrationBuilder.RenameColumn(
                name: "cliente_id",
                table: "Aluguels",
                newName: "CodigoCliente");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Aluguels",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_Aluguels_CodigoCliente",
                table: "Aluguels",
                column: "CodigoCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Aluguels_CodigoVeiculo",
                table: "Aluguels",
                column: "CodigoVeiculo");
        }
    }
}
