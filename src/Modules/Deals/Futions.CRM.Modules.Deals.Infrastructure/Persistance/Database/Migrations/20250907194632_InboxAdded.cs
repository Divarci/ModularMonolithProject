using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Futions.CRM.Modules.Deals.Infrastructure.Persistance.Database.Migrations;

/// <inheritdoc />
public partial class InboxAdded : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "DealsInboxMessage",
            schema: "deals",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                OccurredOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                ProcessedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                Error = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DealsInboxMessage", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "DealsInboxMessageConsumer",
            schema: "deals",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DealsInboxMessageConsumer", x => new { x.Id, x.Name });
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "DealsInboxMessage",
            schema: "deals");

        migrationBuilder.DropTable(
            name: "DealsInboxMessageConsumer",
            schema: "deals");
    }
}
