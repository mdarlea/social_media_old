using Microsoft.Practices.Unity;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Infrastructure.Data.SocialMedia.UnitOfWork;

namespace SocialMedia.DI.Registry.Unity
{
    public class RegistryForUnitOfWorks : IConfigureUnity
    {
        public void Configure(IUnityContainer container)
        {
            container.RegisterType<ITransactionUnitOfWork, SocialMediaUnitOfWorkMySql>(
              new PerResolveLifetimeManager(),
              new InjectionConstructor("SocialMediaDataSource"));
        }
    }
}
