using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.AddressAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.AddressTypeAgg;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Services
{
    public class AddressService: IAddressService
    {
        private readonly IAddressRepository addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            if (addressRepository == null) throw new ArgumentNullException(nameof(addressRepository));
            this.addressRepository = addressRepository;
        }
        public async Task<Address> GetAddressAsync(
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
            var specification = AddressSpecifications.AddressLatitudeAndLongitude(latitude, longitude);
            var addresses = await addressRepository.AllMatchingAsync(specification);

            //create a new address
            var newAddress = AddressFactory.CreateAddress(streetAddress,
                suiteNumber,
                city,
                state,
                zip,
                latitude,
                longitude,
                geolocationStreetNumber,
                geolocationStreet,
                countryIsoCode,
                isMainAddress,
                addressType);

            var persistent = addresses.FirstOrDefault(a => a.IsEqualWith(newAddress));
            return (persistent==null) ? newAddress : persistent;
        }

        #region dispose
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                addressRepository.Dispose();
            }
        }
        #endregion dispose
    }
}
