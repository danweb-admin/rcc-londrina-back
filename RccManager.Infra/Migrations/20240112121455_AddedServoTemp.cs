using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RccManager.Infra.Migrations
{
    public partial class AddedServoTemp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServosTemp",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    active = table.Column<bool>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(maxLength: 200, nullable: false),
                    birthday = table.Column<DateTime>(nullable: false),
                    cpf = table.Column<string>(maxLength: 100, nullable: false),
                    email = table.Column<string>(maxLength: 200, nullable: false),
                    cellphone = table.Column<string>(maxLength: 100, nullable: false),
                    main_ministry = table.Column<string>(maxLength: 30, nullable: false),
                    secondary_ministry = table.Column<string>(maxLength: 30, nullable: true),
                    @checked = table.Column<bool>(name: "checked", nullable: false),
                    grupoOracaoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServosTemp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServosTemp_GrupoOracoes_grupoOracaoId",
                        column: x => x.grupoOracaoId,
                        principalTable: "GrupoOracoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServosTemp_grupoOracaoId",
                table: "ServosTemp",
                column: "grupoOracaoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServosTemp");
        }
    }
}
