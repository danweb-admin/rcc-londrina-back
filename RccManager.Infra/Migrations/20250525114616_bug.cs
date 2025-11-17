using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RccManager.Infra.Migrations
{
    public partial class bug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InscricoesEvento");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InscricoesEvento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    amount_paid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    cellphone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    cpf = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GrupoOracaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    registered = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    value = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
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
    }
}
