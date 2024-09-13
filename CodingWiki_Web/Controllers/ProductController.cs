using Practice5_DataAccess.Data;
using Practice5_Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace Practice5_Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Product> objList = _db.Products.ToList();
            return View(objList);
        }
        public IActionResult Upsert(int? id)
        {
            Product obj = new();
            if (id == null || id == 0)
            {
                //create 
                return View(obj);
            }
            //edit 
            obj = _db.Products.FirstOrDefault(g => g.ProductId == id);
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
                if (obj.ProductId == 0)
                {
                    //create
                    _db.Products.Add(obj);

                }
                else
                {
                    //update
                    _db.Products.Update(obj);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        public IActionResult Delete(int? id)
        {
            Product obj = new();
            obj = _db.Products.FirstOrDefault(g => g.ProductId == id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Products.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
