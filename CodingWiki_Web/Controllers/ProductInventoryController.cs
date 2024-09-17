using Practice5_DataAccess.Data;
using Practice5_Model.Models;
using Microsoft.AspNetCore.Mvc;
using Practice5_DataAccess.Data.AdoRepositories;
using Practice5_DataAccess.Interface;
using Practice5_DataAccess.Data.EfRepositories;

namespace Practice5_Web.Controllers
{
    public class ProductInventoryController : Controller
    {
        IRepositoryProductsInventory ProductsInventoryRepository;
        public ProductInventoryController()
        {
            //_db = db;
            var access_id = 1;
            if (access_id == 0){
                ProductsInventoryRepository = new EFProductsInventoryRepository();
            }
            else
                ProductsInventoryRepository = new ADOProductsInventoryRepository();
        }

        public IActionResult Index()
        {
            return View(ProductsInventoryRepository.GetProductsInventory());
        }
        public IActionResult Upsert(int? id)
        {
            ProductInventory obj = ProductsInventoryRepository.UpdateProductInventory(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductInventory obj)
        {
            if (ModelState.IsValid)
            {
                ProductsInventoryRepository.UpdateProductInventory(obj);
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        public IActionResult Delete(int? id)
        {
            bool isObjectFound = ProductsInventoryRepository.DeleteProductInventory(id);
            if (!isObjectFound)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }
    }
}
