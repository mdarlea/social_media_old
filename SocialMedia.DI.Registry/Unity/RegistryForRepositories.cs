using Microsoft.Practices.Unity;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.AddressAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.EventAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.MessageAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.MessageOperationAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.SentMessageAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamedTweetAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamFilterAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamingEventAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg;
using Swaksoft.Infrastructure.Data.SocialMedia.SocialModule.Repositories;

namespace SocialMedia.DI.Registry.Unity
{
    public class RegistryForRepositories : IConfigureUnity
    {
        public void Configure(IUnityContainer container)
        {
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IUserProfileRepository, UserProfileRepository>();
            container.RegisterType<IStreamedTweetRepository, StreamedTweetRepository>();
            container.RegisterType<ISentMessageRepository, SentMessageRepository>();
            container.RegisterType<IMessageRepository, MessageRepository>();
            container.RegisterType<IMessageOperationRepository, MessageOperationRepository>();
            container.RegisterType<IStreamFilterRepository, StreamFilterRepository>();
            container.RegisterType<IStreamingEventRepository, StreamingEventRepository>();
            container.RegisterType<IAddressRepository, AddressRepository>();
            container.RegisterType<IEventRepository, EventRepository>();
        }
    }
}
