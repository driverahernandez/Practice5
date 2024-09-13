using Practice5_DataAccess.Data;
using Practice5_Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace Practice5_Web.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly ApplicationDbContext _db;
        public PurchaseController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Purchase> objList = _db.Purchases.ToList();
            return View(objList);
        }
        public IActionResult Upsert(int? id)
        {
            Purchase obj = new();
            if (id == null || id == 0)
            {
                //create 
                return View(obj);
            }
            //edit 
            obj = _db.Purchases.FirstOrDefault(g => g.PurchaseId == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Purchase obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.PurchaseId == 0)
                {
                    //create
                    _db.Purchases.Add(obj);

                }
                else
                {
                    //update
                    _db.Purchases.Update(obj);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        public IActionResult Delete(int? id)
        {
            Purchase obj = new();
            obj = _db.Purchases.FirstOrDefault(g => g.PurchaseId == id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Purchases.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
