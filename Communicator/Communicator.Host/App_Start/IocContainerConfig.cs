using Microsoft.Practices.Unity;
using System.Web.Http;

namespace Swaksoft.Communicator.Host
{
    public static class IocContainerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            var bootstrap = new UnityBootstrap(container);
            
            config.DependencyResolver = new UnityResolver(container);
        }
    }
}