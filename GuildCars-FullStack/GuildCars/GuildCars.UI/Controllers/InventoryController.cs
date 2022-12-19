using GuildCars.Data.Factories;
using GuildCars.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class InventoryController : Controller
    {
        // GET: Inventory
        public ActionResult Details(int id)
        {
            var repo = VehicleRepoFactory.GetRepo();
            var model = repo.GetVehicleViewObjectById(id);
            try
            {
                if (string.IsNullOrEmpty(model.VinNumber))
                {
                    TempData["Success"] = "Sorry, that vehicle is no longer in our inventory!";
                    return RedirectToAction("Index", "Home");
                }

            }
            catch (Exception)
            {
                TempData["Success"] = "Sorry, that vehicle is no longer in our inventory!";
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public ActionResult New()
        {
            ViewBag.Message = "Your New Inventory page.";
            return View();
        }
        public ActionResult Used()
        {
            ViewBag.Message = "Your Used Inventory page.";
            return View();
        }
    }
}
