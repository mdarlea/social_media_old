using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Swaksoft.Application.Seedwork.Extensions;
using Swaksoft.Application.Seedwork.Services;
using Swaksoft.Application.SocialMedia.Dto;
using Swaksoft.Core.Dto;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.AddressAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.AddressTypeAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Services;
using Swaksoft.Infrastructure.Crosscutting.Extensions;
using Swaksoft.Infrastructure.Crosscutting.Validation;
using Address = Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.AddressAgg.Address;

namespace Swaksoft.Application.SocialMedia.SocialModule.Services
{
    public class AddressAppService : AppServiceBase<AddressAppService>, IAddressAppService
    {
        private readonly IAddressRepository addressRepository;
        private readonly IUserRepository userRepository;
        private readonly IAddressService addressService;

        public AddressAppService(
            IAddressRepository addressRepository, 
            IUserRepository userRepository,
            IAddressService addressService)
        {
            if (addressRepository == null) throw new ArgumentNullException(nameof(addressRepository));
            if (userRepository == null) throw new ArgumentNullException(nameof(userRepository));
            if (addressService == null) throw new ArgumentNullException(nameof(addressService));

            this.addressRepository = addressRepository;
            this.userRepository = userRepository;
            this.addressService = addressService;
        }

        public async Task<Dto.AddressResult> AddNewAddressAsync(Dto.Address address)
        {
            var log = GetLog();

            var addr = await addressService.GetAddressAsync(
                address.StreetAddress,
                address.SuiteNumber,
                address.City,
                address.State,
                address.Zip,
                address.Latitude,
                address.Longitude,
                address.GeolocationStreetNumber,
                address.GeolocationStreet,
                address.CountryIsoCode,
                address.IsMainAddress);

            if (addr.IsTransient())
            {
                var result = await addressRepository.SaveEntityAsync(addr);
                return result.ProjectAs<Dto.AddressResult>();
            }
                

            log.LogWarning($"Attempt to create a duplicate address for {addr.Latitude},{addr.Longitude}");
            return addr.ProjectedAs<Dto.AddressResult>();
        }

        public Dto.CollectionActionResult<Dto.Address> FindAddressesForUser(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException(nameof(userId));

            var dto= new CollectionActionResult<Dto.Address>();

            var user = userRepository.GetUserWithAddresses(userId);
            if (user == null)
            {
                dto.Status = ActionResultCode.Failed;
                dto.Message = $"Cannot find a user with {userId} id";
                return dto;
            }

            dto.Status = ActionResultCode.Success;
            dto.Items = user.Addresses.ProjectedAsCollection<Dto.Address>();
            return dto;
        }

        public async Task<Dto.AddressResult> AddAddressToUserAsync(Dto.AddAddressToUserRequest addAddressToUser)
        {
            if (addAddressToUser == null) throw new ArgumentNullException(nameof(addAddressToUser));
            if (addAddressToUser.Address == null) throw new ArgumentException(@"Address cannot be null", nameof(addAddressToUser));
            var log = GetLog();

            var address = addAddressToUser.Address;

            //get the user
            //var user = userRepository.Get(addAddressToUser.UserId);
            var user = userRepository.GetUserWithAddresses(addAddressToUser.UserId);
            if (user == null) throw new InvalidDataException(
                    $"Could not find a user with {addAddressToUser.UserId} id");

            //create a new address
           var addr = await addressService.GetAddressAsync(
                    address.StreetAddress,
                    address.SuiteNumber,
                    address.City,
                    address.State,
                    address.Zip,
                    address.Latitude,
                    address.Longitude,
                    address.GeolocationStreetNumber,
                    address.GeolocationStreet,
                    address.CountryIsoCode,
                    address.IsMainAddress);

            using (var uofw = userRepository.BeginTransaction())
            {
                if (addr.IsTransient())
                {
                    //validates the new address
                    var entityValidator = EntityValidatorLocator.CreateValidator();
                    var isValid = entityValidator.IsValid(address);
                    if (!isValid)
                    {
                        return new AddressResult
                        {
                            Status = ActionResultCode.Failed,
                            Message = "Invalid address",
                            Errors = entityValidator.GetInvalidMessages(address).ToList()
                        };
                    }
                }

                if (user.AddAddress(addr))
                {
                    await uofw.CommitAsync();
                }
            }
            return addr.ProjectedAs<Dto.AddressResult>();
        }
        
        public void UpdateAddress(Dto.Address address)
        {
            if (address == null) throw new ArgumentNullException(nameof(address));

            //get the current address
            var persisted = addressRepository.Get(address.Id);
            if (persisted == null)
            {
                throw new DataException($"Could not find an address with the {address.Id} id");
            }

            //the updated address
            var current = MaterializeAddressFromDto(address);

            using (var uofw = addressRepository.BeginTransaction())
            {
                addressRepository.Merge(persisted,current);
                uofw.Commit();
            }
        }

        public static Address MaterializeAddressFromDto(Dto.Address address)
        {
            AddressType addressType=null;
            var addressTypeId = address.AddressTypeId ?? 0;
           
            if (addressTypeId > 0)
            {
                addressType = new AddressType("empty");
                addressType.ChangeCurrentIdentity(addressTypeId);
            }
            var newAddress = AddressFactory.CreateAddress(address.StreetAddress,
                                                           address.SuiteNumber,
                                                           address.City,
                                                           address.State,
                                                           address.Zip,
                                                           address.Latitude,
                                                           address.Longitude,
                                                           address.GeolocationStreetNumber,
                                                           address.GeolocationStreet,
                                                           address.CountryIsoCode,
                                                           address.IsMainAddress,
                                                           addressType);
            if (addressTypeId > 0)
            {
                newAddress.SetTheAddressTypeReference(addressTypeId);
            }
            newAddress.ChangeCurrentIdentity(address.Id);

            return newAddress;

        }



        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (!disposing) return;

            addressRepository.Dispose();
            userRepository.Dispose();
            addressService.Dispose();
        }

		public AddressResult FindAddressById(int id)
		{
			var address = addressRepository.Get(id);
			if (address == null) {
				return new AddressResult
				{
					Status = ActionResultCode.Failed,
					Message = $"Could not find an adress with the {id} id"
				};
			}
			return address.ProjectedAs<AddressResult>();
		}
	}
   
}
