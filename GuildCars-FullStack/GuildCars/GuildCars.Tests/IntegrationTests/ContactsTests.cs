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
    public class ContactsTests
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
        public void CanAddGetDeleteContacts()
        {
            var repo = new ContactsRepo();

            var allContacts = repo.GetAllContacts();
            Assert.AreEqual(2, allContacts.Count());
            Assert.AreEqual(2, allContacts[0].ContactId);
            Assert.AreEqual(1, allContacts[1].ContactId);

            Contact contactToAdd = new Contact();
            contactToAdd.ContactName = "Johnny Walker";
            contactToAdd.ContactEmail = "Johnny@Walker.com";
            contactToAdd.ContactPhone = "612-876-8350";
            contactToAdd.ContactMessage = "This is a test message! :)";

            repo.InsertNewContact(contactToAdd);

            var updateContacts = repo.GetAllContacts();
            Assert.AreEqual(3, updateContacts.Count());

            Assert.AreEqual(3, updateContacts[0].ContactId);
            Assert.AreEqual(2, updateContacts[1].ContactId);
            Assert.AreEqual(1, updateContacts[2].ContactId);

            Assert.AreEqual("Johnny Walker", updateContacts[0].ContactName);
            Assert.AreEqual("Johnny@Walker.com", updateContacts[0].ContactEmail);
            Assert.AreEqual("612-876-8350", updateContacts[0].ContactPhone);
            Assert.AreEqual("This is a test message! :)", updateContacts[0].ContactMessage);
            Assert.AreEqual(DateTime.Today.ToShortDateString(), updateContacts[0].ContactSentDate.ToShortDateString());

            repo.DeleteContact(3);

            Assert.AreEqual(3, updateContacts.Count());
            Assert.AreEqual(2, allContacts[0].ContactId);
            Assert.AreEqual(1, allContacts[1].ContactId);
        }

    }


}
