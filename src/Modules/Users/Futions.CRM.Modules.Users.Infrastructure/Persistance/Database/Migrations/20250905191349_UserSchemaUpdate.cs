using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Futions.CRM.Modules.Users.Infrastructure.Persistance.Database.Migrations;

/// <inheritdoc />
public partial class UserSchemaUpdate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "users");

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
            name: "Role",
            schema: "users",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Role", x => x.Id);
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
                { new Guid("d9a55c23-3362-484f-bd5a-81337ffa59c2"), new Guid("46efca56-d955-4815-80f5-14d382471c52"), new Guid("4c2c722b-094b-4111-bddb-0086af5277ed") }
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

        migrationBuilder.DropTable(
            name: "Role",
            schema: "users");

        migrationBuilder.DropTable(
            name: "User",
            schema: "users");
    }
}
