using Microsoft.EntityFrameworkCore.Migrations;

namespace Wings.Examples.UseCase.Server.Migrations
{
    public partial class changeattrtable8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attrs_AttrCategories_AttrCategoryId",
                table: "Attrs");

            migrationBuilder.DropIndex(
                name: "IX_Attrs_AttrCategoryId",
                table: "Attrs");
        }
    }
}
