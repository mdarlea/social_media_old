using System;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using SocialMedia.DI.Registry;
using SocialMedia.DI.Registry.Unity;
using SocialMedia.Host.Configuration.Unity;

namespace SocialMedia.Host
{
    public static class IocContainerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            var bootstrap = new Bootstrap();
            bootstrap.Register(container);

            var factory = new UnityApiDependencyResolverFactory(container);

            //Web API resolver
            config.DependencyResolver = ((IApiDependencyResolverFactory)factory).CreateResolver();

            //MVC resolver
            DependencyResolver.SetResolver(((IMvcDependencyResolverFactory)factory).CreateResolver());
        }
    }
}