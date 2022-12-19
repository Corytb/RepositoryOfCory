using GuildCars.Data.Repos;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Tests.IntegrationTests
{
    [TestFixture]
    public class GuildCarsTests
    {
        [SetUp]
        public void Init()
        {
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "DbResetGuildCars";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Connection = cn;
                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
        [Test]
        public void CanLoadBodyTypes()
        {

            var repo = new CarBodiesRepo();

            var types = repo.GetAll();

            Assert.AreEqual(4, types.Count);

            Assert.AreEqual(1, types[0].CarBodyTypeId);
            Assert.AreEqual("Car", types[0].CarBodyType);
        }
        [Test]
        public void CanLoadColorTypes()
        {

            var repo = new ColorsRepo();

            var colors = repo.GetAll();

            Assert.AreEqual(5, colors.Count);

            Assert.AreEqual(1, colors[0].ColorTypeId);
            Assert.AreEqual("Red", colors[0].ColorType);
        }
        [Test]
        public void CanLoadPaymentTypes()
        {

            var repo = new PaymentTypesRepo();

            var types = repo.GetAll();

            Assert.AreEqual(3, types.Count);

            Assert.AreEqual(1, types[0].PaymentTypeId);
            Assert.AreEqual("Bank Finance", types[0].PayType);
        }
        [Test]
        public void CanLoadSpecials()
        {

            var repo = new SpecialsRepo();

            var specials = repo.GetAll();

            Assert.AreEqual(6, specials.Count);

            Assert.AreEqual(6, specials[0].SpecialId);
            Assert.AreEqual("Test6", specials[0].SpecialTitle);
            Assert.AreEqual("Test description", specials[0].SpecialDescription);
            Assert.AreEqual("specialImagePlaceholder.png", specials[0].SpecialImageFileName);
        }
        [Test]
        public void CanAddAndDeleteSpecial()
        {
            Special specialToAdd = new Special();
            var repo = new SpecialsRepo();

            specialToAdd.SpecialTitle = "Test8";
            specialToAdd.SpecialDescription = "Test Description";
            specialToAdd.SpecialImageFileName = "testImage8.png";

            repo.SpecialsInsert(specialToAdd);

            var allSpecials = repo.GetAll();
            Assert.AreEqual(7, allSpecials.Count());

            Assert.AreEqual(7, allSpecials[0].SpecialId);
            Assert.AreEqual("Test8", allSpecials[0].SpecialTitle);
            Assert.AreEqual("Test Description", allSpecials[0].SpecialDescription);
            Assert.AreEqual("testImage8.png", allSpecials[0].SpecialImageFileName);

            repo.SpecialsDelete(7);

            allSpecials = repo.GetAll();

            Assert.AreEqual(6, allSpecials.Count());
        }
        [Test]
        public void CanLoadSpecialImagePath()
        {

            var repo = new SpecialsRepo();

            var special = repo.GetSpecialImageById(1);

            Assert.IsNotNull(special);

            Assert.AreEqual("specialImagePlaceholder.png", special.SpecialImageFileName);
        }

        [Test]
        public void CanLoadStates()
        {
            var repo = new StatesRepo();

            var states = repo.GetAll();

            Assert.AreEqual(3, states.Count);

            Assert.AreEqual("KY", states[0].StateId);
            Assert.AreEqual("Kentucky", states[0].StateName);
        }
        [Test]
        public void CanLoadTransmissions()
        {
            var repo = new TransmissionsRepo();

            var transmissions = repo.GetAll();

            Assert.AreEqual(3, transmissions.Count);

            Assert.AreEqual(1, transmissions[0].TransmissionTypeId);
            Assert.AreEqual("Automatic", transmissions[0].TransmissionType);

            Assert.AreEqual(2, transmissions[1].TransmissionTypeId);
            Assert.AreEqual("Manual", transmissions[1].TransmissionType);

            Assert.AreEqual(3, transmissions[2].TransmissionTypeId);
            Assert.AreEqual("NA", transmissions[2].TransmissionType);
        }

        [Test]
        public void CanLoadCarMakes()
        {
            var repo = new CarMakesRepo();

            List<ShowCarMake> carMakes = repo.GetAll().ToList();

            Assert.AreEqual(4, carMakes.Count);

            Assert.AreEqual(1, carMakes[0].CarMakeId);
            Assert.AreEqual("Ford", carMakes[0].CarMakeName);
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", carMakes[0].UserId);
            Assert.AreEqual(DateTime.Today.ToShortDateString(), carMakes[0].CarMakeAddedDate.ToShortDateString());
            Assert.AreEqual("adminTest@guildcars.com", carMakes[0].UserEmail);

            Assert.AreEqual(2, carMakes[1].CarMakeId);
            Assert.AreEqual("Lotus", carMakes[1].CarMakeName);
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", carMakes[1].UserId);
            Assert.AreEqual(DateTime.Today.ToShortDateString(), carMakes[1].CarMakeAddedDate.ToShortDateString());
            Assert.AreEqual("adminTest@guildcars.com", carMakes[1].UserEmail);

            Assert.AreEqual(3, carMakes[2].CarMakeId);
            Assert.AreEqual("Tesla", carMakes[2].CarMakeName);
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", carMakes[2].UserId);
            Assert.AreEqual(DateTime.Today.ToShortDateString(), carMakes[2].CarMakeAddedDate.ToShortDateString());
            Assert.AreEqual("adminTest@guildcars.com", carMakes[2].UserEmail);

            Assert.AreEqual(4, carMakes[3].CarMakeId);
            Assert.AreEqual("Toyota", carMakes[3].CarMakeName);
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", carMakes[3].UserId);
            Assert.AreEqual(DateTime.Today.ToShortDateString(), carMakes[3].CarMakeAddedDate.ToShortDateString());
            Assert.AreEqual("adminTest@guildcars.com", carMakes[3].UserEmail);
        }

        [Test]
        public void InsertCarMake()
        {
            CarMake makeToAdd = new CarMake();
            var repo = new CarMakesRepo();

            makeToAdd.CarMakeName = "Nissan";
            makeToAdd.UserId = "00000000-0000-0000-0000-000000000000";

            repo.Insert(makeToAdd);
            Assert.AreEqual(5, makeToAdd.CarMakeId);

            //var carMakes = repo.GetAll();
            //Assert.AreEqual(5, carMakes.Count);

            //Assert.AreEqual(5, carMakes[4].CarMakeId);
            //Assert.AreEqual("Nissan", carMakes[4].CarMakeName);
            //Assert.AreEqual("00000000-0000-0000-0000-000000000000", carMakes[4].UserId);
            //Assert.AreEqual(DateTime.Today.ToShortDateString(), carMakes[4].CarMakeAddedDate.ToShortDateString());
        }
        [Test]
        public void AddMakeIfNotDuplicateWorks()
        {
            CarMake makeToAdd = new CarMake();
            var repo = new CarMakesRepo();
            var carMakes = repo.GetAll().Count();
            bool addSuccessful;
            Assert.AreEqual(4, carMakes);

            makeToAdd.CarMakeName = "Nissan";
            makeToAdd.UserId = "00000000-0000-0000-0000-000000000000";
            addSuccessful = repo.AddMakeIfNotDuplicate(makeToAdd);
            Assert.AreEqual(5, makeToAdd.CarMakeId);
            Assert.AreEqual(true, addSuccessful);
            carMakes = repo.GetAll().Count();
            Assert.AreEqual(5, carMakes);

            makeToAdd.CarMakeName = "Tesla";
            makeToAdd.UserId = "00000000-0000-0000-0000-000000000000";
            addSuccessful = repo.AddMakeIfNotDuplicate(makeToAdd);
            Assert.AreEqual(false, addSuccessful);
            carMakes = repo.GetAll().Count();
            Assert.AreEqual(5, carMakes);

        }

        [Test]
        public void CheckForDuplicateCarMakes()
        {
            var repo = new CarMakesRepo();
            var carMakeToCheck = "Lotus";

            List<ShowCarMake> carMakes = repo.GetAll().ToList();

            bool isDuplicateCarMake = repo.IsDuplicateCarMake(carMakeToCheck, carMakes);

            Assert.AreEqual(true, isDuplicateCarMake);

            carMakeToCheck = "Ford";
            isDuplicateCarMake = repo.IsDuplicateCarMake(carMakeToCheck, carMakes);
            Assert.AreEqual(true, isDuplicateCarMake);
            carMakeToCheck = "forD";
            isDuplicateCarMake = repo.IsDuplicateCarMake(carMakeToCheck, carMakes);
            Assert.AreEqual(true, isDuplicateCarMake);
            carMakeToCheck = "Lotus";
            isDuplicateCarMake = repo.IsDuplicateCarMake(carMakeToCheck, carMakes);
            Assert.AreEqual(true, isDuplicateCarMake);
            carMakeToCheck = "Tesla";
            isDuplicateCarMake = repo.IsDuplicateCarMake(carMakeToCheck, carMakes);
            Assert.AreEqual(true, isDuplicateCarMake);
            carMakeToCheck = "tesla";
            isDuplicateCarMake = repo.IsDuplicateCarMake(carMakeToCheck, carMakes);
            Assert.AreEqual(true, isDuplicateCarMake);
            carMakeToCheck = "Toyota";
            isDuplicateCarMake = repo.IsDuplicateCarMake(carMakeToCheck, carMakes);
            Assert.AreEqual(true, isDuplicateCarMake);
            carMakeToCheck = "TOYOTA";
            isDuplicateCarMake = repo.IsDuplicateCarMake(carMakeToCheck, carMakes);
            Assert.AreEqual(true, isDuplicateCarMake);


            carMakeToCheck = "Jeep";
            isDuplicateCarMake = repo.IsDuplicateCarMake(carMakeToCheck, carMakes);
            Assert.AreEqual(false, isDuplicateCarMake);
            carMakeToCheck = "Buick";
            isDuplicateCarMake = repo.IsDuplicateCarMake(carMakeToCheck, carMakes);
            Assert.AreEqual(false, isDuplicateCarMake);
            carMakeToCheck = "Honda";
            isDuplicateCarMake = repo.IsDuplicateCarMake(carMakeToCheck, carMakes);
            Assert.AreEqual(false, isDuplicateCarMake);
            carMakeToCheck = "tuskla";
            isDuplicateCarMake = repo.IsDuplicateCarMake(carMakeToCheck, carMakes);
            Assert.AreEqual(false, isDuplicateCarMake);
        }

        [Test]
        public void CanLoadCarModels()
        {
            var repo = new CarModelsRepo();

            List<ShowCarModel> carModels = repo.GetAll().ToList();

            Assert.AreEqual(5, carModels.Count);

            Assert.AreEqual(1, carModels[0].CarMakeId);
            Assert.AreEqual("Ford", carModels[0].CarMakeName);
            Assert.AreEqual(1, carModels[0].CarModelId);
            Assert.AreEqual("Mustang", carModels[0].CarModelName);
            Assert.AreEqual(DateTime.Today.ToShortDateString(), carModels[0].CarModelAddedDate.ToShortDateString());
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", carModels[0].UserId);
            Assert.AreEqual("adminTest@guildcars.com", carModels[0].UserEmail);


            Assert.AreEqual(3, carModels[4].CarMakeId);
            Assert.AreEqual("Tesla", carModels[4].CarMakeName);
            Assert.AreEqual(5, carModels[4].CarModelId);
            Assert.AreEqual("Model S", carModels[4].CarModelName);
            Assert.AreEqual(DateTime.Today.ToShortDateString(), carModels[4].CarModelAddedDate.ToShortDateString());
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", carModels[4].UserId);
            Assert.AreEqual("adminTest@guildcars.com", carModels[0].UserEmail);
        }

        [Test]
        public void CanLoadModelsByMake()
        {
            var repo = new CarModelsRepo();

            List<ShowCarModel> carModels = repo.GetModelsByMake(1).ToList();

            Assert.AreEqual(2, carModels.Count);

            Assert.AreEqual(1, carModels[0].CarMakeId);
            Assert.AreEqual("Ford", carModels[0].CarMakeName);
            Assert.AreEqual(1, carModels[0].CarModelId);
            Assert.AreEqual("Mustang", carModels[0].CarModelName);
            Assert.AreEqual(DateTime.Today.ToShortDateString(), carModels[0].CarModelAddedDate.ToShortDateString());
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", carModels[0].UserId);

            Assert.AreEqual(1, carModels[1].CarMakeId);
            Assert.AreEqual("Ford", carModels[1].CarMakeName);
            Assert.AreEqual(2, carModels[1].CarModelId);
            Assert.AreEqual("Festiva", carModels[1].CarModelName);
            Assert.AreEqual(DateTime.Today.ToShortDateString(), carModels[1].CarModelAddedDate.ToShortDateString());
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", carModels[1].UserId);

        }

        [Test]
        public void CanAddCarModel()
        {
            CarModel modelToAdd = new CarModel();
            var repo = new CarModelsRepo();


            modelToAdd.CarMakeId = 2;
            modelToAdd.CarModelName = "Mark II";
            modelToAdd.UserId = "00000000-0000-0000-0000-000000000000";

            repo.Insert(modelToAdd);
            Assert.AreEqual(6, modelToAdd.CarModelId);
        }


        [Test]
        public void CheckForDuplicateCarModels()
        {
            var repo = new CarModelsRepo();

            var carModelToCheck = "Elise";
            var carMakeId = 2;
            bool IsDuplicateCarModel = repo.IsDuplicateCarModel(carModelToCheck, carMakeId);
            Assert.AreEqual(true, IsDuplicateCarModel);

            carModelToCheck = "Mustang";
            carMakeId = 1;
            IsDuplicateCarModel = repo.IsDuplicateCarModel(carModelToCheck, carMakeId);
            Assert.AreEqual(true, IsDuplicateCarModel);

            carModelToCheck = "Festiva";
            carMakeId = 1;
            IsDuplicateCarModel = repo.IsDuplicateCarModel(carModelToCheck, carMakeId);
            Assert.AreEqual(true, IsDuplicateCarModel);

            carModelToCheck = "Focus";
            carMakeId = 1;
            IsDuplicateCarModel = repo.IsDuplicateCarModel(carModelToCheck, carMakeId);
            Assert.AreEqual(false, IsDuplicateCarModel);

            carModelToCheck = "mustang";
            carMakeId = 1;
            IsDuplicateCarModel = repo.IsDuplicateCarModel(carModelToCheck, carMakeId);
            Assert.AreEqual(true, IsDuplicateCarModel);

            carModelToCheck = "model s";
            carMakeId = 3;
            IsDuplicateCarModel = repo.IsDuplicateCarModel(carModelToCheck, carMakeId);
            Assert.AreEqual(true, IsDuplicateCarModel);

            carModelToCheck = "prius";
            carMakeId = 4;
            IsDuplicateCarModel = repo.IsDuplicateCarModel(carModelToCheck, carMakeId);
            Assert.AreEqual(false, IsDuplicateCarModel);
        }

        [Test]
        public void CanLoadFeaturedVehicles()
        {

            var repo = new VehiclesRepo();

            List<VehicleShortItem> vehicles = repo.GetFeaturedVehicles().ToList();

            Assert.AreEqual(25, vehicles.Count);

            Assert.AreEqual(4, vehicles[3].VehicleId);
            Assert.AreEqual("41111111111111115", vehicles[3].VinNumber);
            Assert.AreEqual("2001", vehicles[3].ModelYear);
            Assert.AreEqual(99000, vehicles[3].SalePrice);
            Assert.AreEqual("placeholder.png", vehicles[3].VehicleImageFileName);
            Assert.AreEqual(1, vehicles[3].CarMakeId);
            Assert.AreEqual("Ford", vehicles[3].CarMakeName);
            Assert.AreEqual(1, vehicles[3].CarModelId);
            Assert.AreEqual("Mustang", vehicles[3].CarModelName);
        }

        [Test]
        public void CanLoadVehicleById()
        {
            var repo = new VehiclesRepo();

            VehicleDetails vehicle = repo.GetVehicleViewObjectById(49);

            Assert.IsNotNull(vehicle);

            Assert.AreEqual("55555555555555555", vehicle.VinNumber);
            Assert.AreEqual("1995", vehicle.ModelYear);
            Assert.AreEqual(50000.00, vehicle.MSRP);
            Assert.AreEqual(50000.00, vehicle.SalePrice);
            Assert.AreEqual("A great little car", vehicle.VehicleDescription);
            Assert.AreEqual("placeholder.png", vehicle.VehicleImageFileName);
            Assert.AreEqual(false, vehicle.IsNewCar);
            Assert.AreEqual(10000, vehicle.VehicleMileage);
            Assert.AreEqual(5, vehicle.BodyColorTypeId);
            Assert.AreEqual("Blue", vehicle.BodyColorType);
            Assert.AreEqual(1, vehicle.InteriorColorTypeId);
            Assert.AreEqual("Red", vehicle.InteriorColorType);
            Assert.AreEqual(1, vehicle.CarBodyTypeId);
            Assert.AreEqual("Car", vehicle.CarBodyType);
            Assert.AreEqual(1, vehicle.CarMakeId);
            Assert.AreEqual("Ford", vehicle.CarMakeName);
            Assert.AreEqual(1, vehicle.CarModelId);
            Assert.AreEqual("Mustang", vehicle.CarModelName);
            Assert.AreEqual(2, vehicle.TransmissionTypeId);
            Assert.AreEqual("Manual", vehicle.TransmissionType);
            Assert.AreEqual(false, vehicle.IsFeaturedCar);

        }

        [Test]
        public void EmptyVehicleIsNull()
        {
            var repo = new VehiclesRepo();

            VehicleDetails vehicle = repo.GetVehicleViewObjectById(9999);

            Assert.IsNull(vehicle);

        }
    }
}
