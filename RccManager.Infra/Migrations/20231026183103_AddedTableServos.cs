using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RccManager.Infra.Migrations
{
    public partial class AddedTableServos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Servos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    active = table.Column<bool>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    birthday = table.Column<DateTime>(nullable: false),
                    cpf = table.Column<string>(maxLength: 14, nullable: false),
                    email = table.Column<string>(maxLength: 80, nullable: false),
                    cellphone = table.Column<string>(maxLength: 15, nullable: false),
                    main_ministry = table.Column<string>(maxLength: 30, nullable: false),
                    secondary_ministry = table.Column<string>(maxLength: 30, nullable: true),
                    grupoOracaoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servos_GrupoOracoes_grupoOracaoId",
                        column: x => x.grupoOracaoId,
                        principalTable: "GrupoOracoes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Servos_grupoOracaoId",
                table: "Servos",
                column: "grupoOracaoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Servos");
        }
    }
}
