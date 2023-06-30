using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RccManager.Infra.Migrations
{
    public partial class AddedParoquiaCapela : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParoquiasCapelas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    ativo = table.Column<bool>(nullable: false),
                    endereco = table.Column<string>(maxLength: 100, nullable: false),
                    bairro = table.Column<string>(maxLength: 50, nullable: false),
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    decanatoId = table.Column<Guid>(nullable: false),
                    cidade = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParoquiasCapelas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParoquiasCapelas_Decanatos_decanatoId",
                        column: x => x.decanatoId,
                        principalTable: "Decanatos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParoquiasCapelas_decanatoId",
                table: "ParoquiasCapelas",
                column: "decanatoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParoquiasCapelas");
        }
    }
}
