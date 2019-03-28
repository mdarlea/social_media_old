using System.Threading.Tasks;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.AddressAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.AddressTypeAgg;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Services
{
    public interface IAddressService: System.IDisposable
	{
        Task<Address> GetAddressAsync(
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
            AddressType addressType = null);
    }
}