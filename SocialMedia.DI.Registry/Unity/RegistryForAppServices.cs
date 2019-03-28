using System;
using Microsoft.Practices.Unity;
using Swaksoft.Application.SocialMedia.SocialModule.Services;

namespace SocialMedia.DI.Registry.Unity
{
    public class RegistryForAppServices : IConfigureUnity
    {
        public void Configure(IUnityContainer container)
        {
            container.RegisterType<IAddressAppService, AddressAppService>();
            container.RegisterType<IEventAppService, EventAppService>();
        }
    }
}
