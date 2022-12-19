using GuildCars.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class InventoryReportViewModel
    {
        public IEnumerable<VehicleInventoryReport> NewVehicleInventory { get; set; }
        public IEnumerable<VehicleInventoryReport> UsedVehicleInventory { get; set; }
    }
}