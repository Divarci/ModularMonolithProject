using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Futions.CRM.Modules.Users.Infrastructure.Persistance.Database.Migrations;

/// <inheritdoc />
public partial class TablesReDesign : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
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

        migrationBuilder.DropPrimaryKey(
            name: "PK_Roles",
            schema: "users",
            table: "Roles");

        migrationBuilder.DeleteData(
            schema: "users",
            table: "Roles",
            keyColumn: "Name",
            keyValue: "Administrator");

        migrationBuilder.DeleteData(
            schema: "users",
            table: "Roles",
            keyColumn: "Name",
            keyValue: "Member");

        migrationBuilder.RenameTable(
            name: "Roles",
            schema: "users",
            newName: "Role",
            newSchema: "users");

        migrationBuilder.AddColumn<Guid>(
            name: "Id",
            schema: "users",
            table: "Role",
            type: "uniqueidentifier",
            nullable: false,
            defaultValue: Guid.Empty);

        migrationBuilder.AddPrimaryKey(
            name: "PK_Role",
            schema: "users",
            table: "Role",
            column: "Id");

        migrationBuilder.CreateTable(
            name: "Permission",
            schema: "users",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Permission", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "UserRole",
            schema: "users",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserRole", x => x.Id);
                table.ForeignKey(
                    name: "FK_UserRole_Role_RoleId",
                    column: x => x.RoleId,
                    principalSchema: "users",
                    principalTable: "Role",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_UserRole_User_UserId",
                    column: x => x.UserId,
                    principalSchema: "users",
                    principalTable: "User",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "RolePermission",
            schema: "users",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RolePermission", x => x.Id);
                table.ForeignKey(
                    name: "FK_RolePermission_Permission_PermissionId",
                    column: x => x.PermissionId,
                    principalSchema: "users",
                    principalTable: "Permission",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_RolePermission_Role_RoleId",
                    column: x => x.RoleId,
                    principalSchema: "users",
                    principalTable: "Role",
                    principalColumn: "Id");
            });

        migrationBuilder.InsertData(
            schema: "users",
            table: "Permission",
            columns: ["Id", "Code"],
            values: new object[,]
            {
                { new Guid("39675711-a006-4b01-ba29-0d933f268060"), "deals:read" },
                { new Guid("3ed06827-98b7-4916-bef7-70e3119dd8ec"), "deals:update" },
                { new Guid("46efca56-d955-4815-80f5-14d382471c52"), "deals:remove" },
                { new Guid("818d85f9-5961-40a1-9d4a-e207e30ba783"), "users:update" },
                { new Guid("c935b2be-3d2f-44c4-9c2d-f00b5375ac48"), "users:read" },
                { new Guid("e9338b23-2d61-4ec3-aa80-ba8fe59b65f0"), "deals:create" }
            });

        migrationBuilder.InsertData(
            schema: "users",
            table: "Role",
            columns: ["Id", "Name"],
            values: new object[,]
            {
                { new Guid("4c2c722b-094b-4111-bddb-0086af5277ed"), "Administrator" },
                { new Guid("5de1f66b-991d-49ea-ad1a-059b5eebcf8d"), "Member" }
            });

        migrationBuilder.InsertData(
            schema: "users",
            table: "RolePermission",
            columns: ["Id", "PermissionId", "RoleId"],
            values: new object[,]
            {
                { new Guid("07c74200-ad79-47cc-bfc5-6a03574d9ea1"), new Guid("3ed06827-98b7-4916-bef7-70e3119dd8ec"), new Guid("5de1f66b-991d-49ea-ad1a-059b5eebcf8d") },
                { new Guid("1eb17514-e8da-48a0-b3e2-b14c204f1df3"), new Guid("818d85f9-5961-40a1-9d4a-e207e30ba783"), new Guid("4c2c722b-094b-4111-bddb-0086af5277ed") },
                { new Guid("28624ad4-d4f9-4dfd-81c9-9f8644bbf3cb"), new Guid("e9338b23-2d61-4ec3-aa80-ba8fe59b65f0"), new Guid("4c2c722b-094b-4111-bddb-0086af5277ed") },
                { new Guid("4ae14806-e433-43ed-a1ce-b80ebcd9a811"), new Guid("39675711-a006-4b01-ba29-0d933f268060"), new Guid("5de1f66b-991d-49ea-ad1a-059b5eebcf8d") },
                { new Guid("4f5f60f4-530f-4cc0-9b61-19f39657f3b9"), new Guid("c935b2be-3d2f-44c4-9c2d-f00b5375ac48"), new Guid("4c2c722b-094b-4111-bddb-0086af5277ed") },
                { new Guid("5492e74b-97ec-42a2-b048-e08eb9c179c0"), new Guid("c935b2be-3d2f-44c4-9c2d-f00b5375ac48"), new Guid("5de1f66b-991d-49ea-ad1a-059b5eebcf8d") },
                { new Guid("9613b245-c7f4-4d29-a740-81cd729a5158"), new Guid("39675711-a006-4b01-ba29-0d933f268060"), new Guid("4c2c722b-094b-4111-bddb-0086af5277ed") },
                { new Guid("a2fcb4af-b11b-48cb-925a-80bf668e0ebe"), new Guid("e9338b23-2d61-4ec3-aa80-ba8fe59b65f0"), new Guid("5de1f66b-991d-49ea-ad1a-059b5eebcf8d") },
                { new Guid("ba67ed03-8a2a-48b4-bc9b-cf5440064cb3"), new Guid("3ed06827-98b7-4916-bef7-70e3119dd8ec"), new Guid("4c2c722b-094b-4111-bddb-0086af5277ed") },
                { new Guid("d9a55c23-3362-484f-bd5a-81337ffa59c2"), new Guid("46efca56-d955-4815-80f5-14d382471c52"), new Guid("4c2c722b-094b-4111-bddb-0086af5277ed") },
                { new Guid("f6c3f373-afd8-41cf-a3d4-4d4b7a7a1ac2"), new Guid("46efca56-d955-4815-80f5-14d382471c52"), new Guid("5de1f66b-991d-49ea-ad1a-059b5eebcf8d") }
            });

        migrationBuilder.CreateIndex(
            name: "IX_RolePermission_PermissionId",
            schema: "users",
            table: "RolePermission",
            column: "PermissionId");

        migrationBuilder.CreateIndex(
            name: "IX_RolePermission_RoleId",
            schema: "users",
            table: "RolePermission",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "IX_UserRole_RoleId",
            schema: "users",
            table: "UserRole",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "IX_UserRole_UserId",
            schema: "users",
            table: "UserRole",
            column: "UserId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "RolePermission",
            schema: "users");

        migrationBuilder.DropTable(
            name: "UserRole",
            schema: "users");

        migrationBuilder.DropTable(
            name: "Permission",
            schema: "users");

        migrationBuilder.DropPrimaryKey(
            name: "PK_Role",
            schema: "users",
            table: "Role");

        migrationBuilder.DeleteData(
            schema: "users",
            table: "Role",
            keyColumn: "Id",
            keyColumnType: "uniqueidentifier",
            keyValue: new Guid("4c2c722b-094b-4111-bddb-0086af5277ed"));

        migrationBuilder.DeleteData(
            schema: "users",
            table: "Role",
            keyColumn: "Id",
            keyColumnType: "uniqueidentifier",
            keyValue: new Guid("5de1f66b-991d-49ea-ad1a-059b5eebcf8d"));

        migrationBuilder.DropColumn(
            name: "Id",
            schema: "users",
            table: "Role");

        migrationBuilder.RenameTable(
            name: "Role",
            schema: "users",
            newName: "Roles",
            newSchema: "users");

        migrationBuilder.AddPrimaryKey(
            name: "PK_Roles",
            schema: "users",
            table: "Roles",
            column: "Name");

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

        migrationBuilder.InsertData(
            schema: "users",
            table: "Permissions",
            column: "Code",
            values: new object[]
            {
                "deals:create",
                "deals:read",
                "deals:remove",
                "deals:update",
                "users:read",
                "users:update"
            });

        migrationBuilder.InsertData(
            schema: "users",
            table: "Roles",
            column: "Name",
            values: new object[]
            {
                "Administrator",
                "Member"
            });

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
            name: "IX_UserRoles_UserId",
            schema: "users",
            table: "UserRoles",
            column: "UserId");
    }
}
