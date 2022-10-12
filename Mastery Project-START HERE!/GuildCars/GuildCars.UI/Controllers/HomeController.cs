using GuildCars.Data.Factories;
using GuildCars.Models.Tables;
using GuildCars.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new HomeIndexViewModel();

            var vehiclesRepo = VehicleRepoFactory.GetRepo();
            var specialsRepo = SpecialRepoFactory.GetRepo();

            model.FeaturedVehicles = vehiclesRepo.GetFeaturedVehicles();
            model.Specials = specialsRepo.GetAll();
            return View(model);

        }

        public ActionResult Contact(string vinNumber, string carMake, string carModel, string carYear)
        {
            var model = new ContactViewModel();
            model.Contact = new Contact();

            var messageText = "Inquiring on the " + carYear + " " + carMake + ", " + carModel + ", " + "(Vin no. " + vinNumber + ")... ";
            if (!string.IsNullOrEmpty(vinNumber))
            {
                model.Contact.ContactMessage = messageText;
            }
            model.MapKey = "ApNyjup5SzCK-IM2Uzlyw_1mzppU-_mLoxIOc9_Mt29PD5HgFWLaXiTgVEmYcu_F";

            return View(model);
        }
        [HttpPost]
        public ActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repo = ContactRepoFactory.GetRepo();

                try
                {                    
                    repo.InsertNewContact(model.Contact); TempData["Success"] = "Message sent successfully! We'll reach out as soon as possible!";
                    return RedirectToAction("Contact");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                model.Contact = new Contact();
                return View(model);
            }
        }

        public ActionResult Specials()
        {
            var model = SpecialRepoFactory.GetRepo().GetAll();
            return View(model);
        }
    }
}