using Microsoft.Practices.Unity;
using Swaksoft.Application.SocialMedia.SocialModule.Providers;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.SocialMedia.SocialModule.Services;

namespace SocialMedia.DI.Registry.Unity
{
    public class RegistryForDomainServices : IConfigureUnity
    {
        public void Configure(IUnityContainer container)
        {
            container.RegisterType<ITweetProcessorService, TweetProcessorService>();
            container.RegisterType<IMessageSenderService, MessageSenderService>();

            container.RegisterType<IAddressService, AddressService>();
        }
    }
}
