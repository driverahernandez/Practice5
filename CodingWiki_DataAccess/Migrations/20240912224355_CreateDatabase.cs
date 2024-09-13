using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Practice5_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    PurchaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false),
                    PurchaseDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.PurchaseId);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    SaleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false),
                    SaleDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.SaleId);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Price", "ProductName" },
                values: new object[,]
                {
                    { 1, 199.99000000000001, "Computer" },
                    { 2, 99.290000000000006, "Phone" },
                    { 3, 79.489999999999995, "Printer" },
                    { 4, 39.899999999999999, "Desk" },
                    { 5, 29.890000000000001, "Chair" },
                    { 6, 29.390000000000001, "Headphones" },
                    { 7, 19.989999999999998, "Mouse" },
                    { 8, 24.949999999999999, "Keyboard" },
                    { 9, 34.689999999999998, "Table" },
                    { 10, 7.79, "Backpack" }
                });

            migrationBuilder.InsertData(
                table: "Sales",
                columns: new[] { "SaleId", "ProductId", "SaleDate", "Total" },
                values: new object[,]
                {
                    { 1, 1, new DateOnly(2024, 9, 21), 2199.98 },
                    { 2, 2, new DateOnly(2024, 9, 12), 1299.49 },
                    { 3, 3, new DateOnly(2024, 9, 13), 33499.989999999998 },
                    { 4, 4, new DateOnly(2024, 9, 14), 28734.119999999999 },
                    { 5, 3, new DateOnly(2024, 9, 6), 397421.09999999998 },
                    { 6, 4, new DateOnly(2024, 9, 30), 23879.290000000001 },
                    { 7, 1, new DateOnly(2024, 9, 9), 834.92999999999995 },
                    { 8, 2, new DateOnly(2024, 9, 10), 97342.869999999995 },
                    { 9, 3, new DateOnly(2024, 9, 24), 8921.7299999999996 },
                    { 10, 4, new DateOnly(2024, 9, 12), 181734.39000000001 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "Sales");
        }
    }
}
