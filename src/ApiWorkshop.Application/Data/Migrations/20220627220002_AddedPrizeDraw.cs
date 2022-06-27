using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiWorkshop.Application.Data.Migrations
{
    public partial class AddedPrizeDraw : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrizeDraws",
                schema: "workshop",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GiftId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrizeDraws", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrizeDraws_Gifts_GiftId",
                        column: x => x.GiftId,
                        principalSchema: "workshop",
                        principalTable: "Gifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrizeDraws_GiftId",
                schema: "workshop",
                table: "PrizeDraws",
                column: "GiftId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrizeDraws",
                schema: "workshop");
        }
    }
}
