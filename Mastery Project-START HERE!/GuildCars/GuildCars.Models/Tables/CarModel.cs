using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.Models.Tables
{
    public class CarModel
    {
        public int CarModelId { get; set; }
        public int CarMakeId { get; set; }
        public string CarModelName { get; set; }
        public string UserId { get; set; }
        public DateTime CarModelAddedDate { get; set; }
    }
}