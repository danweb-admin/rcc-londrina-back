using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RccManager.Infra.Migrations
{
    public partial class ChangedColumnType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "birthday",
                table: "ServosTemp",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "birthday1",
                table: "ServosTemp",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "birthday1",
                table: "ServosTemp");

            migrationBuilder.AlterColumn<string>(
                name: "birthday",
                table: "ServosTemp",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime));
        }
    }
}
