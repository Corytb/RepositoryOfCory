using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public interface ICarMakesRepo
    {
        IEnumerable<ShowCarMake> GetAll();
        void Insert(CarMake carMake);
        bool IsDuplicateCarMake(string carMakeCandidate, List<ShowCarMake> carMakes);
        bool AddMakeIfNotDuplicate(CarMake carMake);
    }
}
