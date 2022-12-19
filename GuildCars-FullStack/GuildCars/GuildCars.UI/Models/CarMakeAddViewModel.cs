using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Models
{
    public class CarMakeAddViewModel : IValidatableObject
    {
        public IEnumerable<ShowCarMake> CarMakes { get; set; }

        public CarMake CarMake { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            List<ValidationResult> errors = new List<ValidationResult>();
            if (string.IsNullOrEmpty(CarMake.CarMakeName))
            {
                errors.Add(new ValidationResult("Car Make Name is required"));
            }
            return errors;
        }

    }
}