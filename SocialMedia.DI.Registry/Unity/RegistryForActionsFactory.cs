using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Swaksoft.Application.SocialMedia.SocialModule.Providers;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.Seedwork.Specification;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.EventAgg;

namespace SocialMedia.DI.Registry.Unity
{
    public class RegistryForActionsFactory  : IConfigureUnity
    {
        public void Configure(IUnityContainer container)
        {
            //sigleton
            container.RegisterType<ActionsFactory<IProviderFactory>, ProviderActionsFactory>(
                new ContainerControlledLifetimeManager());
          
        }
    }
}
