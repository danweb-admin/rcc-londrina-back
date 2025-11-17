using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RccManager.Infra.Migrations
{
    public partial class AddedTableEventos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    active = table.Column<bool>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    event_name = table.Column<string>(maxLength: 150, nullable: false),
                    start_date = table.Column<DateTime>(nullable: false),
                    end_date = table.Column<DateTime>(nullable: false),
                    organizer_email = table.Column<string>(maxLength: 100, nullable: true),
                    organizer_phone = table.Column<string>(maxLength: 20, nullable: true),
                    event_image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    pix = table.Column<string>(maxLength: 50, nullable: true),
                    value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    status = table.Column<string>(maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Eventos");
        }
    }
}
