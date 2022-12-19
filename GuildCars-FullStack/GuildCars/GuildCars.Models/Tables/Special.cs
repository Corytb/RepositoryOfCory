using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.Models.Tables
{
    public class Special
    {
        public int SpecialId { get; set; }
        public string SpecialTitle { get; set; }
        public string SpecialDescription { get; set; }
        public string SpecialImageFileName { get; set; }
    }
}