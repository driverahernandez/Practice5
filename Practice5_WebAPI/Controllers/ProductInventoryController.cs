using Microsoft.AspNetCore.Mvc;
using Practice5_DataAccess.Data;
using Practice5_DataAccess.Interface;
using Practice5_Model.Models;
using Practice5_DataAccess.Data.RepositoryFactory;

namespace Practice5_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductInventoryController : ControllerBase
    {
        private IRepositoryProductsInventory ProductsInventoryRepository;

        public ProductInventoryController(IRepositoryProductsInventoryFactory repositoryProductsInventoryFactory)
        {
            ProductsInventoryRepository = repositoryProductsInventoryFactory.GetProductsInventoryRepository();
        }

        [HttpGet]
        public IActionResult GetProductsInventory()
        {
            return Ok(ProductsInventoryRepository.GetProductsInventory());
        }

        [HttpGet("{id}")]
        public IActionResult GetProductInventoryById(int id)
        {
            return Ok(ProductsInventoryRepository.GetProductInventoryById(id));
        }
        [HttpPut("{id}")]
        public IActionResult UpdateProductInventory(int id, [FromBody] ProductInventory ProductInventory)
        {
            ProductInventory.ProductId = id;
            ProductsInventoryRepository.UpdateProductInventory(ProductInventory);
            return Ok();
        }
        [HttpPost]
        public IActionResult CreateProductInventory([FromBody] ProductInventory ProductInventory)
        {
            ProductsInventoryRepository.CreateProductInventory(ProductInventory);
            return CreatedAtAction(nameof(GetProductInventoryById),
                new { id = ProductInventory.ProductId },
                ProductInventory);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProductInventory(int id)
        {
            bool isObjectFound = ProductsInventoryRepository.DeleteProductInventory(id);
            if (!isObjectFound)
            {
                return NotFound();
            }
            return Ok();
        }

    }
}
