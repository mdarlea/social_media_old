using System;
using System.Threading.Tasks;

namespace Swaksoft.Application.SocialMedia.SocialModule.Services
{
    public interface IAddressAppService : IDisposable
    {
		Dto.AddressResult FindAddressById(int id);
        Dto.CollectionActionResult<Dto.Address> FindAddressesForUser(string userId);
        Task<Dto.AddressResult> AddNewAddressAsync(Dto.Address address);
        Task<Dto.AddressResult> AddAddressToUserAsync(Dto.AddAddressToUserRequest addAddressToUser);
        void UpdateAddress(Dto.Address address);
    }
}
