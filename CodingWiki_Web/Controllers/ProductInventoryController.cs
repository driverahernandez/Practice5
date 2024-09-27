using Practice5_DataAccess.Data;
using Practice5_Model.Models;
using Microsoft.AspNetCore.Mvc;
using Practice5_DataAccess.Data.AdoRepositories;
using Practice5_DataAccess.Interface;
using Practice5_DataAccess.Data.EfRepositories;
using Practice5_Web.Data;

namespace Practice5_Web.Controllers
{
    public class ProductInventoryController : Controller
    {
        public readonly IWebApiExecuter WebApiExecuter;

        public ProductInventoryController(IWebApiExecuter webApiExecuter)
        {
            WebApiExecuter = webApiExecuter;
        }

        public async Task<IActionResult> Index()
        {
            var result = await WebApiExecuter.InvokeGet<List<ProductInventory>>("/api/ProductInventory");
            return View(result);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            ProductInventory obj = new ProductInventory();
            if (id != null)
                obj = await WebApiExecuter.InvokeGet<ProductInventory>($"/api/ProductInventory/{id}");
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProductInventory obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.ProductId == 0)
                {
                    await WebApiExecuter.InvokePost<ProductInventory>("/api/ProductInventory", obj);

                    return RedirectToAction("Index");
                }


                else
                {
                    await WebApiExecuter.InvokePut<ProductInventory>($"/api/ProductInventory/{obj.ProductId}", obj);
                    return RedirectToAction("Index");
                }
            }
            return View(obj);

        }

        public async Task<IActionResult> Delete(int? id)
        {
            await WebApiExecuter.InvokeDelete($"/api/ProductInventory/{id}");
            return RedirectToAction("Index");
        }
    }
}
