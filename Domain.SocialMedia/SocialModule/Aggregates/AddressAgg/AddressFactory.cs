using System;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.AddressTypeAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.CountryAgg;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.AddressAgg
{
    public static class AddressFactory
    {
        public static Address CreateAddress(
            string streetAddress,
            string suiteNumber,
            string city,
            string state,
            string zip,
            double latitude,
            double longitude,
            int geolocationStreetNumber,
            string geolocationStreet,
            string countryIsoCode,
            bool isMainAddress,
            AddressType addressType = null)
        {
            var streetAddr = streetAddress?.Trim();

            var suite = suiteNumber?.Trim();

            //check if an appartment or suite number was specified in the street address
            if (string.IsNullOrEmpty(suite))
            {
                suite = Address.GetSuiteNumberFromStreetAddress(streetAddr);
                if (!string.IsNullOrWhiteSpace(suite))
                {
                    streetAddr = streetAddr?.Replace(suite, string.Empty).Trim();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(streetAddr))
                {
                    var position = streetAddr.Length - suite.Length;
                    var apt = streetAddr.Substring(position);
                    if (apt.Equals(suite))
                    {
                        streetAddr = streetAddress.Substring(0, position).Trim();
                        if (!string.IsNullOrEmpty(streetAddr) && streetAddr.Substring(streetAddr.Length - 1, 1) == ",")
                        {
                            streetAddr = streetAddr.Substring(0, streetAddr.Length - 1);
                        }
                    }
                }
            }
            var address = new Address
            {
                StreetAddress = streetAddr,
                SuiteNumber = suite,
                City = city.Trim(),
                State = state.Trim(),
                Zip = zip.Trim(),
                CountryIsoCode = countryIsoCode,
                Latitude = latitude,
                Longitude = longitude,
                GeolocationStreetNumber = geolocationStreetNumber,
                GeolocationStreet = geolocationStreet,
                IsMainAddress = isMainAddress
            };
            if (addressType != null)
            {
                address.SetTheAddressTypeForThisAddress(addressType);
            }
            
            return address;
        }
    }
}
