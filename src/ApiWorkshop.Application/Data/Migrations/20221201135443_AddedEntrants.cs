using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiWorkshop.Application.Data.Migrations
{
    public partial class AddedEntrants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entrants_Draws_DrawId",
                schema: "workshop",
                table: "Entrants");

            migrationBuilder.AlterColumn<Guid>(
                name: "DrawId",
                schema: "workshop",
                table: "Entrants",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Entrants_Draws_DrawId",
                schema: "workshop",
                table: "Entrants",
                column: "DrawId",
                principalSchema: "workshop",
                principalTable: "Draws",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entrants_Draws_DrawId",
                schema: "workshop",
                table: "Entrants");

            migrationBuilder.AlterColumn<Guid>(
                name: "DrawId",
                schema: "workshop",
                table: "Entrants",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Entrants_Draws_DrawId",
                schema: "workshop",
                table: "Entrants",
                column: "DrawId",
                principalSchema: "workshop",
                principalTable: "Draws",
                principalColumn: "Id");
        }
    }
}
