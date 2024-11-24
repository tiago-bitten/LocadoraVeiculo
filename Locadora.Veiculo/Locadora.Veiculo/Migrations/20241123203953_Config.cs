using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Locadora.Veiculo.Migrations
{
    /// <inheritdoc />
    public partial class Config : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Manutencoes_Veiculos_VeiculoId",
                table: "Manutencoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Veiculos",
                table: "Veiculos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Manutencoes",
                table: "Manutencoes");

            migrationBuilder.DropIndex(
                name: "IX_Manutencoes_VeiculoId",
                table: "Manutencoes");

            migrationBuilder.DropColumn(
                name: "VeiculoId",
                table: "Manutencoes");

            migrationBuilder.RenameTable(
                name: "Veiculos",
                newName: "Veiculo");

            migrationBuilder.RenameTable(
                name: "Manutencoes",
                newName: "Manutencao");

            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "Veiculo",
                newName: "tipo");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Veiculo",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Modelo",
                table: "Veiculo",
                newName: "modelo");

            migrationBuilder.RenameColumn(
                name: "Inativo",
                table: "Veiculo",
                newName: "inativo");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Veiculo",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "Veiculo",
                newName: "data_criacao");

            migrationBuilder.RenameColumn(
                name: "DataAlteracao",
                table: "Veiculo",
                newName: "data_alteracao");

            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "Manutencao",
                newName: "tipo");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Manutencao",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Inativo",
                table: "Manutencao",
                newName: "inativo");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Manutencao",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "DataInicio",
                table: "Manutencao",
                newName: "data_inicio");

            migrationBuilder.RenameColumn(
                name: "DataFinal",
                table: "Manutencao",
                newName: "data_final");

            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "Manutencao",
                newName: "data_criacao");

            migrationBuilder.RenameColumn(
                name: "DataAlteracao",
                table: "Manutencao",
                newName: "data_alteracao");

            migrationBuilder.RenameColumn(
                name: "CodigoVeiculo",
                table: "Manutencao",
                newName: "veiculo_id");

            migrationBuilder.AlterColumn<string>(
                name: "tipo",
                table: "Manutencao",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Veiculo",
                table: "Veiculo",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Manutencao",
                table: "Manutencao",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_Manutencao_veiculo_id",
                table: "Manutencao",
                column: "veiculo_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Manutencao_Veiculo_veiculo_id",
                table: "Manutencao",
                column: "veiculo_id",
                principalTable: "Veiculo",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Manutencao_Veiculo_veiculo_id",
                table: "Manutencao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Veiculo",
                table: "Veiculo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Manutencao",
                table: "Manutencao");

            migrationBuilder.DropIndex(
                name: "IX_Manutencao_veiculo_id",
                table: "Manutencao");

            migrationBuilder.RenameTable(
                name: "Veiculo",
                newName: "Veiculos");

            migrationBuilder.RenameTable(
                name: "Manutencao",
                newName: "Manutencoes");

            migrationBuilder.RenameColumn(
                name: "tipo",
                table: "Veiculos",
                newName: "Tipo");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Veiculos",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "modelo",
                table: "Veiculos",
                newName: "Modelo");

            migrationBuilder.RenameColumn(
                name: "inativo",
                table: "Veiculos",
                newName: "Inativo");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Veiculos",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "data_criacao",
                table: "Veiculos",
                newName: "DataCriacao");

            migrationBuilder.RenameColumn(
                name: "data_alteracao",
                table: "Veiculos",
                newName: "DataAlteracao");

            migrationBuilder.RenameColumn(
                name: "tipo",
                table: "Manutencoes",
                newName: "Tipo");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Manutencoes",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "inativo",
                table: "Manutencoes",
                newName: "Inativo");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Manutencoes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "veiculo_id",
                table: "Manutencoes",
                newName: "CodigoVeiculo");

            migrationBuilder.RenameColumn(
                name: "data_inicio",
                table: "Manutencoes",
                newName: "DataInicio");

            migrationBuilder.RenameColumn(
                name: "data_final",
                table: "Manutencoes",
                newName: "DataFinal");

            migrationBuilder.RenameColumn(
                name: "data_criacao",
                table: "Manutencoes",
                newName: "DataCriacao");

            migrationBuilder.RenameColumn(
                name: "data_alteracao",
                table: "Manutencoes",
                newName: "DataAlteracao");

            migrationBuilder.AlterColumn<int>(
                name: "Tipo",
                table: "Manutencoes",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "VeiculoId",
                table: "Manutencoes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Veiculos",
                table: "Veiculos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Manutencoes",
                table: "Manutencoes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Manutencoes_VeiculoId",
                table: "Manutencoes",
                column: "VeiculoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Manutencoes_Veiculos_VeiculoId",
                table: "Manutencoes",
                column: "VeiculoId",
                principalTable: "Veiculos",
                principalColumn: "Id");
        }
    }
}
