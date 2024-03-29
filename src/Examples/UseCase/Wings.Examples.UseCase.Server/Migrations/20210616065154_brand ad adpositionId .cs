﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Wings.Examples.UseCase.Server.Migrations
{
    public partial class brandadadpositionId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ads_AdPositions_AdPositionId",
                table: "Ads");

            migrationBuilder.AlterColumn<int>(
                name: "AdPositionId",
                table: "Ads",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_AdPositions_AdPositionId",
                table: "Ads",
                column: "AdPositionId",
                principalTable: "AdPositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ads_AdPositions_AdPositionId",
                table: "Ads");

            migrationBuilder.AlterColumn<int>(
                name: "AdPositionId",
                table: "Ads",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_AdPositions_AdPositionId",
                table: "Ads",
                column: "AdPositionId",
                principalTable: "AdPositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
