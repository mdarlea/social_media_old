using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialMedia.Host.Models
{
    public class AddressModel
    {
        [Display(Name = @"Street Address")]
        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string SuiteNumber { get; set; }

        [Required]
        [Display(Name = @"Region / State")]
        public string State { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = @"Zip Code")]
        public string Zip { get; set; }

        [Display(Name = @"Country")]
        public string CountryIsoCode { get; set; }

        public int GeolocationStreetNumber { get; set; }
        public string GeolocationStreet { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}