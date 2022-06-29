using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiWorkshop.Application.Data.Migrations
{
    public partial class AddedGiftAsNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrizeDraws_Gifts_GiftId",
                schema: "workshop",
                table: "PrizeDraws");

            migrationBuilder.AlterColumn<Guid>(
                name: "GiftId",
                schema: "workshop",
                table: "PrizeDraws",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_PrizeDraws_Gifts_GiftId",
                schema: "workshop",
                table: "PrizeDraws",
                column: "GiftId",
                principalSchema: "workshop",
                principalTable: "Gifts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrizeDraws_Gifts_GiftId",
                schema: "workshop",
                table: "PrizeDraws");

            migrationBuilder.AlterColumn<Guid>(
                name: "GiftId",
                schema: "workshop",
                table: "PrizeDraws",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PrizeDraws_Gifts_GiftId",
                schema: "workshop",
                table: "PrizeDraws",
                column: "GiftId",
                principalSchema: "workshop",
                principalTable: "Gifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
