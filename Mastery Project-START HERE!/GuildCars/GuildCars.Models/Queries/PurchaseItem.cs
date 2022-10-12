using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class PurchaseItem
    {
        public int PurchaseId { get; set; }
        public int PurchasePaymentTypeId { get; set; }
        public string PurchasePaymentType { get; set; }
        public string UserId { get; set; }
        public int VehicleId { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerStreet1 { get; set; }
        public string CustomerStreet2 { get; set; }
        public string CustomerCity { get; set; }
        public string StateId { get; set; }
        public string CustomerZip { get; set; }

    }
}
