using Practice5_DataAccess.Data;
using Practice5_Model.Models;
using Microsoft.AspNetCore.Mvc;
using Practice5_DataAccess.Data.AdoRepositories;
using Practice5_DataAccess.Interface;
using Practice5_DataAccess.Data.EfRepositories;

namespace Practice5_Web.Controllers
{
    public class PurchaseController : Controller
    {
        IRepositoryPurchases PurchasesRepository;
        public PurchaseController()
        {
            if (AccessType.id == 0){
                PurchasesRepository = new EFPurchasesRepository();
            }
            else
                PurchasesRepository = new ADOPurchasesRepository();
        }

        public IActionResult Index()
        {
            return View(PurchasesRepository.GetPurchases());
        }
        public IActionResult Upsert(int? id)
        {
            Purchase obj = PurchasesRepository.UpdatePurchase(id);
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
                PurchasesRepository.UpdatePurchase(obj);
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        public IActionResult Delete(int? id)
        {
            bool isObjectFound = PurchasesRepository.DeletePurchase(id);
            if (!isObjectFound)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }
    }
}
