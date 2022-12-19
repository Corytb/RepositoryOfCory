using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class AddEditVehicleItem
    {
        public int VehicleId { get; set; }
        public string VinNumber { get; set; }
        public string ModelYear { get; set; }
        public decimal MSRP { get; set; }
        public decimal SalePrice { get; set; }
        public string VehicleDescription { get; set; }
        public string VehicleImageFileName { get; set; }
        public bool IsNewCar { get; set; }
        public int VehicleMileage { get; set; }
        public int ColorTypeId { get; set; }
        public string ColorType { get; set; }
        public int CarBodyTypeId { get; set; }
        public string CarBodyType { get; set; }
        public int CarMakeId { get; set; }
        public string CarMakeName { get; set; }
        public int CarModelId { get; set; }
        public string CarModelName { get; set; }
        public int TransmissionTypeId { get; set; }
        public string TransmissionType { get; set; }
    }
}
