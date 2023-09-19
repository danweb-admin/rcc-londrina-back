using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RccManager.Infra.Migrations
{
    public partial class ChangeTableGrupoOracao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GrupoOracoes_Decanatos_decanatoId",
                table: "GrupoOracoes");

            migrationBuilder.DropIndex(
                name: "IX_GrupoOracoes_decanatoId",
                table: "GrupoOracoes");

            migrationBuilder.DropColumn(
                name: "decanatoId",
                table: "GrupoOracoes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "decanatoId",
                table: "GrupoOracoes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_GrupoOracoes_decanatoId",
                table: "GrupoOracoes",
                column: "decanatoId");

            migrationBuilder.AddForeignKey(
                name: "FK_GrupoOracoes_Decanatos_decanatoId",
                table: "GrupoOracoes",
                column: "decanatoId",
                principalTable: "Decanatos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
