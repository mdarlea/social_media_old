using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swaksoft.Domain.Seedwork.Aggregates.ValueObjects;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.AddressTypeAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserAgg;
using Address = Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.AddressAgg.Address;

namespace Application.SocialMedia.Tests.Data
{
    public partial class SocialMediaDatabaseInitializer
    {
        partial void SeedAddresses(TestSocialMediaUnitOfWork context)
        {
            var addressType = new AddressType("Residential");
            context.AddressTypes.Add(addressType);

            addressType = new AddressType("Church");
            context.AddressTypes.Add(addressType);

            context.SaveChanges();

            var user = new User
            {
                Email = "mdarlea1982@yahoo.com",
                UserName = "mdarlea1982@yahoo.com",
                Name = new Name("Michelle", "Darlea")
            };
            user.ChangeCurrentIdentity("ef4b2bdb-eda9-4778-bc1c-ab347a4924f5");
            
            var address = new Address
            {
                StreetAddress = "10023 Belle Rive Blvd.",
                SuiteNumber = "Apt. 1204",
                GeolocationStreetNumber = 10023,
                GeolocationStreet = "Belle Rive Boulevard",
                City = "Jacksonville",
                State = "Florida",
                Zip = "32256",
                CountryIsoCode = "us",
                Latitude = 30.210796,
                Longitude = -81.5489216,
                IsMainAddress = true
            };
            context.Addresses.Add(address);
            user.Addresses.Add(address);
            
            addressType = context.AddressTypes.Find(2);
            address = new Address()
            {
                StreetAddress = "3668 Livernois Rd",
                City = "Troy",
                State = "MI",
                Zip = "48083",
                GeolocationStreetNumber = 3668,
                GeolocationStreet = "3668 Livernois Rd",
                CountryIsoCode = "us",
                Latitude = 42.572365,
                Longitude = -83.146155,
                IsMainAddress = false
            };
            address.SetTheAddressTypeForThisAddress(addressType);

            context.Addresses.Add(address);
          
            addressType = context.AddressTypes.Find(1);
            address = new Address()
            {
                StreetAddress = "1001 Lincoln St.",
                GeolocationStreetNumber = 1001,
                GeolocationStreet = "1001 Lincoln Street",
                City = "Southfield",
                State = "MI",
                Zip = "45345",
                CountryIsoCode = "us",
                Latitude = 20.12345,
                Longitude = 10.3214,
                IsMainAddress = true
            };
            address.SetTheAddressTypeForThisAddress(addressType);
            context.Addresses.Add(address);
       
            context.Users.Add(user);

            context.SaveChanges();
        }
    }
}
