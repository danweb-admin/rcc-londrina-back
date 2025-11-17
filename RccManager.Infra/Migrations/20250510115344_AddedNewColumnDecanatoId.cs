using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RccManager.Infra.Migrations
{
    public partial class AddedNewColumnDecanatoId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventInfo",
                table: "Eventos",
                newName: "event_info");

            migrationBuilder.AddColumn<Guid>(
                name: "decanatoId",
                table: "Eventos",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DecanatoSetorId",
                table: "Eventos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_DecanatoSetorId",
                table: "Eventos",
                column: "DecanatoSetorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Eventos_Decanatos_DecanatoSetorId",
                table: "Eventos",
                column: "DecanatoSetorId",
                principalTable: "Decanatos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Eventos_Decanatos_DecanatoSetorId",
                table: "Eventos");

            migrationBuilder.DropIndex(
                name: "IX_Eventos_DecanatoSetorId",
                table: "Eventos");

            migrationBuilder.DropColumn(
                name: "decanatoId",
                table: "Eventos");

            migrationBuilder.DropColumn(
                name: "DecanatoSetorId",
                table: "Eventos");

            migrationBuilder.RenameColumn(
                name: "event_info",
                table: "Eventos",
                newName: "EventInfo");
        }
    }
}
