using Microsoft.AspNetCore.Mvc;
using Practice5_DataAccess.Data;
using Practice5_DataAccess.Data.RepositoryFactory;
using Practice5_DataAccess.Interface;
using Practice5_Model.Models;
using Practice5_WebAPI.Filters.AuthFilters;

namespace Practice5_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [JwtTokenAuthFilter]
    public class PurchaseController : ControllerBase
    {
        private IRepositoryPurchases PurchasesRepository;

        public PurchaseController(IRepositoryPurchasesFactory repositoryPurchasesFactory)
        {
            PurchasesRepository = repositoryPurchasesFactory.GetPurchasesRepository();
        }

        [HttpGet]
        public IActionResult GetPurchases()
        {
            return Ok(PurchasesRepository.GetPurchases());
        }

        [HttpGet("{id}")]
        public IActionResult GetPurchaseById(int id)
        {
            return Ok(PurchasesRepository.GetPurchaseById(id));
        }
        [HttpPut("{id}")]
        public IActionResult UpdatePurchase(int id, [FromBody] Purchase Purchase)
        {
            Purchase.PurchaseId = id;
            PurchasesRepository.UpdatePurchase(Purchase);
            return Ok();
        }
        [HttpPost]
        public IActionResult CreatePurchase([FromBody] Purchase Purchase)
        {
            PurchasesRepository.CreatePurchase(Purchase);
            return CreatedAtAction(nameof(GetPurchaseById),
                new { id = Purchase.PurchaseId },
                Purchase);
        }
        [HttpDelete("{id}")]
        public IActionResult DeletePurchase(int id)
        {
            bool isObjectFound = PurchasesRepository.DeletePurchase(id);
            if (!isObjectFound)
            {
                return NotFound();
            }
            return Ok();
        }

    }
}
