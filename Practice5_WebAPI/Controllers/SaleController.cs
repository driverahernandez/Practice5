using Microsoft.AspNetCore.Mvc;
using Practice5_DataAccess.Data.RepositoryFactory;
using Practice5_DataAccess.Interface;
using Practice5_Model.Models;
using Practice5_WebAPI.Filters.AuthFilters;

namespace Practice5_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [JwtTokenAuthFilter]
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
            return CreatedAtAction(nameof(GetSaleById),
                new { id = sale.SaleId },
                sale);
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
