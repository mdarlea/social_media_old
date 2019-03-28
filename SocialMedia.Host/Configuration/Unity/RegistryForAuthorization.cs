using System;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;
using SocialMedia.DI.Registry;
using SocialMedia.Host.Authorization.Providers;

namespace SocialMedia.Host.Configuration.Unity
{
    public class RegistryForAuthorization : IConfigureUnity{
        public void Configure(IUnityContainer container)
        {
            container.RegisterType<OAuthAuthorizationServerProvider, SimpleAuthorizationServerProvider>(
                new ContainerControlledLifetimeManager());
        }
    }
}