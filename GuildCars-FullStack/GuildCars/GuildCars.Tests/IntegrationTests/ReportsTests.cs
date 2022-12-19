using GuildCars.Data.Repos;
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
    public class ReportsTests
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
        public void CanLoadNewVehicleInventoryReport()
        {

            var repo = new VehiclesRepo();

            var inventory = repo.GetNewVehicleInventoryReport().ToList();

            Assert.AreEqual(6, inventory.Count);

            Assert.AreEqual("2023", inventory[1].Year);
            Assert.AreEqual("Lotus", inventory[1].Make);
            Assert.AreEqual("Mark I", inventory[1].Model);
            Assert.AreEqual(16, inventory[1].Count);
            Assert.AreEqual(1920000, inventory[1].StockValue);
        }

        [Test]
        public void CanLoadUsedVehicleInventoryReport()
        {

            var repo = new VehiclesRepo();

            var inventory = repo.GetUsedVehicleInventoryReport().ToList();

            Assert.AreEqual(6, inventory.Count);

            Assert.AreEqual("2001", inventory[0].Year);
            Assert.AreEqual("Ford", inventory[0].Make);
            Assert.AreEqual("Mustang", inventory[0].Model);
            Assert.AreEqual(19, inventory[0].Count);
            Assert.AreEqual(1900000, inventory[0].StockValue);


            Assert.AreEqual("1968", inventory[5].Year);
            Assert.AreEqual("Ford", inventory[5].Make);
            Assert.AreEqual("Mustang", inventory[5].Model);
            Assert.AreEqual(1, inventory[5].Count);
            Assert.AreEqual(50000, inventory[5].StockValue);

            Assert.AreEqual("1994", inventory[2].Year);
            Assert.AreEqual("Ford", inventory[2].Make);
            Assert.AreEqual("Mustang", inventory[2].Model);
            Assert.AreEqual(2, inventory[2].Count);
            Assert.AreEqual(100000, inventory[2].StockValue);
        }
    }
}
