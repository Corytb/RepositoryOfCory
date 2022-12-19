using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class SalesSearchParameters
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string MinYear { get; set; }
        public string MaxYear { get; set; }
    }
}
