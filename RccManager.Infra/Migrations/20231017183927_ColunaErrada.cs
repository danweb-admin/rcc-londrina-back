using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RccManager.Infra.Migrations
{
    public partial class ColunaErrada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ParoquiasCapelas_ParoquiaCapelaId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ParoquiaCapelaId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ParoquiaCapelaId",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "GrupoOracaoId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_GrupoOracaoId",
                table: "Users",
                column: "GrupoOracaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_GrupoOracoes_GrupoOracaoId",
                table: "Users",
                column: "GrupoOracaoId",
                principalTable: "GrupoOracoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_GrupoOracoes_GrupoOracaoId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_GrupoOracaoId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GrupoOracaoId",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "DecanatoId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParoquiaCapelaId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParoquiaId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ParoquiaCapelaId",
                table: "Users",
                column: "ParoquiaCapelaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ParoquiasCapelas_ParoquiaCapelaId",
                table: "Users",
                column: "ParoquiaCapelaId",
                principalTable: "ParoquiasCapelas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
