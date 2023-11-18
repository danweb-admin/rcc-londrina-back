using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RccManager.Infra.Migrations
{
    public partial class AddedNewColumnCertificateDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormacoesServos_Formacoes_FormacaoId",
                table: "FormacoesServos");

            migrationBuilder.DropForeignKey(
                name: "FK_FormacoesServos_Servos_ServoId",
                table: "FormacoesServos");

            migrationBuilder.DropForeignKey(
                name: "FK_FormacoesServos_Users_UsuarioId",
                table: "FormacoesServos");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "FormacoesServos",
                newName: "usuarioId");

            migrationBuilder.RenameColumn(
                name: "ServoId",
                table: "FormacoesServos",
                newName: "servoId");

            migrationBuilder.RenameColumn(
                name: "FormacaoId",
                table: "FormacoesServos",
                newName: "formacaoId");

            migrationBuilder.RenameIndex(
                name: "IX_FormacoesServos_UsuarioId",
                table: "FormacoesServos",
                newName: "IX_FormacoesServos_usuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_FormacoesServos_ServoId",
                table: "FormacoesServos",
                newName: "IX_FormacoesServos_servoId");

            migrationBuilder.RenameIndex(
                name: "IX_FormacoesServos_FormacaoId",
                table: "FormacoesServos",
                newName: "IX_FormacoesServos_formacaoId");

            migrationBuilder.AddColumn<DateTime>(
                name: "certificateDate",
                table: "FormacoesServos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_FormacoesServos_Formacoes_formacaoId",
                table: "FormacoesServos",
                column: "formacaoId",
                principalTable: "Formacoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormacoesServos_Servos_servoId",
                table: "FormacoesServos",
                column: "servoId",
                principalTable: "Servos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormacoesServos_Users_usuarioId",
                table: "FormacoesServos",
                column: "usuarioId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormacoesServos_Formacoes_formacaoId",
                table: "FormacoesServos");

            migrationBuilder.DropForeignKey(
                name: "FK_FormacoesServos_Servos_servoId",
                table: "FormacoesServos");

            migrationBuilder.DropForeignKey(
                name: "FK_FormacoesServos_Users_usuarioId",
                table: "FormacoesServos");

            migrationBuilder.DropColumn(
                name: "certificateDate",
                table: "FormacoesServos");

            migrationBuilder.RenameColumn(
                name: "usuarioId",
                table: "FormacoesServos",
                newName: "UsuarioId");

            migrationBuilder.RenameColumn(
                name: "servoId",
                table: "FormacoesServos",
                newName: "ServoId");

            migrationBuilder.RenameColumn(
                name: "formacaoId",
                table: "FormacoesServos",
                newName: "FormacaoId");

            migrationBuilder.RenameIndex(
                name: "IX_FormacoesServos_usuarioId",
                table: "FormacoesServos",
                newName: "IX_FormacoesServos_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_FormacoesServos_servoId",
                table: "FormacoesServos",
                newName: "IX_FormacoesServos_ServoId");

            migrationBuilder.RenameIndex(
                name: "IX_FormacoesServos_formacaoId",
                table: "FormacoesServos",
                newName: "IX_FormacoesServos_FormacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormacoesServos_Formacoes_FormacaoId",
                table: "FormacoesServos",
                column: "FormacaoId",
                principalTable: "Formacoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormacoesServos_Servos_ServoId",
                table: "FormacoesServos",
                column: "ServoId",
                principalTable: "Servos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormacoesServos_Users_UsuarioId",
                table: "FormacoesServos",
                column: "UsuarioId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
