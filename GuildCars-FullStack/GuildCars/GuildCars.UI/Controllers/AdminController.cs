using GuildCars.Data.Factories;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using GuildCars.UI.Models;
using GuildCars.UI.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        // GET: Admin
        public ActionResult Vehicles_Admin()
        {
            ViewBag.Message = "Search All Inventory page.";
            return View();
        }
        public ActionResult Specials_Admin()
        {
            var model = new SpecialsAdminViewModel();

            var specialsRepo = SpecialRepoFactory.GetRepo();
            model.Specials = specialsRepo.GetAll();
            model.Special = new Special();

            return View(model);
        }

        [HttpPost]
        public ActionResult Specials_Admin(SpecialsAdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repo = SpecialRepoFactory.GetRepo();

                try
                {
                    if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                    {
                        var savepath = Server.MapPath("~/ImageSpecials");

                        string fileName = Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);
                        string extension = Path.GetExtension(model.ImageUpload.FileName);

                        var filePath = Path.Combine(savepath, fileName + extension);

                        int counter = 1;
                        while (System.IO.File.Exists(filePath))
                        {
                            filePath = Path.Combine(savepath, fileName + counter.ToString() + extension);
                            counter++;
                        }

                        model.ImageUpload.SaveAs(filePath);

                        model.Special.SpecialImageFileName = Path.GetFileName(filePath);
                    }

                    TempData["Success"] = "New Special Added Successfully!";
                    repo.SpecialsInsert(model.Special);
                    return RedirectToAction("Specials_Admin", new { id = model.Special.SpecialId});
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                var specialsRepo = SpecialRepoFactory.GetRepo();
                model.Specials = specialsRepo.GetAll();
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Delete_Special(int specialId)
        {


            var repo = SpecialRepoFactory.GetRepo();

            var currentSpecial = repo.GetSpecialImageById(specialId);


            var savepath = Server.MapPath("~/ImageSpecials");

            //delete image
            var oldPath = Path.Combine(savepath, currentSpecial.SpecialImageFileName);
            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }

            repo.SpecialsDelete(specialId);
            TempData["Success"] = "Special Deleted Successfully!";
            return RedirectToAction("Specials_Admin");
        }

        public ActionResult AddVehicle_Admin()
        {
            var model = new VehicleAddViewModel();

            var carMakesRepo = CarMakeRepoFactory.GetRepo();
            var carModelsRepo = CarModelRepoFactory.GetRepo();
            var transmissionsRepo = TransmissionRepoFactory.GetRepo();
            var bodyTypeRepo = BodyTypeRepoFactory.GetRepo();
            var colorRepo = ColorRepoFactory.GetRepo();

            model.CarMake = new SelectList(carMakesRepo.GetAll(), "CarMakeId", "CarMakeName");
            model.CarModel = new SelectList(carModelsRepo.GetAll(), "CarModelId", "CarModelName");
            model.Transmission = new SelectList(transmissionsRepo.GetAll(), "TransmissionTypeId", "TransmissionType");
            model.CarBody = new SelectList(bodyTypeRepo.GetAll(), "CarBodyTypeId", "CarBodyType");
            model.Color = new SelectList(colorRepo.GetAll(), "ColorTypeId", "ColorType");
            model.Vehicle = new Vehicle();

            return View(model);
        }
        [HttpPost]
        public ActionResult AddVehicle_Admin(VehicleAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repo = VehicleRepoFactory.GetRepo();

                try
                {
                    if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                    {
                        var savepath = Server.MapPath("~/Images");

                        string fileName = Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);
                        string extension = Path.GetExtension(model.ImageUpload.FileName);

                        var filePath = Path.Combine(savepath, fileName + extension);

                        int counter = 1;                        
                        while (System.IO.File.Exists(filePath))
                        {
                            filePath = Path.Combine(savepath, fileName + counter.ToString() + extension);
                            counter++;
                        }

                        model.ImageUpload.SaveAs(filePath);

                        model.Vehicle.VehicleImageFileName = Path.GetFileName(filePath);
                    }

                    repo.InsertNewVehicle(model.Vehicle);

                    TempData["Success"] = "New Vehicle Added Successfully!";
                    return RedirectToAction("EditVehicle_Admin", new { id = model.Vehicle.VehicleId });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                var carMakesRepo = CarMakeRepoFactory.GetRepo();
                var carModelsRepo = CarModelRepoFactory.GetRepo();
                var transmissionsRepo = TransmissionRepoFactory.GetRepo();
                var bodyTypeRepo = BodyTypeRepoFactory.GetRepo();
                var colorRepo = ColorRepoFactory.GetRepo();

                model.CarMake = new SelectList(carMakesRepo.GetAll(), "CarMakeId", "CarMakeName");
                model.CarModel = new SelectList(carModelsRepo.GetAll(), "CarModelId", "CarModelName");
                model.Transmission = new SelectList(transmissionsRepo.GetAll(), "TransmissionTypeId", "TransmissionType");
                model.CarBody = new SelectList(bodyTypeRepo.GetAll(), "CarBodyTypeId", "CarBodyType");
                model.Color = new SelectList(colorRepo.GetAll(), "ColorTypeId", "ColorType");

                return View(model);
            }
        }

        public ActionResult EditVehicle_Admin(int id)
        {
            var model = new VehicleEditViewModel();
            var vehiclesRepo = VehicleRepoFactory.GetRepo();
            model.Vehicle = vehiclesRepo.GetVehicleTableObjectById(id);
            try
            {
                if (string.IsNullOrEmpty(model.Vehicle.VinNumber))
                {
                    TempData["Fail"] = "Requested vehicle doesn't exist";
                    return RedirectToAction("Vehicles_Admin");
                }

            }
            catch (Exception)
            {
                TempData["Fail"] = "Requested vehicle doesn't exist";
                return RedirectToAction("Vehicles_Admin");
            }


            var carMakesRepo = CarMakeRepoFactory.GetRepo();
            var carModelsRepo = CarModelRepoFactory.GetRepo();
            var transmissionsRepo = TransmissionRepoFactory.GetRepo();
            var bodyTypeRepo = BodyTypeRepoFactory.GetRepo();
            var colorRepo = ColorRepoFactory.GetRepo();

            model.CarMake = new SelectList(carMakesRepo.GetAll(), "CarMakeId", "CarMakeName");
            model.CarModel = new SelectList(carModelsRepo.GetAll(), "CarModelId", "CarModelName");
            model.Transmission = new SelectList(transmissionsRepo.GetAll(), "TransmissionTypeId", "TransmissionType");
            model.CarBody = new SelectList(bodyTypeRepo.GetAll(), "CarBodyTypeId", "CarBodyType");
            model.Color = new SelectList(colorRepo.GetAll(), "ColorTypeId", "ColorType");


            return View(model);
        }

        [HttpPost]
        public ActionResult EditVehicle_Admin(VehicleEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repo = VehicleRepoFactory.GetRepo();

                try
                {
                    var oldVehicle = repo.GetVehicleTableObjectById(model.Vehicle.VehicleId);

                    if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                    {
                        var savepath = Server.MapPath("~/Images");

                        string fileName = Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);
                        string extension = Path.GetExtension(model.ImageUpload.FileName);

                        var filePath = Path.Combine(savepath, fileName + extension);

                        int counter = 1;
                        while (System.IO.File.Exists(filePath))
                        {
                            filePath = Path.Combine(savepath, fileName + counter.ToString() + extension);
                            counter++;
                        }

                        model.ImageUpload.SaveAs(filePath);
                        model.Vehicle.VehicleImageFileName = Path.GetFileName(filePath);

                        //delete old file
                        var oldPath = Path.Combine(savepath, oldVehicle.VehicleImageFileName);
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }
                    else
                    {
                        //didn't replace old file, so keep the file name
                        model.Vehicle.VehicleImageFileName = oldVehicle.VehicleImageFileName;
                    }

                    repo.UpdateVehicle(model.Vehicle);

                    TempData["Success"] = "Vehicle Updated Successfully!";
                    return RedirectToAction("EditVehicle_Admin", new { id = model.Vehicle.VehicleId });
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {

                var carMakesRepo = CarMakeRepoFactory.GetRepo();
                var carModelsRepo = CarModelRepoFactory.GetRepo();
                var transmissionsRepo = TransmissionRepoFactory.GetRepo();
                var bodyTypeRepo = BodyTypeRepoFactory.GetRepo();
                var colorRepo = ColorRepoFactory.GetRepo();

                model.CarMake = new SelectList(carMakesRepo.GetAll(), "CarMakeId", "CarMakeName");
                model.CarModel = new SelectList(carModelsRepo.GetAll(), "CarModelId", "CarModelName");
                model.Transmission = new SelectList(transmissionsRepo.GetAll(), "TransmissionTypeId", "TransmissionType");
                model.CarBody = new SelectList(bodyTypeRepo.GetAll(), "CarBodyTypeId", "CarBodyType");
                model.Color = new SelectList(colorRepo.GetAll(), "ColorTypeId", "ColorType");

                return View(model);
            }
        }

        [HttpPost]
        public ActionResult DeleteVehicle_Admin(int vehicleId)
        {

            var repo = VehicleRepoFactory.GetRepo();
            var currentVehicle = repo.GetVehicleTableObjectById(vehicleId);

            if (currentVehicle.IsPurchasedCar == true)
            {
                TempData["Success"] = "Vehicle already sold and cannot be deleted!";

                return RedirectToAction("Vehicles_Admin");
            }
            else
            {
                var oldVehicle = repo.GetVehicleTableObjectById(vehicleId);

                var savepath = Server.MapPath("~/Images");
                              
                //delete old file
                var oldPath = Path.Combine(savepath, oldVehicle.VehicleImageFileName);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }

                repo.DeleteVehicle(vehicleId);
                TempData["Success"] = "Vehicle Deleted Successfully!";
                return RedirectToAction("Vehicles_Admin");
            }
        }

        public ActionResult Makes_Admin()
        {
            var model = new CarMakeAddViewModel();

            var carMakesRepo = CarMakeRepoFactory.GetRepo();
            model.CarMakes = carMakesRepo.GetAll();
            model.CarMake = new CarMake();

            return View(model);
        }

        [HttpPost]
        public ActionResult Makes_Admin(CarMakeAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repo = CarMakeRepoFactory.GetRepo();

                try
                {
                    model.CarMake.UserId = AuthorizeUtilities.GetUserId(this);
                    bool addSuccessful = repo.AddMakeIfNotDuplicate(model.CarMake);
                    if (addSuccessful)
                    {
                        TempData["Success"] = "New Car Make Added Successfully!";
                        return RedirectToAction("Makes_Admin");
                    }
                    else
                    {
                        TempData["Fail"] = "Add not successful, perhaps that one already exists? Check your database!";
                        return RedirectToAction("Makes_Admin");
                    }
                }
                catch (Exception)
                {
                    TempData["Fail"] = "Add not successful, perhaps that one already exists? Check your database!";
                    return RedirectToAction("Makes_Admin");
                }
            }
            else
            {
                var carMakesRepo = CarMakeRepoFactory.GetRepo();
                model.CarMakes = carMakesRepo.GetAll();
                return View(model);
            }
        }

        public ActionResult Models_Admin()
        {
            var model = new CarModelAddViewModel();

            var carMakesRepo = CarMakeRepoFactory.GetRepo();
            var carModelsRepo = CarModelRepoFactory.GetRepo();
            model.CarMakes = new SelectList(carMakesRepo.GetAll(), "CarMakeId", "CarMakeName");
            model.CarModels = carModelsRepo.GetAll();
            model.CarModel = new CarModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Models_Admin(CarModelAddViewModel model)
        {
            var repo = CarModelRepoFactory.GetRepo();
            bool isDuplicateModelName = repo.IsDuplicateCarModel(model.CarModel.CarModelName, model.CarModel.CarMakeId);

            if (ModelState.IsValid && !isDuplicateModelName)
            {
                try
                {
                    model.CarModel.UserId = AuthorizeUtilities.GetUserId(this);
                    repo.Insert(model.CarModel);
                    TempData["Success"] = "New Car Model Added Successfully!";
                    return RedirectToAction("Models_Admin");
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            else
            {
                var carMakesRepo = CarMakeRepoFactory.GetRepo();
                var carModelsRepo = CarModelRepoFactory.GetRepo();
                model.CarMakes = new SelectList(carMakesRepo.GetAll(), "CarMakeId", "CarMakeName");
                model.CarModels = carModelsRepo.GetAll();

                TempData["Fail"] = "Cannot add that model. It might be a duplicate.";
                return View(model);
            }
        }

        public ActionResult Users_Admin()
        {
            var model = UserRepoFactory.GetRepo().GetUserList();
            return View(model);
        }

        // GET: /Account/Register
        public ActionResult AddUser_Admin()
        {
            var model = new UserAddViewModel();
            var usersRepo = UserRepoFactory.GetRepo();
            model.AspNetRoles = new SelectList(usersRepo.GetAspNetRoles(), "Id", "Name");
            return View(model);
        }
        
        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public async Task<ActionResult> AddUser_Admin(UserAddViewModel model)
        {
            var usersRepo = UserRepoFactory.GetRepo();
            model.AspNetRoles = new SelectList(usersRepo.GetAspNetRoles(), "Id", "Name");

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { FirstName = model.FirstName, LastName = model.LastName, UserRoleId = model.RoleId, UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var userId = user.Id;
                    var roleId = user.UserRoleId;
                    usersRepo.UpdateUserRole(userId, roleId);

                    //await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    TempData["Success"] = "New User Added Successfully!";
                    return RedirectToAction("EditUser_Admin", new { id = user.Id});
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult EditUser_Admin(string id)
        {
            var model = new UserEditViewModel();
            model.user = new ApplicationUser();
            model.user = UserManager.FindById(id);

            var rolesRepo = UserRepoFactory.GetRepo();
            model.AspNetRoles = new SelectList(rolesRepo.GetAspNetRoles(), "Id", "Name");
            model.Email = model.user.Email;
            
            model.FirstName = model.user.FirstName;
            model.LastName = model.user.LastName;
            model.RoleId = model.user.UserRoleId;

            return View(model);
        }

        [HttpPost]
        public ActionResult EditUser_Admin(UserEditViewModel model)
        {
            var usersRepo = UserRepoFactory.GetRepo();
            if (ModelState.IsValid)
            {
                try
                {
                    var currentUser = UserManager.FindById(model.user.Id);
                    currentUser.UserName = model.Email;
                    currentUser.Email = model.Email;
                    currentUser.FirstName = model.FirstName;
                    currentUser.LastName = model.LastName;
                    currentUser.UserRoleId = model.RoleId;

                    var result = UserManager.Update(currentUser);
                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(model.Password))
                        {
                            UserManager.RemovePassword(currentUser.Id);
                            UserManager.AddPassword(currentUser.Id, model.Password);
                        }
                        usersRepo.UpdateUserRole(currentUser.Id, currentUser.UserRoleId);

                        model.modelIsValid = true;

                        //await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);

                        // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                        TempData["Success"] = "Updated Successfully!";
                        return RedirectToAction("EditUser_Admin", new { id = model.user.Id});
                    }
                    AddErrors(result);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                
            }

            model.AspNetRoles = new SelectList(usersRepo.GetAspNetRoles(), "Id", "Name");

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //Helpers
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }

        }
    }
}