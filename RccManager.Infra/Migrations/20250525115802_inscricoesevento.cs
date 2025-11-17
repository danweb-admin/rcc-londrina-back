using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RccManager.Infra.Migrations
{
    public partial class inscricoesevento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InscricoesEvento",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    active = table.Column<bool>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(maxLength: 200, nullable: false),
                    birthday = table.Column<DateTime>(nullable: false),
                    cpf = table.Column<string>(maxLength: 20, nullable: false),
                    email = table.Column<string>(maxLength: 200, nullable: false),
                    cellphone = table.Column<string>(maxLength: 100, nullable: false),
                    GrupoOracaoId = table.Column<Guid>(nullable: true),
                    EventId = table.Column<Guid>(nullable: false),
                    amount_paid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    registered = table.Column<string>(maxLength: 1, nullable: true),
                    status = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InscricoesEvento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InscricoesEvento_Eventos_EventId",
                        column: x => x.EventId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InscricoesEvento_GrupoOracoes_GrupoOracaoId",
                        column: x => x.GrupoOracaoId,
                        principalTable: "GrupoOracoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InscricoesEvento_EventId",
                table: "InscricoesEvento",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_InscricoesEvento_GrupoOracaoId",
                table: "InscricoesEvento",
                column: "GrupoOracaoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InscricoesEvento");
        }
    }
}
