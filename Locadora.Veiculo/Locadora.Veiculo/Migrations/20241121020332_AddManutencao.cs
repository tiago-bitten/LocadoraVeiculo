using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Locadora.Veiculo.Migrations
{
    /// <inheritdoc />
    public partial class AddManutencao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Veiculos",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "'veiculo_' || abs(random() % 89999999 + 10000000)",
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Manutencao",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "'manutencao_' || abs(random() % 89999999 + 10000000)",
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "CodigoVeiculo",
                table: "Manutencao",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoVeiculo",
                table: "Manutencao");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Veiculos",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldDefaultValueSql: "'veiculo_' || abs(random() % 89999999 + 10000000)");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Manutencao",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldDefaultValueSql: "'manutencao_' || abs(random() % 89999999 + 10000000)");
        }
    }
}
