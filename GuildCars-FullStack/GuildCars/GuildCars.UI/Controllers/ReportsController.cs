using GuildCars.Data.Factories;
using GuildCars.Models.Queries;
using GuildCars.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Index_Reports()
        {
            ViewBag.Message = "Choose reports to view.";
            return View();
        }

        public ActionResult SalesReports_Reports()
        {
            ViewBag.Message = "Displays Current Sales Reports.";
            var model = new SalesReportViewModel();

            var salesRepo = UserRepoFactory.GetRepo();
            model.UserWithSales = new SelectList(salesRepo.GetUsersWithSales(), "UserId", "UserName");
            return View(model);
        }
        public ActionResult InventoryReports_Reports()
        {
            ViewBag.Message = "Displays Current Inventory Reports.";
            var model = new InventoryReportViewModel();

            var inventoryRepo = VehicleRepoFactory.GetRepo();
            model.NewVehicleInventory = inventoryRepo.GetNewVehicleInventoryReport();
            model.UsedVehicleInventory = inventoryRepo.GetUsedVehicleInventoryReport();
            return View(model);
        }
    }
}