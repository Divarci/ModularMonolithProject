using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Futions.CRM.Modules.Catalogue.Infrastructure.Persistance.Database.Migrations;

/// <inheritdoc />
public partial class IsPendingAdded : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<bool>(
            name: "IsPending",
            schema: "catalogue",
            table: "ProductBook",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<bool>(
            name: "IsPending",
            schema: "catalogue",
            table: "Product",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.CreateTable(
            name: "CatalogueInboxMessage",
            schema: "catalogue",
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
                table.PrimaryKey("PK_CatalogueInboxMessage", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "CatalogueInboxMessageConsumer",
            schema: "catalogue",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CatalogueInboxMessageConsumer", x => new { x.Id, x.Name });
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "CatalogueInboxMessage",
            schema: "catalogue");

        migrationBuilder.DropTable(
            name: "CatalogueInboxMessageConsumer",
            schema: "catalogue");

        migrationBuilder.DropColumn(
            name: "IsPending",
            schema: "catalogue",
            table: "ProductBook");

        migrationBuilder.DropColumn(
            name: "IsPending",
            schema: "catalogue",
            table: "Product");
    }
}
