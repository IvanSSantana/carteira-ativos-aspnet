using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarteiraAtivos.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoAtivoRelacionamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoginUsuarioId",
                table: "Ativos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ativos_LoginUsuarioId",
                table: "Ativos",
                column: "LoginUsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ativos_LoginUsuarios_LoginUsuarioId",
                table: "Ativos",
                column: "LoginUsuarioId",
                principalTable: "LoginUsuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ativos_LoginUsuarios_LoginUsuarioId",
                table: "Ativos");

            migrationBuilder.DropIndex(
                name: "IX_Ativos_LoginUsuarioId",
                table: "Ativos");

            migrationBuilder.DropColumn(
                name: "LoginUsuarioId",
                table: "Ativos");
        }
    }
}
