using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Futions.CRM.Modules.Users.Infrastructure.Persistance.Database.Migrations;

/// <inheritdoc />
public partial class InitialUsers : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "users");

        migrationBuilder.CreateTable(
            name: "User",
            schema: "users",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Email = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                Fullname = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_User", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_User_Email",
            schema: "users",
            table: "User",
            column: "Email",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "User",
            schema: "users");
    }
}
