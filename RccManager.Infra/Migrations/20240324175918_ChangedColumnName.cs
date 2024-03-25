using Microsoft.EntityFrameworkCore.Migrations;

namespace RccManager.Infra.Migrations
{
    public partial class ChangedColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "birthday1",
                table: "ServosTemp");

            migrationBuilder.AddColumn<string>(
                name: "birthday",
                table: "ServosTemp",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "birthday",
                table: "ServosTemp");

            migrationBuilder.AddColumn<string>(
                name: "birthday1",
                table: "ServosTemp",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }
    }
}
