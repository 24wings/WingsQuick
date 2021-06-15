using Microsoft.EntityFrameworkCore.Migrations;

namespace Wings.Examples.UseCase.Server.Migrations
{
    public partial class changeattrtable4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attrs_AttrCategories_AttrCategoryId",
                table: "Attrs");

            migrationBuilder.DropForeignKey(
                name: "FK_Attrs_AttrCategories_AttrCategoryId1",
                table: "Attrs");

            migrationBuilder.DropIndex(
                name: "IX_Attrs_AttrCategoryId1",
                table: "Attrs");

            migrationBuilder.DropColumn(
                name: "AttrCategoryId1",
                table: "Attrs");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attrs_AttrCategories_AttrCategoryId",
                table: "Attrs");

            migrationBuilder.AlterColumn<int>(
                name: "AttrCategoryId",
                table: "Attrs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AttrCategoryId1",
                table: "Attrs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attrs_AttrCategoryId1",
                table: "Attrs",
                column: "AttrCategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Attrs_AttrCategories_AttrCategoryId",
                table: "Attrs",
                column: "AttrCategoryId",
                principalTable: "AttrCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
