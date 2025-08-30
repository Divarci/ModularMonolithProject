using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Futions.CRM.Modules.Deals.Infrastructure.Persistance.Database.Migrations;

/// <inheritdoc />
public partial class ProductBookAdded : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "ProductBook",
            schema: "deals",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Title = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                Inactive = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProductBook", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Product_ProductBookId",
            schema: "deals",
            table: "Product",
            column: "ProductBookId");

        migrationBuilder.AddForeignKey(
            name: "FK_Product_ProductBook_ProductBookId",
            schema: "deals",
            table: "Product",
            column: "ProductBookId",
            principalSchema: "deals",
            principalTable: "ProductBook",
            principalColumn: "Id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Product_ProductBook_ProductBookId",
            schema: "deals",
            table: "Product");

        migrationBuilder.DropTable(
            name: "ProductBook",
            schema: "deals");

        migrationBuilder.DropIndex(
            name: "IX_Product_ProductBookId",
            schema: "deals",
            table: "Product");
    }
}
