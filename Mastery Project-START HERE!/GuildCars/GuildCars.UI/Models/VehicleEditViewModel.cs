using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Models
{
    public class VehicleEditViewModel : IValidatableObject
    {
        public Vehicle Vehicle { get; set; }
        public IEnumerable<SelectListItem> CarMake { get; set; }
        public IEnumerable<SelectListItem> CarModel { get; set; }
        public IEnumerable<SelectListItem> CarBody { get; set; }
        public IEnumerable<SelectListItem> Transmission { get; set; }
        public IEnumerable<SelectListItem> Color { get; set; }
        public HttpPostedFileBase ImageUpload { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            //validating model year (isn't empty, is a number, is between 2000 - currentYear+1)
            if (string.IsNullOrEmpty(Vehicle.ModelYear))
            {

                errors.Add(new ValidationResult("Model Year is required"));

            }
            else if (!int.TryParse(Vehicle.ModelYear, out int year))
            {
                errors.Add(new ValidationResult("Model Year must be in format YYYY, between 2000 and " + (DateTime.Today.Year + 1)));
            }
            if (int.TryParse(Vehicle.ModelYear, out int yearRange))
            {
                if (yearRange < 2000 || yearRange > (DateTime.Today.Year + 1))
                {
                    errors.Add(new ValidationResult("Model Year must be in format YYYY, between 2000 and " + (DateTime.Today.Year + 1)));
                }
            }

            //New Vehicles must be <1000 miles. 
            if (!int.TryParse(Vehicle.VehicleMileage.ToString(), out int miles))
            {
                errors.Add(new ValidationResult("Vehicle mileage must be a number"));
            }
            if (Vehicle.VehicleMileage < 0)
            {
                errors.Add(new ValidationResult("Can vehicles have negative miles?"));
            }
            if (Vehicle.IsNewCar && Vehicle.VehicleMileage > 1000)
            {
                errors.Add(new ValidationResult("Vehicles over 1000 miles are considered 'new'"));
            }
            //Used Vehicles must be >1000 miles
            if (!Vehicle.IsNewCar && Vehicle.VehicleMileage <= 1000)
            {
                errors.Add(new ValidationResult("If vehicle has under 1000 miles it is considered 'new'"));
            }

            //VIN Number
            string vinPattern = "[A-HJ-NPR-Z0-9]{17}";
            if (string.IsNullOrEmpty(Vehicle.VinNumber))
            {
                errors.Add(new ValidationResult("Vin Number is a required field"));
            }
            else if (!Regex.IsMatch(Vehicle.VinNumber, vinPattern, RegexOptions.IgnoreCase))
            {
                errors.Add(new ValidationResult("Not a valid VIN - Must be 17 alpha-numeric characters, and not contain letters 'o', 'i', or 'q'"));
            }

            //MSRP - must be at least same as Sale Price
            if (Vehicle.MSRP < 0)
            {
                errors.Add(new ValidationResult("This car cannot have negative MSRP value!"));
            }
            if (Vehicle.MSRP < Vehicle.SalePrice)
            {
                errors.Add(new ValidationResult("MSRP must be greater than the Sale Price!"));
            }

            //Sale Price
            if (Vehicle.SalePrice < 0)
            {
                errors.Add(new ValidationResult("Sale Price must be greater than zero!"));
            }

            //vehicle description
            if (string.IsNullOrEmpty(Vehicle.VehicleDescription))
            {
                errors.Add(new ValidationResult("Vehicle Description is required"));
            }

            //VehicleImage is not empty, and contains only image suffixes
            if (ImageUpload != null && ImageUpload.ContentLength > 0)
            {
                var extensions = new string[] { ".jpg", ".png", ".gif", ".jpeg" };

                var extension = Path.GetExtension(ImageUpload.FileName);

                if (!extensions.Contains(extension.ToLower()))
                {
                    errors.Add(new ValidationResult("Image file must be a jpg, png, gif, or jpeg"));
                }
            }

            return errors;
        }
    }
}