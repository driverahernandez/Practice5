using Practice5_DataAccess.Data;
using Practice5_Model.Models;
using Microsoft.AspNetCore.Mvc;
using Practice5_DataAccess.Data.AdoRepositories;
using Practice5_DataAccess.Interface;
using Practice5_DataAccess.Data.EfRepositories;

namespace Practice5_Web.Controllers
{
    public class ProductController : Controller
    {
        IRepositoryProducts ProductsRepository;
        public ProductController()
        {
            if (AccessType.id == 0)
                ProductsRepository = new EFProductsRepository();
            else
                ProductsRepository = new ADOProductsRepository();
        }

        public IActionResult Index()
        {
            return View(ProductsRepository.GetProducts());
        }
        public IActionResult Upsert(int? id)
        {
            Product obj = ProductsRepository.UpdateProduct(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Product obj)
        {
            if (ModelState.IsValid)
            {
                ProductsRepository.UpdateProduct(obj);
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        public IActionResult Delete(int? id)
        {
            bool isObjectFound = ProductsRepository.DeleteProduct(id);
            if (!isObjectFound)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }
        
    }
}
