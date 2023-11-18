using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RccManager.Infra.Migrations
{
    public partial class AddedFormacoesServos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormacoesServos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    active = table.Column<bool>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    servoId = table.Column<Guid>(nullable: false),
                    formacaoId = table.Column<Guid>(nullable: false),
                    usuarioId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormacoesServos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormacoesServos_Formacoes_FormacaoId",
                        column: x => x.formacaoId,
                        principalTable: "Formacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormacoesServos_Servos_ServoId",
                        column: x => x.servoId,
                        principalTable: "Servos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormacoesServos_Users_UsuarioId",
                        column: x => x.usuarioId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormacoesServos_FormacaoId",
                table: "FormacoesServos",
                column: "FormacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_FormacoesServos_ServoId",
                table: "FormacoesServos",
                column: "ServoId");

            migrationBuilder.CreateIndex(
                name: "IX_FormacoesServos_UsuarioId",
                table: "FormacoesServos",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormacoesServos");
        }
    }
}
