using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RccManager.Infra.Migrations
{
    public partial class ParoquiaIdAndDecanatoIdUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<Guid>(
                name: "DecanatoSetorId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParoquiaCapelaId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_DecanatoSetorId",
                table: "Users",
                column: "DecanatoSetorId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ParoquiaCapelaId",
                table: "Users",
                column: "ParoquiaCapelaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Decanatos_DecanatoSetorId",
                table: "Users",
                column: "DecanatoSetorId",
                principalTable: "Decanatos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ParoquiasCapelas_ParoquiaCapelaId",
                table: "Users",
                column: "ParoquiaCapelaId",
                principalTable: "ParoquiasCapelas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Decanatos_DecanatoSetorId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_ParoquiasCapelas_ParoquiaCapelaId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_DecanatoSetorId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ParoquiaCapelaId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DecanatoId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DecanatoSetorId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ParoquiaCapelaId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ParoquiaId",
                table: "Users");
        }
    }
}
