using Practice5_DataAccess.Data;
using Practice5_Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace Practice5_Web.Controllers
{
    public class SaleController : Controller
    {
        private readonly ApplicationDbContext _db;
        public SaleController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Sale> objList = _db.Sales.ToList();
            return View(objList);
        }
        public IActionResult Upsert(int? id)
        {
            Sale obj = new();
            if (id == null || id == 0)
            {
                //create 
                return View(obj);
            }
            //edit 
            obj = _db.Sales.FirstOrDefault(g => g.SaleId == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Sale obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.SaleId == 0)
                {
                    //create
                    _db.Sales.Add(obj);

                }
                else
                {
                    //update
                    _db.Sales.Update(obj);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        public IActionResult Delete(int? id)
        {
            Sale obj = new();
            obj = _db.Sales.FirstOrDefault(g => g.SaleId == id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Sales.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
