using Practice5_Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice5_DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        //property name = name of the table that gets added in the database
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<ProductInventory> ProductsInventory { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //instead of using overridden function, use a constructor where options is passed. 
        //the configuration is  set in the webapp project program.cs file
        //using the constructor of the base class DbContext. 
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //this is for using the consoleapp:
            //options.UseSqlServer("Server=USQRODRIVERAHE1;Database=Practice5;TrustServerCertificate=True;Trusted_Connection=True;");
            //for the webapp the connection string is set in the appsettings.json file. 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var bookList = new Product[]
            {
                new Product { ProductId = 1, ProductName = "Computer", Price = 199.99},
                new Product { ProductId = 2, ProductName = "Phone", Price = 99.29},
                new Product { ProductId = 3, ProductName = "Printer", Price = 79.49},
                new Product { ProductId = 4, ProductName = "Desk", Price = 39.90},
                new Product { ProductId = 5, ProductName = "Chair", Price = 29.89},
                new Product { ProductId = 6, ProductName = "Headphones", Price = 29.39},
                new Product { ProductId = 7, ProductName = "Mouse", Price = 19.99},
                new Product { ProductId = 8, ProductName = "Keyboard", Price = 24.95},
                new Product { ProductId = 9, ProductName = "Table", Price = 34.69},
                new Product { ProductId = 10, ProductName = "Backpack", Price = 7.79}
            };

            //Fluent API
            //can rename table, column, add constraints, keys, etc. 
            modelBuilder.Entity<Product>().HasData(bookList);

            var salesList = new Sale[]
            {
                 new Sale {SaleId = 1, ProductId = 1, Total = 2199.98, SaleDate = new DateOnly(2024, 9, 21)},
                 new Sale {SaleId = 2, ProductId = 2, Total = 1299.49, SaleDate = new DateOnly(2024, 9, 12)},
                 new Sale {SaleId = 3, ProductId = 3, Total = 33499.99, SaleDate = new DateOnly(2024, 9, 13)},
                 new Sale {SaleId = 4, ProductId = 4, Total = 28734.12, SaleDate = new DateOnly(2024, 9, 14)},
                 new Sale {SaleId = 5, ProductId = 3, Total = 397421.10, SaleDate = new DateOnly(2024, 9, 6)},
                 new Sale {SaleId = 6, ProductId = 4, Total = 23879.29, SaleDate = new DateOnly(2024, 9, 30)},
                 new Sale {SaleId = 7, ProductId = 1, Total = 834.93, SaleDate = new DateOnly(2024, 9, 9)},
                 new Sale {SaleId = 8, ProductId = 2, Total = 97342.87, SaleDate = new DateOnly(2024, 9, 10)},
                 new Sale {SaleId = 9, ProductId = 3, Total = 8921.73, SaleDate = new DateOnly(2024, 9, 24)},
                 new Sale {SaleId = 10, ProductId = 4, Total = 181734.39, SaleDate = new DateOnly(2024, 9, 12)}
            };
            modelBuilder.Entity<Sale>().HasData(salesList);

            var purchaseList = new Purchase[]
            {
                new Purchase {PurchaseId = 1, ProductId = 9, Total = 129783.12, PurchaseDate = new DateOnly(2024, 9, 23)},
                new Purchase {PurchaseId = 2, ProductId = 8, Total = 4823.38, PurchaseDate = new DateOnly(2024, 9, 28)},
                new Purchase {PurchaseId = 3, ProductId = 7, Total = 7298.57, PurchaseDate = new DateOnly(2024, 9, 19)},
                new Purchase {PurchaseId = 4, ProductId = 6, Total = 42389.19, PurchaseDate = new DateOnly(2024, 9, 11)},
                new Purchase {PurchaseId = 5, ProductId = 5, Total = 394287.74, PurchaseDate = new DateOnly(2024, 9, 5)},
                new Purchase {PurchaseId = 6, ProductId = 4, Total = 978234.83, PurchaseDate = new DateOnly(2024, 9, 6)},
                new Purchase {PurchaseId = 7, ProductId = 9, Total = 238723.81, PurchaseDate = new DateOnly(2024, 9, 29)},
                new Purchase {PurchaseId = 8, ProductId = 9, Total = 54198.29, PurchaseDate = new DateOnly(2024, 9, 4)},
                new Purchase {PurchaseId = 9, ProductId = 8, Total = 3748615.40, PurchaseDate = new DateOnly(2024, 9, 12)},
                new Purchase {PurchaseId = 10, ProductId = 7, Total = 139874.34, PurchaseDate = new DateOnly(2024, 9, 13)}
            };

            modelBuilder.Entity<Purchase>().HasData(purchaseList);

            var inventoryList = new ProductInventory[]
            {
                new ProductInventory{ProductId = 1, Amount = 23},
                new ProductInventory{ProductId = 2, Amount = 4},
                new ProductInventory{ProductId = 3, Amount = 47},
                new ProductInventory{ProductId = 4, Amount = 84},
                new ProductInventory{ProductId = 5, Amount = 89},
                new ProductInventory{ProductId = 6, Amount = 46},
                new ProductInventory{ProductId = 7, Amount = 17},
                new ProductInventory{ProductId = 8, Amount = 40},
                new ProductInventory{ProductId = 9, Amount = 16},
                new ProductInventory{ProductId = 10, Amount = 34}
            };

            modelBuilder.Entity<ProductInventory>().HasData(inventoryList);
        }
    }
}
