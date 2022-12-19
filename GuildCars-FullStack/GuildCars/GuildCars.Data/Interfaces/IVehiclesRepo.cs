using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public interface IVehiclesRepo
    {

        void DeleteVehicle(int vehicleId);
        IEnumerable<VehicleShortItem> GetFeaturedVehicles();
        VehicleDetails GetVehicleViewObjectById(int vehicldId);
        Vehicle GetVehicleTableObjectById(int vehicldId);
        IEnumerable<VehicleDetails> SearchNew(VehicleSearchParameters parameters);
        IEnumerable<VehicleDetails> SearchUsed(VehicleSearchParameters parameters);
        IEnumerable<VehicleDetails> SearchAll(VehicleSearchParameters parameters);
        void InsertNewVehicle(Vehicle vehicle);
        void UpdateVehicle(Vehicle vehicle);
        IEnumerable<VehicleInventoryReport> GetNewVehicleInventoryReport();
        IEnumerable<VehicleInventoryReport> GetUsedVehicleInventoryReport();
    }
}
