using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Futions.CRM.Modules.Deals.Infrastructure.Persistance.Database.Migrations;

/// <inheritdoc />
public partial class DealsInitial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "deals");

        migrationBuilder.CreateTable(
            name: "Deal",
            schema: "deals",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Title = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                DealStatus = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Deal", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Product",
            schema: "deals",
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
            });

        migrationBuilder.CreateTable(
            name: "DealProduct",
            schema: "deals",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                DealId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Quantity = table.Column<int>(type: "int", nullable: false),
                Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                Discount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DealProduct", x => x.Id);
                table.ForeignKey(
                    name: "FK_DealProduct_Deal_DealId",
                    column: x => x.DealId,
                    principalSchema: "deals",
                    principalTable: "Deal",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_DealProduct_Product_ProductId",
                    column: x => x.ProductId,
                    principalSchema: "deals",
                    principalTable: "Product",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_DealProduct_DealId",
            schema: "deals",
            table: "DealProduct",
            column: "DealId");

        migrationBuilder.CreateIndex(
            name: "IX_DealProduct_ProductId",
            schema: "deals",
            table: "DealProduct",
            column: "ProductId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "DealProduct",
            schema: "deals");

        migrationBuilder.DropTable(
            name: "Deal",
            schema: "deals");

        migrationBuilder.DropTable(
            name: "Product",
            schema: "deals");
    }
}
