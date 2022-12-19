using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class AddModelItem
    {
        public int CarModelId { get; set; }
        public string CarModelName { get; set; }
        public int CarMakeId { get; set; }
        public int UserId { get; set; }
    }
}
