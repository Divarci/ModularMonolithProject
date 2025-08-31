using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Futions.CRM.Modules.Users.Infrastructure.Persistance.Database.Migrations;

/// <inheritdoc />
public partial class UserTableUpdate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "IdentityId",
            schema: "users",
            table: "User",
            type: "nvarchar(450)",
            nullable: false,
            defaultValue: "");

        migrationBuilder.CreateIndex(
            name: "IX_User_IdentityId",
            schema: "users",
            table: "User",
            column: "IdentityId",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_User_IdentityId",
            schema: "users",
            table: "User");

        migrationBuilder.DropColumn(
            name: "IdentityId",
            schema: "users",
            table: "User");
    }
}
