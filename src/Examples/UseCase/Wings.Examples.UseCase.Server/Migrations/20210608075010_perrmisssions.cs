using Microsoft.EntityFrameworkCore.Migrations;

namespace Wings.Examples.UseCase.Server.Migrations
{
    public partial class perrmisssions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_AspNetRoles_RbacRoleId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_RbacRoleId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "RbacRoleId",
                table: "Permissions");

            migrationBuilder.CreateTable(
                name: "PermissionRbacRole",
                columns: table => new
                {
                    PermissionsId = table.Column<int>(type: "int", nullable: false),
                    RolesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionRbacRole", x => new { x.PermissionsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_PermissionRbacRole_AspNetRoles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionRbacRole_Permissions_PermissionsId",
                        column: x => x.PermissionsId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionRbacRole_RolesId",
                table: "PermissionRbacRole",
                column: "RolesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermissionRbacRole");

            migrationBuilder.AddColumn<int>(
                name: "RbacRoleId",
                table: "Permissions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_RbacRoleId",
                table: "Permissions",
                column: "RbacRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_AspNetRoles_RbacRoleId",
                table: "Permissions",
                column: "RbacRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
