﻿using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public interface ISpecialsRepo
    {
        List<Special> GetAll();
        void SpecialsDelete(int specialId);
        void SpecialsInsert(Special special);
        Special GetSpecialImageById(int specialId);

    }
}
