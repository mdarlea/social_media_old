using System;
using System.Linq;
using Microsoft.Practices.Unity;
using SocialMedia.DI.Registry;
using SocialMedia.DI.Registry.Unity;

namespace SocialMedia.Host.Configuration.Unity
{
    public class Bootstrap
    {
       public void Register(IUnityContainer container)
        {
            RegisterReflectedTypes(container);
        }

        private static void RegisterReflectedTypes(IUnityContainer container)
        {
            var profiles = AppDomain.CurrentDomain
                                   .GetAssemblies()
                                   .Where(a =>
                                       !a.FullName.Contains("Microsoft.Practices.Unity") &&
                                       !a.FullName.Contains("Nito") &&
                                       !a.FullName.Contains("AutoMapper") &&
                                       !a.FullName.Contains("Tweetinvi"))
                                   .SelectMany(a => a.GetTypes())
                                   .Where(t => t.GetInterfaces().Contains(typeof(IConfigureUnity)));
            foreach (var item in profiles)
            {
               var instance = Activator.CreateInstance(item) as IConfigureUnity;
                instance?.Configure(container);

            }
        }
    }
}