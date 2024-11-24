using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Locadora.Cliente.Migrations
{
    /// <inheritdoc />
    public partial class configs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Telefone",
                table: "Clientes",
                newName: "telefone");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Clientes",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "Inativo",
                table: "Clientes",
                newName: "inativo");

            migrationBuilder.RenameColumn(
                name: "Endereco",
                table: "Clientes",
                newName: "endereco");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Clientes",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Cpf",
                table: "Clientes",
                newName: "cpf");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Clientes",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "DataNascimento",
                table: "Clientes",
                newName: "data_nascimento");

            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "Clientes",
                newName: "data_criacao");

            migrationBuilder.RenameColumn(
                name: "DataAlteracao",
                table: "Clientes",
                newName: "data_alteracao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "telefone",
                table: "Clientes",
                newName: "Telefone");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "Clientes",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "inativo",
                table: "Clientes",
                newName: "Inativo");

            migrationBuilder.RenameColumn(
                name: "endereco",
                table: "Clientes",
                newName: "Endereco");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Clientes",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "cpf",
                table: "Clientes",
                newName: "Cpf");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Clientes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "data_nascimento",
                table: "Clientes",
                newName: "DataNascimento");

            migrationBuilder.RenameColumn(
                name: "data_criacao",
                table: "Clientes",
                newName: "DataCriacao");

            migrationBuilder.RenameColumn(
                name: "data_alteracao",
                table: "Clientes",
                newName: "DataAlteracao");
        }
    }
}
