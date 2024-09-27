using Practice5_DataAccess.Data;
using Practice5_Model.Models;
using Microsoft.AspNetCore.Mvc;
using Practice5_DataAccess.Data.AdoRepositories;
using Practice5_DataAccess.Interface;
using Practice5_DataAccess.Data.EfRepositories;
using Practice5_Web.Data;

namespace Practice5_Web.Controllers
{
    public class PurchaseController : Controller
    {
        public readonly IWebApiExecuter WebApiExecuter;

        public PurchaseController(IWebApiExecuter webApiExecuter)
        {
            WebApiExecuter = webApiExecuter;
        }

        public async Task<IActionResult> Index()
        {
            var result = await WebApiExecuter.InvokeGet<List<Purchase>>("/api/Purchase");
            return View(result);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Purchase obj = new Purchase();
            if (id != null)
                obj = await WebApiExecuter.InvokeGet<Purchase>($"/api/Purchase/{id}");
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Purchase obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.PurchaseId == 0)
                {
                    await WebApiExecuter.InvokePost<Purchase>("/api/Purchase", obj);

                    return RedirectToAction("Index");
                }


                else
                {
                    await WebApiExecuter.InvokePut<Purchase>($"/api/Purchase/{obj.PurchaseId}", obj);
                    return RedirectToAction("Index");
                }
            }
            return View(obj);

        }

        public async Task<IActionResult> Delete(int? id)
        {
            await WebApiExecuter.InvokeDelete($"/api/Purchase/{id}");
            return RedirectToAction("Index");
        }
    }
}
