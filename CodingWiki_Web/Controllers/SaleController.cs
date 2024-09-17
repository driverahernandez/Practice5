using Practice5_DataAccess.Data;
using Practice5_Model.Models;
using Microsoft.AspNetCore.Mvc;
using Practice5_DataAccess.Interface;
using Practice5_DataAccess.Data.AdoRepositories;
using Practice5_DataAccess.Data.EfRepositories;

namespace Practice5_Web.Controllers
{
    public class SaleController : Controller
    {
        //private readonly ApplicationDbContext _db;
        IRepositorySales SalesRepository;
        public SaleController()
        {
            //_db = db;
            var access_id = 1;
            if (access_id == 0)
                SalesRepository = new EFSalesRepository();
            else
                SalesRepository = new ADOSalesRepository();
        }

        public IActionResult Index()
        {
            return View(SalesRepository.GetSales());
        }
        public IActionResult Upsert(int? id)
        {
            Sale obj = SalesRepository.UpdateSale(id);
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
                SalesRepository.UpdateSale(obj);
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        public IActionResult Delete(int? id)
        {
            bool isObjectFound = SalesRepository.DeleteSale(id);
            if (!isObjectFound)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }
    }
}
