using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiWorkshop.Application.Data.Migrations
{
    public partial class AddedEntrant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Draws",
                schema: "workshop",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PrizeName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PrizePhoto = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    PrizeDescription = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    DrawDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    MallId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Draws", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Entrants",
                schema: "workshop",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    Phone = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    DrawId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entrants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entrants_Draws_DrawId",
                        column: x => x.DrawId,
                        principalSchema: "workshop",
                        principalTable: "Draws",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entrants_DrawId",
                schema: "workshop",
                table: "Entrants",
                column: "DrawId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entrants",
                schema: "workshop");

            migrationBuilder.DropTable(
                name: "Draws",
                schema: "workshop");
        }
    }
}
