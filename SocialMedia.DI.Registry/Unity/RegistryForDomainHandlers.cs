using Microsoft.Practices.Unity;
using Swaksoft.Application.SocialMedia.SocialModule.Handlers;
using Swaksoft.Domain.Seedwork.Events;
using Swaksoft.Domain.SocialMedia.SocialModule.Events;
using Swaksoft.Domain.SocialMedia.SocialModule.Events.Streaming;
using Swaksoft.Domain.SocialMedia.SocialModule.Handlers;

namespace SocialMedia.DI.Registry.Unity
{
    public class RegistryForDomainHandlers : IConfigureUnity
    {
        public void Configure(IUnityContainer container)
        {
            DomainEvents.SetCurrent(new UnityDomainEventDispatcher(container));

            container.RegisterType<IHandle<DisplayActivityOnMap>, DisplayActivityOnMapHandler>("DisplayActivityOnMapHandler");
            container.RegisterType<IHandle<UserProfileCreated>, UserProfileCreatedHandler>("UserProfileCreatedHandler");
            container.RegisterType<IHandle<TweetStreamed>, TweetStreamedHandler>("MessageStreamedHandler");
            container.RegisterType<IHandle<MessageSent>, TweetSentHandler>("TweetSentHandler");
        }
    }
}