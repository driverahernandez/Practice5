using Practice5_DataAccess.Data;
using Practice5_Model.Models;
using Microsoft.AspNetCore.Mvc;
using Practice5_DataAccess.Interface;
using Practice5_DataAccess.Data.AdoRepositories;
using Practice5_DataAccess.Data.EfRepositories;

namespace Practice5_Web.Controllers
{
    public class AccessTypeController : Controller
    {
        public AccessTypeController()
        {
        }

        public IActionResult Index()
        {
            return View(AccessType.getAvailableTypes());
        }
        public IActionResult Set(int id)
        {
            if (id == 0)
            {
                AccessType.setAsEf();
            }
            else
            {
               AccessType.setAsAdo();
            }
            return RedirectToAction("Index");
        }
    }
}
