using Microsoft.AspNetCore.Mvc;
using Practice5_DataAccess.Interface;
using Practice5_Model.Models;
using Practice5_DataAccess.Data.RepositoryFactory;
using Practice5_WebAPI.Filters.AuthFilters;

namespace Practice5_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [JwtTokenAuthFilter]
    public class ProductController : ControllerBase
    {
        private IRepositoryProducts ProductsRepository;

        public ProductController(IRepositoryProductsFactory repositoryProductsFactory)
        {
            ProductsRepository = repositoryProductsFactory.GetProductsRepository();
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(ProductsRepository.GetProducts());
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            return Ok(ProductsRepository.GetProductById(id));
        }
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product Product)
        {
            Product.ProductId = id;
            ProductsRepository.UpdateProduct(Product);
            return Ok();
        }
        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product Product)
        {
            ProductsRepository.CreateProduct(Product);
            return CreatedAtAction(nameof(GetProductById),
                new { id = Product.ProductId },
                Product);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            bool isObjectFound = ProductsRepository.DeleteProduct(id);
            if (!isObjectFound)
            {
                return NotFound();
            }
            return Ok();
        }

    }
}
