using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;

namespace SocialMedia.DI.Registry.Unity
{
    public class UnityApiDependencyResolverFactory : IApiDependencyResolverFactory, IMvcDependencyResolverFactory
    {
        private readonly IUnityContainer container;

        public UnityApiDependencyResolverFactory(IUnityContainer container)
        {
            this.container = container;
        }

        IDependencyResolver IApiDependencyResolverFactory.CreateResolver()
        {
            return new UnityResolver(container);
        }

        System.Web.Mvc.IDependencyResolver IMvcDependencyResolverFactory.CreateResolver()
        {
            var resolver = new UnityDependencyResolver(container);
            container.RegisterInstance<System.Web.Mvc.IDependencyResolver>(resolver);
            return resolver;
        }
    }
}