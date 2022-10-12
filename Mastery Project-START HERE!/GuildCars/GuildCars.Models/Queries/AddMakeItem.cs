using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class AddMakeItem
    {
        public int CarMakeId { get; set; }
        public string CarMakeName { get; set; }
        public int UserId { get; set; }
    }
}
