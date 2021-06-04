using Microsoft.EntityFrameworkCore.Migrations;

namespace Wings.Api.Migrations
{
    public partial class removemenuparentChildrenfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menus_Menus_ParentId",
                table: "Menus");

            migrationBuilder.DropIndex(
                name: "IX_Menus_ParentId",
                table: "Menus");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "Menus",
                newName: "Url");

            migrationBuilder.AddColumn<string>(
                name: "TreePath",
                table: "Menus",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TreePath",
                table: "Menus");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Menus",
                newName: "Path");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_ParentId",
                table: "Menus",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_Menus_ParentId",
                table: "Menus",
                column: "ParentId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
