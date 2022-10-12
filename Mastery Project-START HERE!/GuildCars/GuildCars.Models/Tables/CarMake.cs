using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.Models.Tables
{
    public class CarMake
    {
        public int CarMakeId { get; set; }

        public string CarMakeName { get; set; }
        public string UserId { get; set; }
        public DateTime CarMakeAddedDate { get; set; }
    }
}