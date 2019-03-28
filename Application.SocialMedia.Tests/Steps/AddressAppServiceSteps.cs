using System;
using System.Linq;
using System.Threading.Tasks;
using Application.SocialMedia.Tests.Data;
using Should;
using Swaksoft.Application.SocialMedia.Dto;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using System.Data.Entity;
using Domain = Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.AddressAgg;

namespace Application.SocialMedia.Tests.Steps
{
    [Binding]
    public class AddressAppServiceSteps
    {
        private readonly DataContext context;
        private AddressResult returnedAddressResultDto;
        private int givenAddressId;
        private Domain.Address givenAddress;
        private int newAddressId;
        
        public AddressAppServiceSteps(DataContext context)
        {
            this.context = context;
        }

        [AfterScenario("AddNewAddressForUser")]
        public void AddNewAddressForUser()
        {
            using (var dbContext = new TestSocialMediaUnitOfWork())
            {
                var user = dbContext.Users.Include(u => u.Addresses).SingleOrDefault(u => u.Id == context.GivenUserId);
                var address = dbContext.Addresses.FirstOrDefault(a => a.Id == newAddressId);

                user.Addresses.Remove(address);
                dbContext.Addresses.Remove(address);

                dbContext.SaveChanges();
            }
            
            returnedAddressResultDto = null;
         }

        [AfterScenario("UpdateAnExistingAddress")]
        public void AfterUpdateAnExistingAddress()
        {
            using (var dbContext = new TestSocialMediaUnitOfWork())
            {
                //restore the updated address
                var address = dbContext.Addresses.FirstOrDefault(a => a.Id == givenAddressId);

                address.StreetAddress = givenAddress.StreetAddress;
                address.SuiteNumber = givenAddress.SuiteNumber;
                address.City = givenAddress.City;
                address.State = givenAddress.State;
                address.Zip = givenAddress.Zip;
                address.GeolocationStreet = givenAddress.GeolocationStreet;
                address.GeolocationStreetNumber = givenAddress.GeolocationStreetNumber;
                address.IsMainAddress = givenAddress.IsMainAddress;
                address.Latitude = givenAddress.Latitude;
                address.Longitude = givenAddress.Longitude;
                address.CountryIsoCode = givenAddress.CountryIsoCode;

                address.SetTheAddressTypeForThisAddress(givenAddress.AddressType);

                dbContext.SaveChanges();
            }
          }

      
        [Given(@"The user with the '(.*)' id")]
        public void GivenTheUserWithTheId(string p0)
        {
            context.GivenUserId = p0;
            if(context.GivenEventForUserDto == null)
            {
                context.GivenEventForUserDto = new EventForUserRequest();
            }
            context.GivenEventForUserDto.UserId = p0;
       }
        
        [Given(@"the following address:")]
        public void GivenTheFollowingAddress(Table table)
        {
            context.GivenAddressDto = table.CreateInstance<Address>();
        }
        
        [When(@"I associate this address with the given user")]
        public async  Task WhenIassociateThisAddressWithTheGivenUser()
        {
            var service = context.GetAddressAppService();
            var dto = new AddAddressToUserRequest()
            {
                UserId = context.GivenUserId,
                Address = context.GivenAddressDto
            };
            returnedAddressResultDto = await service.AddAddressToUserAsync(dto);
        }

