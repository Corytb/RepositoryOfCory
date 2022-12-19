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
    public class CarModelAddViewModel : IValidatableObject
    {

        public IEnumerable<ShowCarModel> CarModels { get; set; }
        public IEnumerable<SelectListItem> CarMakes { get; set; }
        public CarModel CarModel { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            List<ValidationResult> errors = new List<ValidationResult>();
            if (string.IsNullOrEmpty(CarModel.CarModelName))
            {
                errors.Add(new ValidationResult("Model Name is required"));
            }
            return errors;
        }
    }
}