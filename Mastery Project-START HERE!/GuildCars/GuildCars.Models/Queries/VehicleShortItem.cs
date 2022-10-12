using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class VehicleShortItem
    {
        public int VehicleId { get; set; }
        public string VinNumber { get; set; }
        public string ModelYear { get; set; }
        public decimal SalePrice { get; set; }
        public string VehicleImageFileName { get; set; }
        public int CarMakeId { get; set; }
        public string CarMakeName { get; set; }
        public int CarModelId { get; set; }
        public string CarModelName { get; set; }
    }
}