        [Then(@"A new address with the information below shoud be added to the database")]
        public void ThenANewAddressWithTheInformationBelowShoudBeAddedToTheDatabase(Table table)
        {
            var expectedAddress = table.CreateInstance<Domain.Address>();

            using (var dbContext = new TestSocialMediaUnitOfWork())
            {
                var newAddress = (
                    from a in dbContext.Addresses
                    where a.StreetAddress == expectedAddress.StreetAddress &&
                          a.SuiteNumber == expectedAddress.SuiteNumber &&
                          a.City == expectedAddress.City &&
                          a.State == expectedAddress.State &&
                          a.Zip == expectedAddress.Zip &&
                          a.CountryIsoCode == expectedAddress.CountryIsoCode &&
                          a.GeolocationStreetNumber == expectedAddress.GeolocationStreetNumber &&
                          a.GeolocationStreet == expectedAddress.GeolocationStreet &&
                          a.GeolocationStreetNumber == expectedAddress.GeolocationStreetNumber &&
                          a.Latitude == expectedAddress.Latitude &&
                          a.Longitude == expectedAddress.Longitude &&
                          a.IsMainAddress == expectedAddress.IsMainAddress

                    select a).FirstOrDefault();

                newAddressId = newAddress.Id;
                 
                newAddress.ShouldNotBeNull();

                newAddress.StreetAddress.ShouldEqual(expectedAddress.StreetAddress);
                newAddress.SuiteNumber.ShouldEqual(expectedAddress.SuiteNumber);
                newAddress.City.ShouldEqual(expectedAddress.City);
                newAddress.State.ShouldEqual(expectedAddress.State);
                newAddress.Zip.ShouldEqual(expectedAddress.Zip);
                newAddress.CountryIsoCode.ShouldEqual(expectedAddress.CountryIsoCode);
                newAddress.GeolocationStreet.ShouldEqual(expectedAddress.GeolocationStreet);
                newAddress.GeolocationStreetNumber.ShouldEqual(expectedAddress.GeolocationStreetNumber);
                newAddress.Longitude.ShouldEqual(expectedAddress.Longitude);
                newAddress.Latitude.ShouldEqual(expectedAddress.Latitude);
                newAddress.IsMainAddress.ShouldEqual(expectedAddress.IsMainAddress);
            }
        }
    
        
        [Then(@"the id of the new address should not be equal with (.*)")]
        public void ThenTheIdOfTheNewAddressShouldNotBeEqualWith(int p0)
        {
            newAddressId.ShouldNotEqual(p0);
        }
        
        [Then(@"the newly added address should be associated with the given user")]
        public void ThenTheNewlyAddedAddressShouldBeAssociatedWithTheGivenUser()
        {
            using (var dbContext = new TestSocialMediaUnitOfWork())
            {
                var user = dbContext.Users.Include(u => u.Addresses).SingleOrDefault(u => u.Id == context.GivenUserId);

                user.ShouldNotBeNull();

                var address = user.Addresses.SingleOrDefault(a => a.Id == newAddressId);

                address.ShouldNotBeNull();
            }
        }
        
        [Then(@"the given user should be associated with (.*) addresses")]
        public void ThenTheGivenUserShouldBeAssociatedWithAddresses(int p0)
        {
            using (var dbContext = new TestSocialMediaUnitOfWork())
            {
                var user = dbContext.Users.Include(u => u.Addresses).SingleOrDefault(u => u.Id == context.GivenUserId);
                user.Addresses.ToList().Count.ShouldEqual(p0);
            }
        }

        [Then(@"the address application service should return a dto with the following information")]
        public void ThenTheAddressApplicationServiceShouldReturnADtoWithTheFollowingInformation(Table table)
        {
            var expectedDto = table.CreateInstance<AddressResult>();

            returnedAddressResultDto.Status.ShouldEqual(expectedDto.Status);
            returnedAddressResultDto.StreetAddress.ShouldEqual(expectedDto.StreetAddress);
            returnedAddressResultDto.SuiteNumber.ShouldEqual(expectedDto.SuiteNumber);
            returnedAddressResultDto.City.ShouldEqual(expectedDto.City);
            returnedAddressResultDto.State.ShouldEqual(expectedDto.State);
            returnedAddressResultDto.Zip.ShouldEqual(expectedDto.Zip);
            returnedAddressResultDto.GeolocationStreet.ShouldEqual(expectedDto.GeolocationStreet);
            returnedAddressResultDto.GeolocationStreetNumber.ShouldEqual(expectedDto.GeolocationStreetNumber);
            returnedAddressResultDto.Latitude.ShouldEqual(expectedDto.Latitude);
            returnedAddressResultDto.Longitude.ShouldEqual(expectedDto.Longitude);
            returnedAddressResultDto.CountryIsoCode.ShouldEqual(expectedDto.CountryIsoCode);
        }

        [Then(@"the id of the returned address should be equal with the id of the newly created address")]
        public void ThenTheIdOfTheReturnedAddressShouldBeEqualWithTheIdOfTheNewlyCreatedAddress()
        {
            returnedAddressResultDto.Id.ShouldEqual(newAddressId);
        }
       

