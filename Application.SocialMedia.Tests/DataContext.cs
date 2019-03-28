using System.Collections.Generic;
using Application.SocialMedia.Tests.Data;
using Application.SocialMedia.Tests.ExternalApps;
using Swaksoft.Application.SocialMedia.Dto;
using Swaksoft.Application.SocialMedia.SocialModule.Services;
using Swaksoft.Core.External;
using Swaksoft.Domain.SocialMedia.SocialModule.Contracts;
using Swaksoft.Domain.SocialMedia.SocialModule.Contracts.Streaming;
using Swaksoft.Domain.SocialMedia.SocialModule.Services;
using Swaksoft.Infrastructure.Data.SocialMedia.SocialModule.Repositories;
using Twitter = Swaksoft.Infrastructure.AntiCorruption.Twitter.Services;
using Foursquare = Swaksoft.Infrastructure.AntiCorruption.Foursquare.Services;

namespace Application.SocialMedia.Tests
{
    public class DataContext
    {
        public EventWithAddress GivenEventDto { get; set; }
        public EventForUserRequest GivenEventForUserDto { get; set; }
        public string GivenUserId { get; set; }
        public int GivenEventId { get; set; }
        public Address GivenAddressDto { get; set; }
        public List<OAuthRequest> OAuthRequests { get; set; }
        public UserProfileRequest UserProfileRequest { get; set; }
        public StreamFilterRequest StreamFilterRequest { get; set; }
        public MessageOperationRequest MessageOperationRequest { get; set; }

        public IAddressAppService GetAddressAppService()
        {
            var uofw = new ProfiledSocialMediaUnitOfWork();
            return new AddressAppService(
                new AddressRepository(uofw),
                new UserRepository(uofw),
                new AddressService(new AddressRepository(uofw)));
        }

        public IEventAppService GetEventAppService()
        {
            var uofw = new ProfiledSocialMediaUnitOfWork();
            return new EventAppService(new EventRepository(uofw),
                new UserRepository(uofw),
                new AddressRepository(uofw), 
                new AddressService(new AddressRepository(uofw)));
        }

        public IUserAppService GetUserAppService()
        {
             var uofw = new ProfiledSocialMediaUnitOfWork();

            return new UserAppService(
                new UserRepository(uofw),
                new StreamFilterRepository(uofw), 
                new List<ExternalProviderCredentials>
                {
                    new TwitterCredentials(),
                    new FoursquareCredentials()
                },
                new List<IUserProfileAdapter>
                {
                    new Twitter.UserProfileAdapter(),
                    new Foursquare.UserProfileAdapter()
                }) ;
        }

        public IRealTimeStreamingAppService GetTwitterStreamingAppService()
        {
            var uofw = new ProfiledSocialMediaUnitOfWork();
            var clients = new ClientsRegistry();

            return new TwitterStreamingAppService(
                new RealTimeStreamingFactory(new UserRepository(uofw), new StreamFilterRepository(uofw)), 
                new Twitter.StreamingAdapter(clients));
        }

        public IMessageSenderAdapter GetMessageSenderServiceAgent()
        {
            return new Twitter.MessageSenderAdapter();
        }
    }
}
