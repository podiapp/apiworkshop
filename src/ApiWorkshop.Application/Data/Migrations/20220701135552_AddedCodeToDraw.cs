using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiWorkshop.Application.Data.Migrations
{
    public partial class AddedCodeToDraw : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "workshop",
                table: "PrizeDraws",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                schema: "workshop",
                table: "PrizeDraws");
        }
    }
}
