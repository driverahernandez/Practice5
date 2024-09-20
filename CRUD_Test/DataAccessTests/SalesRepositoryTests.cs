using Microsoft.EntityFrameworkCore;
using Practice5_DataAccess.Data;
using Practice5_DataAccess.Data.EfRepositories;
using Practice5_DataAccess.Interface;
using Practice5_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice5.Test.DataAccessTests
{
    public class SalesRepositoryTests
    {
        private Sale _sale1;
        private Sale _sale2; 
        private Sale _sale3;
        private List<Sale> _salesList;
        private IRepositorySales _salesRepository; 

        [SetUp]
        public void Setup()
        {
            _sale1 = new Sale {ProductId = 2, SaleDate = new DateOnly(2024, 9, 18), Total = 1.1 };
            _sale2 = new Sale {ProductId = 2, SaleDate = new DateOnly(2024, 9, 19), Total = 1.2 };
            _sale3 = new Sale {ProductId = 4, SaleDate = new DateOnly(2024, 9, 20), Total = 1.3 };
            _salesList  = new List<Sale> {_sale1, _sale2, _sale3};
 
        }

        [Test]
        public void UpdateSale_Given_IdZero_ReturnsNewSale()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "temp_Sales").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                _salesRepository = new EFSalesRepository(context);
                var saleResult = _salesRepository.UpdateSale(0);
                Assert.AreEqual(0, saleResult.SaleId);          //Pass
                Assert.AreEqual(0, saleResult.ProductId);       //Pass
                //Assert.IsNull(saleResult);                    //Fail
            }
        }
        
        [Test]
        public void UpdateSale_Given_SaleWithIdZero_CreatesNewSaleInDB()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "temp_Sales").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                _salesRepository = new EFSalesRepository(context);

                _salesRepository.UpdateSale(_sale1);

                var saleFromDb = context.Sales.FirstOrDefault();
                //new sale will be assigned SaleId 1 
                Assert.AreEqual(1, saleFromDb.SaleId);                      //Pass
                Assert.AreEqual(_sale1.ProductId, saleFromDb.ProductId);    //Pass
                //Assert.AreEqual(0, saleFromDb.SaleId);                    //Fail
            }
        }

        [Test]
        public void GetSales_ReturnsListOfSales()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "temp_Sales").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                _salesRepository = new EFSalesRepository(context);
                _salesRepository.UpdateSale(_sale1);
                _salesRepository.UpdateSale(_sale2);
                _salesRepository.UpdateSale(_sale3);

                var result = _salesRepository.GetSales();

                CollectionAssert.AreEqual(_salesList, result);      //Pass
            }
        }
        [Test]
        public void UpdateSales_Given_ExistingId_ReturnsSaleWithThatId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "temp_Sales").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                _salesRepository = new EFSalesRepository(context);
                // create 3 sales in db
                _salesRepository.UpdateSale(_sale1);
                _salesRepository.UpdateSale(_sale2);
                _salesRepository.UpdateSale(_sale3);

                var existingId = 2; 

                var result = _salesRepository.UpdateSale(existingId);
                Assert.AreEqual(existingId, result.SaleId);     //Pass
                //Assert.AreEqual(0, result.ProductId);           //Fail


            }
        }
        [Test]
        public void UpdateSales_Given_ExistingSale_UpdatesSale()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "temp_Sales").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                _salesRepository = new EFSalesRepository(context);
                // create sale in db
                _salesRepository.UpdateSale(_sale1);
                var existingId = 1;
                // update existing sale. web project follows these steps:
                // 1. find sale
                var saleResult = _salesRepository.UpdateSale(existingId);
                // 2. sale data gets edited in Web View.
                saleResult.ProductId = 1;
                saleResult.SaleDate = new DateOnly(2024, 9, 12);
                saleResult.Total= 2.1;
                // 3. update sale in database
                _salesRepository.UpdateSale(saleResult);


                var saleFromDb = context.Sales.FirstOrDefault();

                Assert.AreEqual(2.1, saleFromDb.Total);                             //Pass
                Assert.AreEqual(1, saleFromDb.ProductId);                           //Pass
                Assert.AreEqual(new DateOnly(2024, 9, 12), saleFromDb.SaleDate);    //Pass
                //Assert.AreEqual(0, saleFromDb.SaleId);                            //Fail

            }
        }

        [Test]
        public void DeleteSale_Given_ValidId_ReturnsTrue()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "temp_Sales").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                _salesRepository = new EFSalesRepository(context);
                // create 3 sales in db
                _salesRepository.UpdateSale(_sale1);
                _salesRepository.UpdateSale(_sale2);
                _salesRepository.UpdateSale(_sale3);

                var validId = 2;
                var wasSaleFound = _salesRepository.DeleteSale(validId);
                Assert.True(wasSaleFound);          //Pass
                //Assert.False(wasSaleFound);       //Fail
            }
        }

        [Test]
        public void DeleteSale_Given_ValidId_DeletesSale()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "temp_Sales").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                _salesRepository = new EFSalesRepository(context);
                // create 3 sales in db
                _salesRepository.UpdateSale(_sale1);
                _salesRepository.UpdateSale(_sale2);
                _salesRepository.UpdateSale(_sale3);

                var validId = 2;

                var wasSaleFound = _salesRepository.DeleteSale(validId);
                var saleFromDb = context.Sales.FirstOrDefault(s=> s.SaleId==validId);
                Assert.IsNull(saleFromDb);                          //Pass

                var salesListFromDb = _salesRepository.GetSales();
                //Assert.AreEqual(3, salesListFromDb.Count());       //Fail
            }
        }

        [Test]
        public void DeleteSale_Given_InValidId_ReturnsFalse()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "temp_Sales").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                _salesRepository = new EFSalesRepository(context);
                // create 3 sales in db
                _salesRepository.UpdateSale(_sale1);
                _salesRepository.UpdateSale(_sale2);
                _salesRepository.UpdateSale(_sale3);

                var inValidId = 5;
                var wasSaleFound = _salesRepository.DeleteSale(inValidId);
                Assert.False(wasSaleFound);          //Pass
                //Assert.True(wasSaleFound);         //Fail
            }
        }

        
    }
}
