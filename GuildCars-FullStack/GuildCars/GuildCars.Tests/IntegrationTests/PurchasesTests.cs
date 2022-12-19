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
    public class PurchasesTests
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
        public void CanAddNewPurchase()
        {
            Purchase purchaseToMake = new Purchase();
            var repo = new PurchasesRepo();

            purchaseToMake.PurchasePaymentTypeId = 2;
            purchaseToMake.UserId = "11111111-1111-1111-1111-111111111111";
            purchaseToMake.VehicleId = 10;
            purchaseToMake.PurchasePrice = 99000;
            purchaseToMake.CustomerName = "Dave Brathall";
            purchaseToMake.CustomerPhone = "612-407-9242";
            purchaseToMake.CustomerEmail = "dave@brathall.com";
            purchaseToMake.CustomerStreet1 = "6041 Oakland Ave. S.";
            purchaseToMake.CustomerStreet2 = "Suite 205";
            purchaseToMake.CustomerCity = "Stillwater";
            purchaseToMake.StateId = "MN";
            purchaseToMake.CustomerZip = "55417";

            repo.InsertNewPurchase(purchaseToMake);
            Assert.AreEqual(15, purchaseToMake.PurchaseId);

            //var vehicleRepo = new VehiclesRepo();

            //var addedVehicle = new Vehicle();
            ////newly added vehicle is purchased
            //addedVehicle = vehicleRepo.GetVehicleTableObjectById(10);
            //Assert.AreEqual(true, addedVehicle.IsPurchasedCar);
        }

        [Test]
        public void CanLoadPurchaseById()
        {

            var repo = new PurchasesRepo();

            PurchaseItem purchase = repo.GetPurchaseById(1);

            Assert.IsNotNull(purchase);

            Assert.AreEqual(1, purchase.PurchaseId);
            Assert.AreEqual(1, purchase.PurchasePaymentTypeId);
            Assert.AreEqual("Bank Finance", purchase.PurchasePaymentType);
            Assert.AreEqual("11111111-1111-1111-1111-111111111111", purchase.UserId);
            Assert.AreEqual(30, purchase.VehicleId);
            Assert.AreEqual(99000, purchase.PurchasePrice);
            Assert.AreEqual("10/10/2000", purchase.PurchaseDate.ToShortDateString());
            Assert.AreEqual("Dave Brathall", purchase.CustomerName);
            Assert.AreEqual("612-866-6537", purchase.CustomerPhone);
            Assert.AreEqual("dave@brathall.com", purchase.CustomerEmail);
            Assert.AreEqual("123 Hudson St", purchase.CustomerStreet1);
            Assert.AreEqual("Suite 208", purchase.CustomerStreet2);
            Assert.AreEqual("Stillwater", purchase.CustomerCity);
            Assert.AreEqual("MN", purchase.StateId);
            Assert.AreEqual("55417", purchase.CustomerZip);
        }
    }
}
