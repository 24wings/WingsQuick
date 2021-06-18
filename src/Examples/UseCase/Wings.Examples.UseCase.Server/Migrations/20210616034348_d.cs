using Microsoft.EntityFrameworkCore.Migrations;

namespace Wings.Examples.UseCase.Server.Migrations
{
    public partial class d : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attrs_AttrCategories_AttrCategoryId",
                table: "Attrs");

            migrationBuilder.DropForeignKey(
                name: "FK_Attrs_Categories_CategoryId",
                table: "Attrs");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Attrs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AttrCategoryId",
                table: "Attrs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attrs_AttrCategories_AttrCategoryId",
                table: "Attrs");

            migrationBuilder.DropForeignKey(
                name: "FK_Attrs_Categories_CategoryId",
                table: "Attrs");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Attrs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AttrCategoryId",
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
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attrs_Categories_CategoryId",
                table: "Attrs",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
