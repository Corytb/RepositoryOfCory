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
    public class UsedVehicleSearchTests
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

            var found = repo.SearchUsed(new VehicleSearchParameters { MinPrice = 51000M });

            Assert.AreEqual(19, found.Count());
        }

        [Test]
        public void CanSearchOnMaxPrice()
        {
            var repo = new VehiclesRepo();

            var found = repo.SearchUsed(new VehicleSearchParameters { MaxPrice = 98000M });

            Assert.AreEqual(6, found.Count());
        }

        [Test]
        public void CanSearchOnMinYear()
        {
            var repo = new VehiclesRepo();

            var found = repo.SearchUsed(new VehicleSearchParameters { MinYear = 1994 });

            Assert.AreEqual(20, found.Count());
        }

        [Test]
        public void CanSearchOnMaxYear()
        {
            var repo = new VehiclesRepo();

            var found = repo.SearchUsed(new VehicleSearchParameters { MaxYear = 1995 });

            Assert.AreEqual(6, found.Count());
        }

        [Test]
        public void CanSearchOnYearRange()
        {
            var repo = new VehiclesRepo();

            var found = repo.SearchUsed(new VehicleSearchParameters { MinYear = 1990, MaxYear = 1994 });

            Assert.AreEqual(4, found.Count());
        }

        [Test]
        public void CanSearchOnPriceRange()
        {
            var repo = new VehiclesRepo();

            var found = repo.SearchUsed(new VehicleSearchParameters { MinPrice = 51000, MaxPrice = 98000 });

            Assert.AreEqual(0, found.Count());
        }

        [Test]
        public void CanSearchOnParameters()
        {
            var repo = new VehiclesRepo();

            var found = repo.SearchUsed(new VehicleSearchParameters { GeneralSearchParam = "s" });
            Assert.AreEqual(20, found.Count());

            found = repo.SearchUsed(new VehicleSearchParameters { GeneralSearchParam = "f" });
            Assert.AreEqual(20, found.Count());

            found = repo.SearchUsed(new VehicleSearchParameters { GeneralSearchParam = "01" });
            Assert.AreEqual(19, found.Count());

            found = repo.SearchUsed(new VehicleSearchParameters { GeneralSearchParam = "20" });
            Assert.AreEqual(19, found.Count());

            found = repo.SearchUsed(new VehicleSearchParameters { GeneralSearchParam = "lo" });
            Assert.AreEqual(0, found.Count());

            found = repo.SearchUsed(new VehicleSearchParameters { GeneralSearchParam = "mark" });
            Assert.AreEqual(0, found.Count());

            found = repo.SearchUsed(new VehicleSearchParameters { GeneralSearchParam = "2023" });
            Assert.AreEqual(0, found.Count());

            found = repo.SearchUsed(new VehicleSearchParameters { GeneralSearchParam = "" });
            Assert.AreEqual(20, found.Count());

            found = repo.SearchNew(new VehicleSearchParameters { MaxPrice = 10M, GeneralSearchParam = "" });
            Assert.AreEqual(0, found.Count());

            found = repo.SearchNew(new VehicleSearchParameters { MinYear = 2024, GeneralSearchParam = null });
            Assert.AreEqual(0, found.Count());

            found = repo.SearchNew(new VehicleSearchParameters { GeneralSearchParam = "asdf" });
            Assert.AreEqual(0, found.Count());

            found = repo.SearchUsed(new VehicleSearchParameters { GeneralSearchParam = null });
            Assert.AreEqual(20, found.Count());
            List<VehicleDetails> vehicles = found.ToList();
            Assert.AreEqual(100000M, vehicles[0].MSRP);
        }
    }
}
