using GuildCars.Data.Factories;
using GuildCars.Models.Tables;
using GuildCars.UI.Models;
using GuildCars.UI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    [Authorize(Roles = "Sales")]
    public class SalesController : Controller
    {
        // GET: Sales
        public ActionResult Index_Sales()
        {
            ViewBag.Message = "Search All Inventory page.";
            return View();
        }

        public ActionResult PurchaseVehicle_Sales(int id)
        {
            var model = new PurchaseVehicleViewModel();

            var vehiclesRepo = VehicleRepoFactory.GetRepo();
            var statesRepo = StateRepoFactory.GetRepo();
            var paymentTypeRepo = PaymentTypeRepoFactory.GetRepo();

            model.State = new SelectList(statesRepo.GetAll(), "StateId", "StateId");
            model.PaymentType = new SelectList(paymentTypeRepo.GetAll(), "PaymentTypeId", "PayType");
            model.Vehicle = vehiclesRepo.GetVehicleViewObjectById(id);

            model.Purchase = new Purchase();

            return View(model);
        }

        [HttpPost]
        public ActionResult PurchaseVehicle_Sales(PurchaseVehicleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repo = PurchaseRepoFactory.GetRepo();
                try
                {
                    model.Purchase.UserId = AuthorizeUtilities.GetUserId(this);
                    repo.InsertNewPurchase(model.Purchase);
                    TempData["Success"] = "Purchase Successfully Completed! Please reivew the details, below:";
                    return RedirectToAction("ConfirmPurchase_Sales", new { id = model.Purchase.PurchaseId});
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                var vehiclesRepo = VehicleRepoFactory.GetRepo();
                var statesRepo = StateRepoFactory.GetRepo();
                var paymentTypeRepo = PaymentTypeRepoFactory.GetRepo();

                model.State = new SelectList(statesRepo.GetAll(), "StateId", "StateId");
                model.PaymentType = new SelectList(paymentTypeRepo.GetAll(), "PaymentTypeId", "PayType");
                model.Vehicle = vehiclesRepo.GetVehicleViewObjectById(model.Vehicle.VehicleId);

                return View(model);
            }
        }

        public ActionResult ConfirmPurchase_Sales(int id)
        {
            var model = new PurchaseVehicleViewModel();

            var vehiclesRepo = VehicleRepoFactory.GetRepo();
            var purchasesRepo = PurchaseRepoFactory.GetRepo();

            model.confirmedPurchase = purchasesRepo.GetPurchaseById(id);

            if (model.confirmedPurchase == null)
            {
                throw new Exception("That Purchase ID does not match any purchse record.");
            }
            model.Vehicle = vehiclesRepo.GetVehicleViewObjectById(model.confirmedPurchase.VehicleId);

            return View(model);
        }


        public ActionResult Contacts_Sales(string vinNumber)
        {
            var model = new ContactViewModel();

            var contactsRepo = ContactRepoFactory.GetRepo();
            model.Contacts = contactsRepo.GetAllContacts();
            model.Contact = new Contact();

            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteContact_Sales(int contactId)
        {

            var repo = ContactRepoFactory.GetRepo();
            repo.DeleteContact(contactId);
            TempData["Success"] = "Message Deleted Successfully!";
            return RedirectToAction("Contacts_Sales");
        }
    }
}