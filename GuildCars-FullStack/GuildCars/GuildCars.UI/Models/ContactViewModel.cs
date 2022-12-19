using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class ContactViewModel : IValidatableObject
    {
        public IEnumerable<Contact> Contacts { get; set; }
        public Contact Contact { get; set; }
        public string MapKey { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (string.IsNullOrEmpty(Contact.ContactName))
            {
                errors.Add(new ValidationResult("What is your name?"));
            }
            if (string.IsNullOrEmpty(Contact.ContactMessage))
            {
                errors.Add(new ValidationResult("What do you want to message us about?"));
            }

            //ensure that either email or phone are provided, and validate their contents, if present
            if (string.IsNullOrEmpty(Contact.ContactEmail) && string.IsNullOrEmpty(Contact.ContactPhone))
            {
                errors.Add(new ValidationResult("Please provide either a phone number or email, or both! :)"));
            }
            if (!string.IsNullOrWhiteSpace(Contact.ContactPhone))
            {
                if (Contact.ContactPhone.Length != 12)
                {
                    errors.Add(new ValidationResult("Please provide phone number in US format XXX-XXX-XXXX"));
                }
                if (!(new PhoneAttribute().IsValid(Contact.ContactPhone)))
                {
                    errors.Add(new ValidationResult("That phone number is not considered valid."));
                }
            }
            if (!string.IsNullOrWhiteSpace(Contact.ContactEmail))
            {
                if (!(new EmailAddressAttribute().IsValid(Contact.ContactEmail)))
                {
                    errors.Add(new ValidationResult("That email not considered valid."));
                }
            }

            return errors;
        }
    }
}