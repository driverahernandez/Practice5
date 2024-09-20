using Moq;
using Practice5_DataAccess; 
using Practice5_Web;
using Practice5_DataAccess.Interface;
using Practice5_Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Practice5_Model.Models;


namespace Practice5.Test.WebTests
{
    public class SalesControllerTests : IDisposable
    {
        public IRepositorySales _salesRepository;
        public SaleController _saleController;

        public int invalidId = 10;
        public int validId = 1; 

        [SetUp]
        public void Setup()
        {
            var mockSalesRepository = new Mock<IRepositorySales>();

            List<Sale> salesList = new List<Sale> { 
                new Sale{SaleId = 1, ProductId = 2, SaleDate = new DateOnly(2024, 9, 18), Total = 1.1},
                new Sale{SaleId = 2, ProductId = 2, SaleDate = new DateOnly(2024, 9, 19), Total = 1.2},
                new Sale{SaleId = 3, ProductId = 4, SaleDate = new DateOnly(2024, 9, 20), Total = 1.3}  };

            mockSalesRepository.Setup(s => s.GetSales()).Returns(salesList);

            // when UpdateSale method receives null or 0, it returns a new Sale object
            mockSalesRepository.Setup(s => s.UpdateSale(0)).Returns(new Sale());
            // when UpdateSale method receives a valid id, it returns the existing Sale object
            // could use It.Is<int>(t => t >= 1 && t <= 3) to pass an int between 1 and 3
            mockSalesRepository.Setup(s => s.UpdateSale(validId)).Returns(salesList[0]);
            // when UpdateSale method receives an invalid id, it returns null 
            mockSalesRepository.Setup(s => s.UpdateSale(invalidId)).Returns((Sale)null);
            
            // when DeleteSale method receives a valid id, it returns true
            mockSalesRepository.Setup(s => s.DeleteSale(1)).Returns(true);
            // when DeleteSale method receives an invalid id, it returns false
            mockSalesRepository.Setup(s => s.DeleteSale(4)).Returns(false);


            var _salesFactory = new Mock<IRepositorySalesFactory>();
            _salesFactory.Setup(s => s.GetSalesRepository()).Returns(mockSalesRepository.Object);

            //_salesRepository = mockSalesRepository.Object; 
            _saleController = new SaleController(_salesFactory.Object);
        }

        // test for CREATE action
        [Test]
        [Category("CREATE")]
        public void Upsert_Given_IdZero_ReturnsViewOfNewSale()
        {
            var result = _saleController.Upsert(0) as ViewResult;
            Assert.IsInstanceOf<Sale>(result.Model);              //Pass
            var newSale = result.Model as Sale;
            Assert.That(newSale.SaleId, Is.EqualTo(0));           //Pass
            //Assert.That(newSale.SaleId, Is.EqualTo(invalidId)); //Fail
        }

        // test for READ action
        [Test]
        [Category("READ")]
        public void Index_ReturnsViewWithSalesList()
        {
            var result = _saleController.Index() as ViewResult;
            Assert.NotNull(result);       //Pass
            //Assert.Null(result.Model);  //Fail
        }

         // tests for UPDATE action
        [Test]
        [Category("UPDATE")]
        public void Upsert_Given_InvalidId_ReturnsNotFound()
        {
            var result = _saleController.Upsert(invalidId);
            Assert.IsInstanceOf<NotFoundResult>(result);    //Pass
            //Assert.IsInstanceOf<ViewResult>(result);      //Fail
        }

        [Test]
        [Category("UPDATE")]
        public void Upsert_Given_ValidId_ReturnsViewOfSale()
        {
            var result = _saleController.Upsert(validId);

            Assert.IsInstanceOf<ViewResult>(result);        //Pass
            var viewresult = result as ViewResult;
            Assert.IsInstanceOf<Sale>(viewresult.Model);    //Pass
            //Assert.IsInstanceOf<NotFoundResult>(result);  //Fail
        }

        [Test]
        [Category("DELETE")]
        public void Delete_Given_InvalidId_ReturnsNotFound()
        {
            var result = _saleController.Delete(invalidId);
            Assert.IsInstanceOf<NotFoundResult>(result);    //Pass
            //Assert.IsInstanceOf<ViewResult>(result);      //Fail
        }

        [Test]
        [Category("DELETE")]
        public void Delete_Given_ValidId_ReturnsViewOfSale()
        {
            var result = _saleController.Upsert(validId);

            Assert.IsInstanceOf<ViewResult>(result);        //Pass
            var viewresult = result as ViewResult;
            Assert.IsInstanceOf<Sale>(viewresult.Model);    //Pass
            //Assert.IsInstanceOf<NotFoundResult>(result);  //Fail
        }

        public void Dispose()
        {
            //
        }
    }
}