using Practice5_DataAccess.Data;
using Practice5_Model.Models;
using Microsoft.AspNetCore.Mvc;
using Practice5_DataAccess.Data.AdoRepositories;
using Practice5_DataAccess.Interface;
using Practice5_DataAccess.Data.EfRepositories;
using Practice5_Web.Data;

namespace Practice5_Web.Controllers
{
    public class ProductController : Controller
    {
        public readonly IWebApiExecuter WebApiExecuter;

        public ProductController(IWebApiExecuter webApiExecuter)
        {
            WebApiExecuter = webApiExecuter;
        }

        public async Task<IActionResult> Index()
        {
            var result = await WebApiExecuter.InvokeGet<List<Product>>("/api/Product");
            return View(result);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Product obj = new Product();
            if (id != null)
                obj = await WebApiExecuter.InvokeGet<Product>($"/api/Product/{id}");
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Product obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.ProductId == 0)
                {
                    await WebApiExecuter.InvokePost<Product>("/api/Product", obj);

                    return RedirectToAction("Index");
                }


                else
                {
                    await WebApiExecuter.InvokePut<Product>($"/api/Product/{obj.ProductId}", obj);
                    return RedirectToAction("Index");
                }
            }
            return View(obj);

        }

        public async Task<IActionResult> Delete(int? id)
        {
            await WebApiExecuter.InvokeDelete($"/api/Product/{id}");
            return RedirectToAction("Index");
        }

    }
}
