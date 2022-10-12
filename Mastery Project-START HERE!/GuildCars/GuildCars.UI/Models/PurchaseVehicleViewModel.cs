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
    public class PurchaseVehicleViewModel : IValidatableObject
    {
        public Purchase Purchase { get; set; }

        public PurchaseItem confirmedPurchase { get; set; }
        public IEnumerable<SelectListItem> State { get; set; }
        public IEnumerable<SelectListItem> PaymentType { get; set; }


        public VehicleDetails Vehicle { get; set; }



        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            Purchase.VehicleId = Vehicle.VehicleId;
            List<ValidationResult> errors = new List<ValidationResult>();

            if (Purchase.VehicleId == 0)
            {
                errors.Add(new ValidationResult("Purchase not assigned to a Vehicle ID"));
            }

            if (string.IsNullOrWhiteSpace(Purchase.CustomerName))
            {
                errors.Add(new ValidationResult("Customer must have a name"));
            }

            //ensure that either email or phone are provided, and validate their contents, if present
            if (string.IsNullOrEmpty(Purchase.CustomerPhone) && string.IsNullOrEmpty(Purchase.CustomerEmail))
            {
                errors.Add(new ValidationResult("How do we contact the customer? Must provide either an email or phone (or both)"));
            }
            if (!string.IsNullOrWhiteSpace(Purchase.CustomerPhone))
            {
                if (Purchase.CustomerPhone.Length != 12)
                {
                    errors.Add(new ValidationResult("Please provide phone number in US format XXX-XXX-XXXX"));
                }
                if (!(new PhoneAttribute().IsValid(Purchase.CustomerPhone)))
                {
                    errors.Add(new ValidationResult("Customer phone not considered valid."));
                }
            }
            if (!string.IsNullOrWhiteSpace(Purchase.CustomerEmail))
            {
                if (!(new EmailAddressAttribute().IsValid(Purchase.CustomerEmail)))
                {
                    errors.Add(new ValidationResult("Customer email not considered valid."));
                }
            }

            if (string.IsNullOrEmpty(Purchase.CustomerStreet1))
            {
                errors.Add(new ValidationResult("Customer Street1 must not be empty"));
            }
            if (!string.IsNullOrWhiteSpace(Purchase.CustomerStreet2))
            {
                if (Purchase.CustomerStreet2.Length > 150)
                {
                    errors.Add(new ValidationResult("Street2 is quite long; please keep under 150 characters"));
                }
            }

            if (string.IsNullOrEmpty(Purchase.CustomerCity))
            {
                errors.Add(new ValidationResult("Customer has no city; where do they live?"));
            }

            if (string.IsNullOrWhiteSpace(Purchase.CustomerZip))
            {
                errors.Add(new ValidationResult("Zip is empty"));
            }
            else if (!int.TryParse(Purchase.CustomerZip, out int zip))
            {
                errors.Add(new ValidationResult("Zip must be a number"));
            }
            else if (Purchase.CustomerZip.Length != 5)
            {
                errors.Add(new ValidationResult("Customer Zipcode must be exactly 5 digits long"));
            }

            if (Purchase.PurchasePrice > Vehicle.MSRP)
            {
                errors.Add(new ValidationResult("Purchase price cannot exceed MSRP"));
            }

            var salePrice = Vehicle.SalePrice;
            decimal minPurchPrice = .95M;
            if (Purchase.PurchasePrice < Decimal.Multiply(salePrice, minPurchPrice))
            {
                errors.Add(new ValidationResult("Purchase Price cannot be less than 95% of vehicle Sale Price"));
            }            


            return errors;
        }
    }
}