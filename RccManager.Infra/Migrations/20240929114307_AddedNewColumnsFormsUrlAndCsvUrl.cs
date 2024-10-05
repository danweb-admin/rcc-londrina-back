using Microsoft.EntityFrameworkCore.Migrations;

namespace RccManager.Infra.Migrations
{
    public partial class AddedNewColumnsFormsUrlAndCsvUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "csvUrl",
                table: "GrupoOracoes",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "formsUrl",
                table: "GrupoOracoes",
                maxLength: 300,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "csvUrl",
                table: "GrupoOracoes");

            migrationBuilder.DropColumn(
                name: "formsUrl",
                table: "GrupoOracoes");
        }
    }
}
