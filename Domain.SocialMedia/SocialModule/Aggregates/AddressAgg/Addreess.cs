using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Swaksoft.Domain.Seedwork.Aggregates;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.AddressTypeAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserAgg;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.AddressAgg
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public class Address : Entity
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
		#region properties   
		[MaxLength(100)]
		public string PlaceName { get; set; }

        [MaxLength(50)]
        public string StreetAddress { get; set; }

        [MaxLength(10)]
        public string SuiteNumber { get; set; }
        
        public int GeolocationStreetNumber { get; set; }

        [MaxLength(50)]
        public string GeolocationStreet { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [MaxLength(30)]
        public string State { get; set; }

        [MaxLength(20)]
        [Required(ErrorMessage = "Zip code is required")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Country ISO code is required")]
        [MaxLength(50)]
        public string CountryIsoCode { get; set; }
       
        [Required(ErrorMessage = "Latitude is required")]
        public double Latitude { get; set; }

        [Required(ErrorMessage = "Longitude is required")]
        public double Longitude { get; set; }

        [Required]
        public bool IsMainAddress { get; set; }

        public int? AddressTypeId { get; private set; }
        public virtual AddressType AddressType { get; private set; }

        #endregion properties
        
        public void SetTheAddressTypeForThisAddress(AddressType addressType)
        {
            if (addressType == null || addressType.IsTransient())
            {
                throw new ArgumentException("Cannot associate transient or null address type");
            }

            AddressTypeId = addressType.Id;
            AddressType = addressType;
        }

        public void SetTheAddressTypeReference(int addressTypeId)
        {
            if (addressTypeId > 0)
            {
                //fix relation
                AddressTypeId = addressTypeId;

                AddressType = null;
            }
        }

        public bool IsEqualWith(Address address)
        {
            if ((!Latitude.Equals(address.Latitude)) || (!Longitude.Equals(address.Longitude)))
                return false;
            if (Equals(address, StringComparison.CurrentCultureIgnoreCase)) return true;

            if (!City.Trim().Equals(address.City.Trim(), StringComparison.CurrentCultureIgnoreCase) ||
                !State.Trim().Equals(address.State.Trim(), StringComparison.CurrentCultureIgnoreCase) ||
                !CountryIsoCode.Equals(address.CountryIsoCode))
            {
                return false;
            }

            //check if zip code is equal
            var currentZip = Zip.Trim();
            var zip = address.Zip.Trim();
            if (currentZip.Length > zip.Length)
            {
                if (!currentZip.Substring(0, zip.Length).Equals(zip))
                {
                    return false;
                }
            }
            else
            {
                if (!zip.Substring(0, currentZip.Length).Equals(currentZip))
                {
                    return false;
                }
            }

            var currentSuite = SuiteNumber?.Trim();
            var currentStreet = StreetAddress?.Trim();
            if (string.IsNullOrEmpty(currentSuite))
            {
                currentSuite = GetSuiteNumberFromStreetAddress(currentStreet);
                if (!string.IsNullOrWhiteSpace(currentSuite))
                {
                    currentStreet = currentStreet?.Replace(currentSuite, string.Empty).Trim();
                }
            }

            var suite = address.SuiteNumber?.Trim();
            var street = address.StreetAddress?.Trim();
            if (string.IsNullOrEmpty(suite))
            {
                suite = GetSuiteNumberFromStreetAddress(street);
                if (!string.IsNullOrWhiteSpace(suite))
                {
                    street = street?.Replace(suite, string.Empty).Trim();
                }
            }

            if (string.IsNullOrEmpty(currentStreet))
            {
                if (!string.IsNullOrEmpty(street)) return false;
            }
            else
            {
                if (string.IsNullOrEmpty(currentSuite))
                {
                    if (!string.IsNullOrEmpty(suite)) return false;
                }
            }

            if (!currentStreet.Equals(street) && !currentSuite.Equals(suite)) return false;


            return true;

        }

        public static string GetSuiteNumberFromStreetAddress(string streetAddress)
        {
            if (string.IsNullOrWhiteSpace(streetAddress)) return null;

            var streetAddr = streetAddress.Trim();

            //check if the street number is at the begining of street address
            var idx = streetAddr.IndexOf(" ", StringComparison.Ordinal);
            if (idx < 0) return null;

            var value = streetAddr.Substring(0, idx).Trim();

            idx = streetAddr.LastIndexOf(" ", StringComparison.Ordinal);
            if (idx < 0) return null;

            var valueStr = streetAddr.Substring(idx + 1).Trim();

            var values = valueStr.Split(new[] { "#", "." }, StringSplitOptions.None);
            if (values.Length > 0)
            {
                valueStr = values[values.Length - 1];
            }

            var isNumber = Regex.IsMatch(valueStr, @"^\d+$");

            if (Regex.IsMatch(value, @"^\d+$"))
            {
                return (isNumber) ? valueStr : null;
            }
            if (!isNumber) return null;

            //check for street number
            value = streetAddr.Substring(0, idx).Trim();

            idx = value.LastIndexOf(" ", StringComparison.Ordinal);
            if (idx < 0) return null;

            value = value.Substring(idx + 1).Trim();

            values = value.Split(new[] {"#", "."}, StringSplitOptions.None);
            if (values.Length > 0)
            {
                value = values[values.Length - 1];
            }

            return (Regex.IsMatch(value, @"^\d+$")) ? valueStr : null;
        }
       

        #region equality

        protected bool Equals(Address other, StringComparison comparison)
        {
            return base.Equals(other) && 
                string.Equals(StreetAddress, other.StreetAddress, comparison) && 
                string.Equals(SuiteNumber, other.SuiteNumber,comparison) && 
                GeolocationStreetNumber == other.GeolocationStreetNumber && 
                string.Equals(GeolocationStreet, other.GeolocationStreet,comparison) && 
                string.Equals(City, other.City,comparison) && 
                string.Equals(State, other.State,comparison) && 
                string.Equals(Zip, other.Zip,comparison) && 
                string.Equals(CountryIsoCode, other.CountryIsoCode,comparison) && 
                Latitude.Equals(other.Latitude) && 
                Longitude.Equals(other.Longitude);
        }

        public bool Equals(object obj, StringComparison comparison)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Address)obj,comparison);
        }

        public override bool Equals(object obj)
        {
            return Equals((Address) obj, StringComparison.CurrentCultureIgnoreCase);
        }
        #endregion equality
    }
}
