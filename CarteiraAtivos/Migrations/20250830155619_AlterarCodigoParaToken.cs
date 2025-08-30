using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarteiraAtivos.Migrations
{
    /// <inheritdoc />
    public partial class AlterarCodigoParaToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Codigo",
                table: "RedefinicoesSenha",
                newName: "Token");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Token",
                table: "RedefinicoesSenha",
                newName: "Codigo");
        }
    }
}
