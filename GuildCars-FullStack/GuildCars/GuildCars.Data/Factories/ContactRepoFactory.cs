using GuildCars.Data.Interfaces;
using GuildCars.Data.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Factories
{
    public static class ContactRepoFactory
    {
        public static IContactsRepo GetRepo()
        {
            switch (Settings.GetRepositoryType())
            {
                case "ADO":
                    return new ContactsRepo();
                default:
                    throw new Exception("Could not find valid repository type configuration value.");
            }
        }
    }
}
