using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarteiraAtivos.Migrations
{
    /// <inheritdoc />
    public partial class CriandoTabelaAuxiliarRedefinicaoSenhas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RedefinicoesSenha",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiraEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Utilizado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RedefinicoesSenha", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RedefinicoesSenha");
        }
    }
}
