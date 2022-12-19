using GuildCars.Data.Interfaces;
using GuildCars.Data.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Factories
{
    public class BodyTypeRepoFactory
    {
        public static ICarBodiesRepo GetRepo()
        {
            switch (Settings.GetRepositoryType())
            {
                case "ADO":
                    return new CarBodiesRepo();
                default:
                    throw new Exception("Could not find valid repository type configuration value.");
            }
        }
    }
}
