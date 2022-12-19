using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class SpecialsAdminViewModel : IValidatableObject
    {
        public IEnumerable<Special> Specials { get; set; }
        public Special Special { get; set; }
        public HttpPostedFileBase ImageUpload { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (string.IsNullOrEmpty(Special.SpecialTitle))
            {
                errors.Add(new ValidationResult("This Special needs a title!"));
            }
            else if ((Special.SpecialTitle).Length > 50)
            {
                errors.Add(new ValidationResult("Title must be 50 characters or less!"));
            }

            if (string.IsNullOrEmpty(Special.SpecialDescription))
            {
                errors.Add(new ValidationResult("This Special needs a description!"));
            }
            else if (Special.SpecialDescription.Length > 850)
            {
                errors.Add(new ValidationResult("Description must be 850 characters or less!"));
            }

            //Special is not empty, and contains only image suffixes
            if (ImageUpload != null && ImageUpload.ContentLength > 0)
            {
                var extensions = new string[] { ".jpg", ".png", ".gif", ".jpeg" };

                var extension = Path.GetExtension(ImageUpload.FileName);

                if (!extensions.Contains(extension.ToLower()))
                {
                    errors.Add(new ValidationResult("Image file must be a jpg, png, gif, or jpeg"));
                }
            }
            else
            {
                errors.Add(new ValidationResult("Image file is required"));
            }


            return errors;
        }
    }
}