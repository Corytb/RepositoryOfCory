﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Models
{
    public class SalesReportViewModel
    {
        public IEnumerable<SelectListItem> UserWithSales { get; set; }
    }
}