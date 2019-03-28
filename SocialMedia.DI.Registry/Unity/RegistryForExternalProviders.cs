using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Swaksoft.Core.External;
using Swaksoft.Domain.SocialMedia.SocialModule.Contracts;

namespace SocialMedia.DI.Registry.Unity
{
    public class RegistryForExternalProviders : IConfigureUnity
    {
        public void Configure(IUnityContainer container)
        {
            container.RegisterType<IEnumerable<ExternalProviderCredentials>, ExternalProviderCredentials[]>();
            container.RegisterType<IEnumerable<IUserProfileAdapter>, IUserProfileAdapter[]>();
        }
    }
}
