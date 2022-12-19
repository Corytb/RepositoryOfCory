using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class ShowCarMake
    {
        public int CarMakeId { get; set; }
        public string CarMakeName { get; set; }
        public DateTime CarMakeAddedDate { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }
    }
}
