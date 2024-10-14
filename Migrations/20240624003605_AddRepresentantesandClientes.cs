using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.Web.alunosII.Migrations
{
    /// <inheritdoc />
    public partial class AddRepresentantesandClientes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tbl_Representante",
                columns: table => new
                {
                    RepresentanteId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NomeRepresentante = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Cpf = table.Column<string>(type: "NVARCHAR2(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Representante", x => x.RepresentanteId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Cliente",
                columns: table => new
                {
                    ClienteId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Sobrenome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "date", nullable: false),
                    Observacao = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: true),
                    RepresentanteId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Cliente", x => x.ClienteId);
                    table.ForeignKey(
                        name: "FK_Tbl_Cliente_Tbl_Representante_RepresentanteId",
                        column: x => x.RepresentanteId,
                        principalTable: "Tbl_Representante",
                        principalColumn: "RepresentanteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Cliente_RepresentanteId",
                table: "Tbl_Cliente",
                column: "RepresentanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Representante_Cpf",
                table: "Tbl_Representante",
                column: "Cpf",
                unique: true,
                filter: "\"Cpf\" IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_Cliente");

            migrationBuilder.DropTable(
                name: "Tbl_Representante");
        }
    }
}
