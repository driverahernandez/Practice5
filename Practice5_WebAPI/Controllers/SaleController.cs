using Microsoft.AspNetCore.Mvc;
using Practice5_DataAccess.Data;
using Practice5_DataAccess.Interface;
using Practice5_Model.Models;

namespace Practice5_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaleController : ControllerBase
    {
        private IRepositorySales SalesRepository;

        public SaleController(IRepositorySalesFactory repositorySalesFactory)
        {
            SalesRepository = repositorySalesFactory.GetSalesRepository();
        }

        [HttpGet]
        public IActionResult GetSales()
        {
            return Ok(SalesRepository.GetSales());
        }

        [HttpGet("{id}")]
        public IActionResult GetSaleById(int id)
        {
            return Ok(SalesRepository.GetSaleById(id));
        }
        [HttpPut("{id}")]
        public IActionResult UpdateSale(int id, [FromBody] Sale sale) 
        {
            sale.SaleId = id;
            SalesRepository.UpdateSale(sale);
            return Ok();
        }
        [HttpPost]
        public IActionResult CreateSale([FromBody] Sale sale) 
        {
            SalesRepository.CreateSale(sale);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteSale(int id)
        {
            bool isObjectFound = SalesRepository.DeleteSale(id);
            if (!isObjectFound)
            {
                return NotFound();
            }
            return Ok();
        }

    }
}
