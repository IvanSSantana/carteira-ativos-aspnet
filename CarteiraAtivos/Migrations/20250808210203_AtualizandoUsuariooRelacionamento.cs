using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarteiraAtivos.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoUsuariooRelacionamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ativos_LoginUsuarios_LoginUsuarioId",
                table: "Ativos");

            migrationBuilder.AlterColumn<int>(
                name: "LoginUsuarioId",
                table: "Ativos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ativos_LoginUsuarios_LoginUsuarioId",
                table: "Ativos",
                column: "LoginUsuarioId",
                principalTable: "LoginUsuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ativos_LoginUsuarios_LoginUsuarioId",
                table: "Ativos");

            migrationBuilder.AlterColumn<int>(
                name: "LoginUsuarioId",
                table: "Ativos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Ativos_LoginUsuarios_LoginUsuarioId",
                table: "Ativos",
                column: "LoginUsuarioId",
                principalTable: "LoginUsuarios",
                principalColumn: "Id");
        }
    }
}
