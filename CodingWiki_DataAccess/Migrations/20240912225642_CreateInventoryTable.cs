using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Practice5_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreateInventoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductsInventory",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsInventory", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "ProductsInventory",
                columns: new[] { "ProductId", "Amount" },
                values: new object[,]
                {
                    { 1, 23 },
                    { 2, 4 },
                    { 3, 47 },
                    { 4, 84 },
                    { 5, 89 },
                    { 6, 46 },
                    { 7, 17 },
                    { 8, 40 },
                    { 9, 16 },
                    { 10, 34 }
                });

            migrationBuilder.InsertData(
                table: "Purchases",
                columns: new[] { "PurchaseId", "ProductId", "PurchaseDate", "Total" },
                values: new object[,]
                {
                    { 1, 9, new DateOnly(2024, 9, 23), 129783.12 },
                    { 2, 8, new DateOnly(2024, 9, 28), 4823.3800000000001 },
                    { 3, 7, new DateOnly(2024, 9, 19), 7298.5699999999997 },
                    { 4, 6, new DateOnly(2024, 9, 11), 42389.190000000002 },
                    { 5, 5, new DateOnly(2024, 9, 5), 394287.73999999999 },
                    { 6, 4, new DateOnly(2024, 9, 6), 978234.82999999996 },
                    { 7, 9, new DateOnly(2024, 9, 29), 238723.81 },
                    { 8, 9, new DateOnly(2024, 9, 4), 54198.290000000001 },
                    { 9, 8, new DateOnly(2024, 9, 12), 3748615.3999999999 },
                    { 10, 7, new DateOnly(2024, 9, 13), 139874.34 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductsInventory");

            migrationBuilder.DeleteData(
                table: "Purchases",
                keyColumn: "PurchaseId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Purchases",
                keyColumn: "PurchaseId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Purchases",
                keyColumn: "PurchaseId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Purchases",
                keyColumn: "PurchaseId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Purchases",
                keyColumn: "PurchaseId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Purchases",
                keyColumn: "PurchaseId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Purchases",
                keyColumn: "PurchaseId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Purchases",
                keyColumn: "PurchaseId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Purchases",
                keyColumn: "PurchaseId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Purchases",
                keyColumn: "PurchaseId",
                keyValue: 10);
        }
    }
}
