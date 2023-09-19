using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RccManager.Infra.Migrations
{
    public partial class AddNewTableGrupoOracao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "bairro",
                table: "ParoquiasCapelas",
                newName: "neighborhood");

            migrationBuilder.RenameColumn(
                name: "cidade",
                table: "ParoquiasCapelas",
                newName: "city");

            migrationBuilder.RenameColumn(
                name: "endereco",
                table: "ParoquiasCapelas",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "ativo",
                table: "ParoquiasCapelas",
                newName: "active");

            migrationBuilder.AddColumn<string>(
                name: "zipCode",
                table: "ParoquiasCapelas",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GrupoOracoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    active = table.Column<bool>(nullable: false),
                    name = table.Column<string>(maxLength: 80, nullable: false),
                    paroquiaId = table.Column<Guid>(nullable: false),
                    type = table.Column<string>(maxLength: 15, nullable: false),
                    dayOfWeek = table.Column<string>(maxLength: 10, nullable: true),
                    local = table.Column<string>(maxLength: 50, nullable: true),
                    time = table.Column<DateTime>(nullable: false),
                    foundationDate = table.Column<DateTime>(nullable: false),
                    address = table.Column<string>(maxLength: 100, nullable: true),
                    neighborhood = table.Column<string>(maxLength: 50, nullable: true),
                    zipCode = table.Column<string>(maxLength: 9, nullable: true),
                    decanatoId = table.Column<Guid>(nullable: false),
                    city = table.Column<string>(maxLength: 50, nullable: true),
                    email = table.Column<string>(maxLength: 80, nullable: true),
                    site = table.Column<string>(maxLength: 50, nullable: true),
                    telephone = table.Column<string>(maxLength: 15, nullable: true),
                    numberOfParticipants = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoOracoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GrupoOracoes_Decanatos_decanatoId",
                        column: x => x.decanatoId,
                        principalTable: "Decanatos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GrupoOracoes_ParoquiasCapelas_paroquiaId",
                        column: x => x.paroquiaId,
                        principalTable: "ParoquiasCapelas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GrupoOracoes_decanatoId",
                table: "GrupoOracoes",
                column: "decanatoId");

            migrationBuilder.CreateIndex(
                name: "IX_GrupoOracoes_paroquiaId",
                table: "GrupoOracoes",
                column: "paroquiaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GrupoOracoes");

            migrationBuilder.DropColumn(
                name: "zipCode",
                table: "ParoquiasCapelas");

            migrationBuilder.RenameColumn(
                name: "neighborhood",
                table: "ParoquiasCapelas",
                newName: "bairro");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "ParoquiasCapelas",
                newName: "cidade");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "ParoquiasCapelas",
                newName: "endereco");

            migrationBuilder.RenameColumn(
                name: "active",
                table: "ParoquiasCapelas",
                newName: "ativo");
        }
    }
}
