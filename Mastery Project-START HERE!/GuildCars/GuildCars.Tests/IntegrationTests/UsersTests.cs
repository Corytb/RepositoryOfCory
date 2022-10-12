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
    public class UsersTests
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

        //Methods work--Users list needed expanding for further app testing
        //Unable to add Users in SSMS without password
        [Test]
        public void CanGetUserList()
        {

            var repo = new UsersRepo();

            var users = repo.GetUserList().ToList();

            Assert.AreEqual(8, users.Count);

            Assert.AreEqual("00000000-0000-0000-0000-000000000000", users[0].UserId);
            Assert.AreEqual("Kurt", users[0].FirstName);
            Assert.AreEqual("Brathall", users[0].LastName);
            Assert.AreEqual("adminTest@guildcars.com", users[0].Email);
            Assert.AreEqual("Admin", users[0].UserRole);

            Assert.AreEqual("f68319c3-5213-4eda-b646-16415ca1618c", users[7].UserId);
            Assert.AreEqual("Mark", users[7].FirstName);
            Assert.AreEqual("Dille", users[7].LastName);
            Assert.AreEqual("sales@gmail.com", users[7].Email);
            Assert.AreEqual("Sales", users[7].UserRole);
        }
        [Test]
        public void GetUsersWithSales()
        {

            var repo = new UsersRepo();

            var users = repo.GetUsersWithSales().ToList();

            Assert.AreEqual(5, users.Count);

            Assert.AreEqual("00000000-0000-0000-0000-000000000000", users[0].UserId);
            Assert.AreEqual("Kurt Brathall", users[0].UserName);

            Assert.AreEqual("33333333-3333-3333-3333-333333333333", users[3].UserId);
            Assert.AreEqual("Carnell Fayson", users[3].UserName);

            Assert.AreEqual("f68319c3-5213-4eda-b646-16415ca1618c", users[4].UserId);
            Assert.AreEqual("Mark Dille", users[4].UserName);
        }

        [Test]
        public void CanGetAspNetRoles()
        {

            var repo = new UsersRepo();

            var roles = repo.GetAspNetRoles().ToList();

            Assert.AreEqual(3, roles.Count);

            Assert.AreEqual("1", roles[0].Id);
            Assert.AreEqual("Admin", roles[0].Name);

            Assert.AreEqual("3", roles[1].Id);
            Assert.AreEqual("Disabled", roles[1].Name);

            Assert.AreEqual("2", roles[2].Id);
            Assert.AreEqual("Sales", roles[2].Name);
        }

        [Test]
        public void CanUpdateUserRole()
        {
            var repo = new UsersRepo();

            repo.UpdateUserRole("654b242e-fd44-45e2-b1ff-68a15e15b6c5", "3");

            var userRoles = repo.GetAllAspNetUserRoles().ToList();
            Assert.AreEqual(3, userRoles.Count);
            Assert.AreEqual("654b242e-fd44-45e2-b1ff-68a15e15b6c5", userRoles[0].UserId);
            Assert.AreEqual("3", userRoles[0].RoleId);

            var user = repo.GetUserById("654b242e-fd44-45e2-b1ff-68a15e15b6c5");
            Assert.AreEqual("654b242e-fd44-45e2-b1ff-68a15e15b6c5", user.UserId);
            Assert.AreEqual("William", user.FirstName);
            Assert.AreEqual("Smith", user.LastName);
            Assert.AreEqual("will.smith@gmail.com", user.Email);
            Assert.AreEqual("3", user.UserRoleId);

            repo.UpdateUserRole("654b242e-fd44-45e2-b1ff-68a15e15b6c5", "1");


            var updatedUserRoles = repo.GetAllAspNetUserRoles().ToList();
            Assert.AreEqual(3, updatedUserRoles.Count);
            Assert.AreEqual("654b242e-fd44-45e2-b1ff-68a15e15b6c5", updatedUserRoles[0].UserId);
            Assert.AreEqual("1", updatedUserRoles[0].RoleId);

            var updatedUser = repo.GetUserById("654b242e-fd44-45e2-b1ff-68a15e15b6c5");
            Assert.AreEqual("654b242e-fd44-45e2-b1ff-68a15e15b6c5", updatedUser.UserId);
            Assert.AreEqual("William", updatedUser.FirstName);
            Assert.AreEqual("Smith", updatedUser.LastName);
            Assert.AreEqual("will.smith@gmail.com", updatedUser.Email);
            Assert.AreEqual("1", updatedUser.UserRoleId);

        }
    }
}
