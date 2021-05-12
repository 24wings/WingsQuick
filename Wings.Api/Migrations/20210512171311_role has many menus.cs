using Microsoft.EntityFrameworkCore.Migrations;

namespace Wings.Api.Migrations
{
    public partial class rolehasmanymenus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menus_Roles_RoleId",
                table: "Menus");

            migrationBuilder.DropIndex(
                name: "IX_Menus_RoleId",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Menus");

            migrationBuilder.CreateTable(
                name: "MenuRole",
                columns: table => new
                {
                    MenusId = table.Column<int>(type: "int", nullable: false),
                    RolesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuRole", x => new { x.MenusId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_MenuRole_Menus_MenusId",
                        column: x => x.MenusId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuRole_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MenuRole_RolesId",
                table: "MenuRole",
                column: "RolesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuRole");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Menus",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Menus_RoleId",
                table: "Menus",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_Roles_RoleId",
                table: "Menus",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
