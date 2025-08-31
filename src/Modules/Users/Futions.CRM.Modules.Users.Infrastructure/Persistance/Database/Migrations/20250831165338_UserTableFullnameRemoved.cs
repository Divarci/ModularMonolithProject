using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Futions.CRM.Modules.Users.Infrastructure.Persistance.Database.Migrations;

/// <inheritdoc />
public partial class UserTableFullnameRemoved : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "Fullname",
            schema: "users",
            table: "User",
            newName: "Lastname");

        migrationBuilder.AlterColumn<string>(
            name: "IdentityId",
            schema: "users",
            table: "User",
            type: "nvarchar(100)",
            maxLength: 100,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(450)");

        migrationBuilder.AddColumn<string>(
            name: "Firstname",
            schema: "users",
            table: "User",
            type: "nvarchar(64)",
            maxLength: 64,
            nullable: false,
            defaultValue: "");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Firstname",
            schema: "users",
            table: "User");

        migrationBuilder.RenameColumn(
            name: "Lastname",
            schema: "users",
            table: "User",
            newName: "Fullname");

        migrationBuilder.AlterColumn<string>(
            name: "IdentityId",
            schema: "users",
            table: "User",
            type: "nvarchar(450)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(100)",
            oldMaxLength: 100);
    }
}