        [Then(@"The adress having the id equal with (.*) should be associated with the given user")]
        public void ThenTheAdressHavingTheIdEqualWithShouldBeAssociatedWithTheGivenUser(int p0)
        {
            using (var dbContext = new TestSocialMediaUnitOfWork())
            {
                var expectedAddress = dbContext.Addresses.SingleOrDefault(a => a.Id == p0);
                expectedAddress.ShouldNotBeNull();

                returnedAddressResultDto.Id.ShouldEqual(p0);
                returnedAddressResultDto.StreetAddress.ShouldEqual(expectedAddress.StreetAddress);
                returnedAddressResultDto.SuiteNumber.ShouldEqual(expectedAddress.SuiteNumber);
                returnedAddressResultDto.City.ShouldEqual(expectedAddress.City);
                returnedAddressResultDto.State.ShouldEqual(expectedAddress.State);
                returnedAddressResultDto.Zip.ShouldEqual(expectedAddress.Zip);
                returnedAddressResultDto.CountryIsoCode.ShouldEqual(expectedAddress.CountryIsoCode);
                returnedAddressResultDto.GeolocationStreet.ShouldEqual(expectedAddress.GeolocationStreet);
                returnedAddressResultDto.GeolocationStreetNumber.ShouldEqual(expectedAddress.GeolocationStreetNumber);
                returnedAddressResultDto.Longitude.ShouldEqual(expectedAddress.Longitude);
                returnedAddressResultDto.Latitude.ShouldEqual(expectedAddress.Latitude);
            }
        }

        [Given(@"the address with the id equal to (.*)")]
        public void GivenTheAddressWithTheIdEqualTo(int p0)
        {
            using (var dbContext = new TestSocialMediaUnitOfWork())
            {
                var address = dbContext.Addresses.FirstOrDefault(a => a.Id == p0);

                givenAddress = new Domain.Address()
                {
                    StreetAddress = address.StreetAddress,
                    SuiteNumber = address.SuiteNumber,
                    City = address.City,
                    State = address.State,
                    Zip = address.Zip,
                    GeolocationStreet = address.GeolocationStreet,
                    GeolocationStreetNumber = address.GeolocationStreetNumber,
                    IsMainAddress = address.IsMainAddress,
                    Latitude = address.Latitude,
                    Longitude = address.Longitude,
                    CountryIsoCode = address.CountryIsoCode
                };
                givenAddress.SetTheAddressTypeForThisAddress(address.AddressType);
                givenAddressId = p0;
            }
        }
        [When(@"I update the address with the given new address")]
        public void WhenIUpdateTheAddressWithTheGivenNewAddress()
        {
            var service = context.GetAddressAppService();
            service.UpdateAddress(context.GivenAddressDto);
        }

        [Then(@"the given address entity should be updated with the information from the given  address dto")]
        public void ThenTheGivenAddressEntityShouldBeUpdatedWithTheInformationFromTheGivenAddressDto()
        {
            using (var dbContext = new TestSocialMediaUnitOfWork())
            {
                var givenAddress = dbContext.Addresses
                    .FirstOrDefault(a => a.Id == givenAddressId);

                var givenAddressDto = context.GivenAddressDto;

                givenAddress.ShouldNotBeNull();
                givenAddress.Id.ShouldEqual(givenAddressDto.Id);
                givenAddress.StreetAddress.ShouldEqual(givenAddressDto.StreetAddress);
                givenAddress.City.ShouldEqual(givenAddressDto.City);
                givenAddress.State.ShouldEqual(givenAddressDto.State);
                givenAddress.Zip.ShouldEqual(givenAddressDto.Zip);
                givenAddress.AddressTypeId.ShouldEqual(givenAddressDto.AddressTypeId);
                givenAddress.GeolocationStreetNumber.ShouldEqual(givenAddressDto.GeolocationStreetNumber);
                givenAddress.GeolocationStreet.ShouldEqual(givenAddressDto.GeolocationStreet);
                givenAddress.Latitude.ShouldEqual(givenAddressDto.Latitude);
                givenAddress.Longitude.ShouldEqual(givenAddressDto.Longitude);
                givenAddress.IsMainAddress.ShouldEqual(givenAddressDto.IsMainAddress);
                givenAddress.CountryIsoCode.ShouldEqual(givenAddressDto.CountryIsoCode);
                givenAddress.AddressTypeId.ShouldEqual(givenAddressDto.AddressTypeId);
            }
        }
    }
}
