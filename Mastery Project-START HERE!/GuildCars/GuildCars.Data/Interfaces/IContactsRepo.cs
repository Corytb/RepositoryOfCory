using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public interface IContactsRepo
    {
        void InsertNewContact(Contact contact);
        List<Contact> GetAllContacts();
        void DeleteContact(int contactId);
    }
}
