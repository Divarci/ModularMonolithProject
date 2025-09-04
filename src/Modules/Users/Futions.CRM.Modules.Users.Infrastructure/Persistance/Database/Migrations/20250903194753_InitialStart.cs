using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Futions.CRM.Modules.Users.Infrastructure.Persistance.Database.Migrations;

/// <inheritdoc />
public partial class InitialStart : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "users");

        migrationBuilder.CreateTable(
            name: "Permissions",
            schema: "users",
            columns: table => new
            {
                Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Permissions", x => x.Code);
            });

        migrationBuilder.CreateTable(
            name: "Roles",
            schema: "users",
            columns: table => new
            {
                Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Roles", x => x.Name);
            });

        migrationBuilder.CreateTable(
            name: "User",
            schema: "users",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Email = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                Firstname = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                Lastname = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                IdentityId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_User", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "RolePermissions",
            schema: "users",
            columns: table => new
            {
                RolesName = table.Column<string>(type: "nvarchar(20)", nullable: false),
                PermissionCode = table.Column<string>(type: "nvarchar(100)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RolePermissions", x => new { x.RolesName, x.PermissionCode });
                table.ForeignKey(
                    name: "FK_RolePermissions_Permissions_PermissionCode",
                    column: x => x.PermissionCode,
                    principalSchema: "users",
                    principalTable: "Permissions",
                    principalColumn: "Code",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_RolePermissions_Roles_RolesName",
                    column: x => x.RolesName,
                    principalSchema: "users",
                    principalTable: "Roles",
                    principalColumn: "Name",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "UserRoles",
            schema: "users",
            columns: table => new
            {
                RoleName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserRoles", x => new { x.RoleName, x.UserId });
                table.ForeignKey(
                    name: "FK_UserRoles_Roles_RoleName",
                    column: x => x.RoleName,
                    principalSchema: "users",
                    principalTable: "Roles",
                    principalColumn: "Name",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_UserRoles_User_UserId",
                    column: x => x.UserId,
                    principalSchema: "users",
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            schema: "users",
            table: "Permissions",
            column: "Code",
            values:
            [
                "deals:create",
                "deals:read",
                "deals:remove",
                "deals:update",
                "users:read",
                "users:update"
            ]);

        migrationBuilder.InsertData(
            schema: "users",
            table: "Roles",
            column: "Name",
            values:
            [
                "Administrator",
                "Member"
            ]);

        migrationBuilder.InsertData(
            schema: "users",
            table: "RolePermissions",
            columns: ["PermissionCode", "RolesName"],
            values: new object[,]
            {
                { "deals:create", "Administrator" },
                { "deals:read", "Administrator" },
                { "deals:remove", "Administrator" },
                { "deals:update", "Administrator" },
                { "users:read", "Administrator" },
                { "users:update", "Administrator" },
                { "deals:create", "Member" },
                { "deals:read", "Member" },
                { "deals:remove", "Member" },
                { "deals:update", "Member" },
                { "users:read", "Member" }
            });

        migrationBuilder.CreateIndex(
            name: "IX_RolePermissions_PermissionCode",
            schema: "users",
            table: "RolePermissions",
            column: "PermissionCode");

        migrationBuilder.CreateIndex(
            name: "IX_User_Email",
            schema: "users",
            table: "User",
            column: "Email",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_User_IdentityId",
            schema: "users",
            table: "User",
            column: "IdentityId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_UserRoles_UserId",
            schema: "users",
            table: "UserRoles",
            column: "UserId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "RolePermissions",
            schema: "users");

        migrationBuilder.DropTable(
            name: "UserRoles",
            schema: "users");

        migrationBuilder.DropTable(
            name: "Permissions",
            schema: "users");

        migrationBuilder.DropTable(
            name: "Roles",
            schema: "users");

        migrationBuilder.DropTable(
            name: "User",
            schema: "users");
    }
}
