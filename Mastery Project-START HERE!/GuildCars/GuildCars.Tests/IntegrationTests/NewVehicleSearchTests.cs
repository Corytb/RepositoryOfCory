using GuildCars.Data.Repos;
using GuildCars.Models.Queries;
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
    public class NewVehicleSearchTests
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
        public void CanSearchOnMinPrice()
        {
            var repo = new VehiclesRepo();

            var found = repo.SearchNew(new VehicleSearchParameters { MinPrice = 75000M });

            Assert.AreEqual(20, found.Count());
        }

        [Test]
        public void CanSearchOnMaxPrice()
        {
            var repo = new VehiclesRepo();

            var found = repo.SearchNew(new VehicleSearchParameters { MaxPrice = 80000M });

            Assert.AreEqual(2, found.Count());
        }

        [Test]
        public void CanSearchOnMinYear()
        {
            var repo = new VehiclesRepo();

            var found = repo.SearchNew(new VehicleSearchParameters { MinYear = 2016 });

            Assert.AreEqual(20, found.Count());
        }

        [Test]
        public void CanSearchOnMaxYear()
        {
            var repo = new VehiclesRepo();

            var found = repo.SearchNew(new VehicleSearchParameters { MaxYear = 2016 });

            Assert.AreEqual(2, found.Count());
        }

        [Test]
        public void CanSearchOnYearRange()
        {
            var repo = new VehiclesRepo();

            var found = repo.SearchNew(new VehicleSearchParameters { MinYear = 2017, MaxYear = 2022 });

            Assert.AreEqual(3, found.Count());
        }

        [Test]
        public void CanSearchOnPriceRange()
        {
            var repo = new VehiclesRepo();

            var found = repo.SearchNew(new VehicleSearchParameters { MinPrice = 80000M, MaxPrice = 118000M });

            Assert.AreEqual(2, found.Count());
        }

        [Test]
        public void CanSearchOnParameters()
        {
            var repo = new VehiclesRepo();

            var found = repo.SearchNew(new VehicleSearchParameters { GeneralSearchParam = "s" });
            Assert.AreEqual(20, found.Count());

            found = repo.SearchNew(new VehicleSearchParameters { GeneralSearchParam = "f" });
            Assert.AreEqual(1, found.Count());

            found = repo.SearchNew(new VehicleSearchParameters { GeneralSearchParam = "01" });
            Assert.AreEqual(3, found.Count());

            found = repo.SearchNew(new VehicleSearchParameters { GeneralSearchParam = "20" });
            Assert.AreEqual(20, found.Count());

            found = repo.SearchNew(new VehicleSearchParameters { GeneralSearchParam = "lo" });
            Assert.AreEqual(19, found.Count());

            found = repo.SearchNew(new VehicleSearchParameters { GeneralSearchParam = "mark" });
            Assert.AreEqual(18, found.Count());

            found = repo.SearchNew(new VehicleSearchParameters { GeneralSearchParam = "2023" });
            Assert.AreEqual(16, found.Count());

            found = repo.SearchNew(new VehicleSearchParameters { MaxPrice = 10M, GeneralSearchParam = "" });
            Assert.AreEqual(0, found.Count());

            found = repo.SearchNew(new VehicleSearchParameters { MaxPrice = 70000M, GeneralSearchParam = "" });
            Assert.AreEqual(1, found.Count());

            found = repo.SearchNew(new VehicleSearchParameters { MinYear = 2024, GeneralSearchParam = null });
            Assert.AreEqual(0, found.Count());

            found = repo.SearchNew(new VehicleSearchParameters { GeneralSearchParam = "asdf" });
            Assert.AreEqual(0, found.Count());

            found = repo.SearchNew(new VehicleSearchParameters { GeneralSearchParam = null });
            Assert.AreEqual(20, found.Count());
            List<VehicleDetails> vehicles = found.ToList();
            Assert.AreEqual(130000M, vehicles[0].MSRP);
        }
    }
}