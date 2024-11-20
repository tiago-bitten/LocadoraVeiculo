using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Locadora.Aluguel.Migrations
{
    /// <inheritdoc />
    public partial class AjusteIdentificador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Aluguels",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Aluguels_CodigoCliente",
                table: "Aluguels",
                column: "CodigoCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Aluguels_CodigoVeiculo",
                table: "Aluguels",
                column: "CodigoVeiculo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Aluguels_CodigoCliente",
                table: "Aluguels");

            migrationBuilder.DropIndex(
                name: "IX_Aluguels_CodigoVeiculo",
                table: "Aluguels");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Aluguels");
        }
    }
}
