using Microsoft.EntityFrameworkCore.Migrations;

namespace Wings.Examples.UseCase.Server.Migrations
{
    public partial class _124 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attrs_AttrCategories_AttrCategoryId1",
                table: "Attrs");

            migrationBuilder.RenameColumn(
                name: "AttrCategoryId1",
                table: "Attrs",
                newName: "AttrCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Attrs_AttrCategoryId1",
                table: "Attrs",
                newName: "IX_Attrs_AttrCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attrs_AttrCategories_AttrCategoryId",
                table: "Attrs",
                column: "AttrCategoryId",
                principalTable: "AttrCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attrs_AttrCategories_AttrCategoryId",
                table: "Attrs");

            migrationBuilder.RenameColumn(
                name: "AttrCategoryId",
                table: "Attrs",
                newName: "AttrCategoryId1");

            migrationBuilder.RenameIndex(
                name: "IX_Attrs_AttrCategoryId",
                table: "Attrs",
                newName: "IX_Attrs_AttrCategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Attrs_AttrCategories_AttrCategoryId1",
                table: "Attrs",
                column: "AttrCategoryId1",
                principalTable: "AttrCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
