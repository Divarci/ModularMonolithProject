using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Futions.CRM.Modules.Catalogue.Infrastructure.Persistance.Database.Migrations;

/// <inheritdoc />
public partial class InitialCatalogueCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "catalogue");

        migrationBuilder.CreateTable(
            name: "ProductBook",
            schema: "catalogue",
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

        migrationBuilder.CreateTable(
            name: "Product",
            schema: "catalogue",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Title = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                ProductBookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Product", x => x.Id);
                table.ForeignKey(
                    name: "FK_Product_ProductBook_ProductBookId",
                    column: x => x.ProductBookId,
                    principalSchema: "catalogue",
                    principalTable: "ProductBook",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_Product_ProductBookId",
            schema: "catalogue",
            table: "Product",
            column: "ProductBookId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Product",
            schema: "catalogue");

        migrationBuilder.DropTable(
            name: "ProductBook",
            schema: "catalogue");
    }
}
