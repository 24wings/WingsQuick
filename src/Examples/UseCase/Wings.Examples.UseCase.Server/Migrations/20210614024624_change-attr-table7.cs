using Microsoft.EntityFrameworkCore.Migrations;

namespace Wings.Examples.UseCase.Server.Migrations
{
    public partial class changeattrtable7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attrs_AttrCategories_AttrCategoryId",
                table: "Attrs");

            migrationBuilder.DropIndex(
                name: "IX_Attrs_AttrCategoryId",
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AttrCategoryId",
                table: "Attrs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Attrs_AttrCategoryId",
                table: "Attrs",
                column: "AttrCategoryId");

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
