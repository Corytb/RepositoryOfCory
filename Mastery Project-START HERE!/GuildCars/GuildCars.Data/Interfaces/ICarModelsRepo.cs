using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public interface ICarModelsRepo
    {
        IEnumerable<ShowCarModel> GetAll();
        IEnumerable<ShowCarModel> GetModelsByMake(int carMakeId);
        void Insert(CarModel carModel);
        bool IsDuplicateCarModel(string carModelCandidate, int carMakeId);
    }
}
