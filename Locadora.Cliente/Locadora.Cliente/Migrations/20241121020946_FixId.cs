using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Locadora.Cliente.Migrations
{
    /// <inheritdoc />
    public partial class FixId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Clientes",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "'cliente_' || abs(random() % 89999999 + 10000000)",
                oldClrType: typeof(string),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Clientes",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldDefaultValueSql: "'cliente_' || abs(random() % 89999999 + 10000000)");
        }
    }
}
