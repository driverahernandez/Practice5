
using Practice5_Model.Models;
using Microsoft.AspNetCore.Mvc;
using Practice5_Web.Data;

namespace Practice5_Web.Controllers
{
    public class SaleController : Controller
    {
        public readonly IWebApiExecuter WebApiExecuter;

        public SaleController(IWebApiExecuter webApiExecuter)
        {
            WebApiExecuter = webApiExecuter;
        }

        public async Task<IActionResult> Index()
        {
            var result = await WebApiExecuter.InvokeGet<List<Sale>>("/api/Sale");
            return View(result);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Sale obj = new Sale();
            if (id != null)
                obj = await WebApiExecuter.InvokeGet<Sale>($"/api/Sale/{id}");
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Sale obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.SaleId == 0){
                    await WebApiExecuter.InvokePost<Sale>("/api/Sale", obj);

                    return RedirectToAction("Index");
                }


                else
                {
                    await WebApiExecuter.InvokePut<Sale>($"/api/Sale/{obj.SaleId}", obj);
                    return RedirectToAction("Index");
                }
            }
            return View(obj);

        }

        public async Task<IActionResult> Delete(int? id)
        {
            await WebApiExecuter.InvokeDelete($"/api/Sale/{id}");
            return RedirectToAction("Index");
        }
    }
}
