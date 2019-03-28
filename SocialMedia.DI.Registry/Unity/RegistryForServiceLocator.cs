using System;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace SocialMedia.DI.Registry.Unity
{
    public class RegistryForServiceLocator : IConfigureUnity
    {
        public void Configure(IUnityContainer container)
        {
            var locator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);
        }
    }
}
