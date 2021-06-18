using Microsoft.EntityFrameworkCore.Migrations;

namespace Wings.Examples.UseCase.Server.Migrations
{
    public partial class _122 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attrs_AttrCategories_AttrCategoryId",
                table: "Attrs");

            migrationBuilder.DropForeignKey(
                name: "FK_Attrs_Categories_CategoryId",
                table: "Attrs");

            migrationBuilder.RenameColumn(
                name: "AttrCategoryId",
                table: "Attrs",
                newName: "AttrCategoryId1");

            migrationBuilder.RenameIndex(
                name: "IX_Attrs_AttrCategoryId",
                table: "Attrs",
                newName: "IX_Attrs_AttrCategoryId1");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Attrs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Attrs_AttrCategories_AttrCategoryId1",
                table: "Attrs",
                column: "AttrCategoryId1",
                principalTable: "AttrCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Attrs_Categories_CategoryId",
                table: "Attrs",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attrs_AttrCategories_AttrCategoryId1",
                table: "Attrs");

            migrationBuilder.DropForeignKey(
                name: "FK_Attrs_Categories_CategoryId",
                table: "Attrs");

            migrationBuilder.RenameColumn(
                name: "AttrCategoryId1",
                table: "Attrs",
                newName: "AttrCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Attrs_AttrCategoryId1",
                table: "Attrs",
                newName: "IX_Attrs_AttrCategoryId");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Attrs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Attrs_AttrCategories_AttrCategoryId",
                table: "Attrs",
                column: "AttrCategoryId",
                principalTable: "AttrCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Attrs_Categories_CategoryId",
                table: "Attrs",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
