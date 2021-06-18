using Microsoft.EntityFrameworkCore.Migrations;

namespace Wings.Examples.UseCase.Server.Migrations
{
    public partial class _1288 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attrs_AttrCategories_AttrCategoryId",
                table: "Attrs");

            migrationBuilder.RenameColumn(
                name: "AttrCategoryId",
                table: "Attrs",
                newName: "attrCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Attrs_AttrCategoryId",
                table: "Attrs",
                newName: "IX_Attrs_attrCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attrs_AttrCategories_attrCategoryId",
                table: "Attrs",
                column: "attrCategoryId",
                principalTable: "AttrCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attrs_AttrCategories_attrCategoryId",
                table: "Attrs");

            migrationBuilder.RenameColumn(
                name: "attrCategoryId",
                table: "Attrs",
                newName: "AttrCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Attrs_attrCategoryId",
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
    }
}
