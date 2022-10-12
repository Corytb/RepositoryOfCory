using GuildCars.Data.Repos;
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
    public class VehicleInsert_UpdateTests
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
        public void CanAddAndDeleteNewVehicle()
        {
            Vehicle vehicleToAdd = new Vehicle();
            var repo = new VehiclesRepo();

            vehicleToAdd.VinNumber = "12345678912345678";
            vehicleToAdd.CarMakeId = 3;
            vehicleToAdd.CarModelId = 5;
            vehicleToAdd.BodyColorTypeId = 2;
            vehicleToAdd.InteriorColorTypeId = 5;
            vehicleToAdd.TransmissionTypeId = 3;
            vehicleToAdd.CarBodyTypeId = 3;
            vehicleToAdd.ModelYear = "2023";
            vehicleToAdd.VehicleMileage = 500;
            vehicleToAdd.MSRP = 50000;
            vehicleToAdd.SalePrice = 49000;
            vehicleToAdd.VehicleDescription = "Brand spankin new!";
            vehicleToAdd.VehicleImageFileName = "testImageName.png";
            vehicleToAdd.IsNewCar = true;

            repo.InsertNewVehicle(vehicleToAdd);
            Assert.AreEqual(72, vehicleToAdd.VehicleId);

            //newly added vehicle is not featured
            var updatedVehicle = repo.GetVehicleViewObjectById(72);
            Assert.AreEqual(false, updatedVehicle.IsFeaturedCar);

            repo.DeleteVehicle(72);

            var nullVehicle = repo.GetVehicleTableObjectById(72);

            Assert.AreEqual(null, nullVehicle);
        }


        [Test]
        public void CanUpdateVehicleDetails()
        {
            Vehicle vehicleToAdd = new Vehicle();
            var repo = new VehiclesRepo();

            vehicleToAdd.VinNumber = "12345678912345678";
            vehicleToAdd.CarMakeId = 3;
            vehicleToAdd.CarModelId = 5;
            vehicleToAdd.BodyColorTypeId = 2;
            vehicleToAdd.InteriorColorTypeId = 5;
            vehicleToAdd.TransmissionTypeId = 3;
            vehicleToAdd.CarBodyTypeId = 3;
            vehicleToAdd.ModelYear = "2023";
            vehicleToAdd.VehicleMileage = 500;
            vehicleToAdd.MSRP = 50000;
            vehicleToAdd.SalePrice = 49000;
            vehicleToAdd.VehicleDescription = "Brand spankin new!";
            vehicleToAdd.VehicleImageFileName = "testImageName.png";
            vehicleToAdd.IsNewCar = true;

            repo.InsertNewVehicle(vehicleToAdd);

            vehicleToAdd.VinNumber = "12345678912345678";
            vehicleToAdd.CarMakeId = 1;
            vehicleToAdd.CarModelId = 1;
            vehicleToAdd.BodyColorTypeId = 3;
            vehicleToAdd.InteriorColorTypeId = 3;
            vehicleToAdd.TransmissionTypeId = 1;
            vehicleToAdd.CarBodyTypeId = 2;
            vehicleToAdd.ModelYear = "2023";
            vehicleToAdd.VehicleMileage = 6000;
            vehicleToAdd.MSRP = 4900;
            vehicleToAdd.SalePrice = 4500;
            vehicleToAdd.VehicleDescription = "Like new!";
            vehicleToAdd.VehicleImageFileName = "updated.png";
            vehicleToAdd.IsNewCar = false;
            vehicleToAdd.IsFeaturedCar = false;

            repo.UpdateVehicle(vehicleToAdd);

            var updatedVehicle = repo.GetVehicleViewObjectById(72);


            Assert.AreEqual("12345678912345678", updatedVehicle.VinNumber);
            Assert.AreEqual(1, updatedVehicle.CarMakeId);
            Assert.AreEqual(1, updatedVehicle.CarModelId);
            Assert.AreEqual(3, updatedVehicle.BodyColorTypeId);
            Assert.AreEqual(3, updatedVehicle.InteriorColorTypeId);
            Assert.AreEqual(1, updatedVehicle.TransmissionTypeId);
            Assert.AreEqual(2, updatedVehicle.CarBodyTypeId);
            Assert.AreEqual("2023", updatedVehicle.ModelYear);
            Assert.AreEqual(6000, updatedVehicle.VehicleMileage);
            Assert.AreEqual(4900, updatedVehicle.MSRP);
            Assert.AreEqual(4500, updatedVehicle.SalePrice);
            Assert.AreEqual("Like new!", updatedVehicle.VehicleDescription);
            Assert.AreEqual("updated.png", updatedVehicle.VehicleImageFileName);
            Assert.AreEqual(false, updatedVehicle.IsNewCar);
            Assert.AreEqual(false, updatedVehicle.IsFeaturedCar);

        }

        [Test]
        public void CanUpdateVehicleFeaturedStatus()
        {
            Vehicle vehicleToAdd = new Vehicle();
            var repo = new VehiclesRepo();

            vehicleToAdd.VinNumber = "12345678912345678";
            vehicleToAdd.CarMakeId = 3;
            vehicleToAdd.CarModelId = 5;
            vehicleToAdd.BodyColorTypeId = 2;
            vehicleToAdd.InteriorColorTypeId = 5;
            vehicleToAdd.TransmissionTypeId = 3;
            vehicleToAdd.CarBodyTypeId = 3;
            vehicleToAdd.ModelYear = "2023";
            vehicleToAdd.VehicleMileage = 500;
            vehicleToAdd.MSRP = 50000;
            vehicleToAdd.SalePrice = 49000;
            vehicleToAdd.VehicleDescription = "Brand spankin new!";
            vehicleToAdd.VehicleImageFileName = "testImageName.png";
            vehicleToAdd.IsNewCar = true;

            repo.InsertNewVehicle(vehicleToAdd);

            vehicleToAdd.VinNumber = "12345678912345678";
            vehicleToAdd.CarMakeId = 1;
            vehicleToAdd.CarModelId = 1;
            vehicleToAdd.BodyColorTypeId = 3;
            vehicleToAdd.InteriorColorTypeId = 3;
            vehicleToAdd.TransmissionTypeId = 1;
            vehicleToAdd.CarBodyTypeId = 2;
            vehicleToAdd.ModelYear = "2023";
            vehicleToAdd.VehicleMileage = 6000;
            vehicleToAdd.MSRP = 4900;
            vehicleToAdd.SalePrice = 4500;
            vehicleToAdd.VehicleDescription = "Like new!";
            vehicleToAdd.VehicleImageFileName = "updated2.png";
            vehicleToAdd.IsNewCar = false;
            vehicleToAdd.IsFeaturedCar = true;

            repo.UpdateVehicle(vehicleToAdd);

            var updatedVehicle = repo.GetVehicleViewObjectById(72);


            Assert.AreEqual("12345678912345678", updatedVehicle.VinNumber);
            Assert.AreEqual(1, updatedVehicle.CarMakeId);
            Assert.AreEqual(1, updatedVehicle.CarModelId);
            Assert.AreEqual(3, updatedVehicle.BodyColorTypeId);
            Assert.AreEqual(3, updatedVehicle.InteriorColorTypeId);
            Assert.AreEqual(1, updatedVehicle.TransmissionTypeId);
            Assert.AreEqual(2, updatedVehicle.CarBodyTypeId);
            Assert.AreEqual("2023", updatedVehicle.ModelYear);
            Assert.AreEqual(6000, updatedVehicle.VehicleMileage);
            Assert.AreEqual(4900, updatedVehicle.MSRP);
            Assert.AreEqual(4500, updatedVehicle.SalePrice);
            Assert.AreEqual("Like new!", updatedVehicle.VehicleDescription);
            Assert.AreEqual("updated2.png", updatedVehicle.VehicleImageFileName);
            Assert.AreEqual(false, updatedVehicle.IsNewCar);
            Assert.AreEqual(true, updatedVehicle.IsFeaturedCar);

        }

    }
}
