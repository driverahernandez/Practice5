using Practice5_DataAccess.Data;
using Practice5_Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace Practice5_Web.Controllers
{
    public class ProductInventoryController : Controller
    {
        ////dependency injection
        ////genrecontroller class depends on applicationdbcontext class
        private readonly ApplicationDbContext _db;
        public ProductInventoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<ProductInventory> objList = _db.ProductsInventory.ToList();
            return View(objList);
        }
        public IActionResult Upsert(int? id)
        {
            ProductInventory obj = new();
            if (id == null || id == 0)
            {
                //create 
                return View(obj);
            }
            //edit 
            obj = _db.ProductsInventory.FirstOrDefault(g => g.ProductId == id);
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
                if (obj.ProductId == 0)
                {
                    //create
                    _db.ProductsInventory.Add(obj);

                }
                else
                {
                    //update
                    _db.ProductsInventory.Update(obj);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        public IActionResult Delete(int? id)
        {
            ProductInventory obj = new();
            obj = _db.ProductsInventory.FirstOrDefault(g => g.ProductId == id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.ProductsInventory.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
