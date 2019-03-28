using Microsoft.Practices.Unity;
using Swaksoft.Domain.SocialMedia.SocialModule.Contracts;
using Swaksoft.Domain.SocialMedia.SocialModule.Contracts.Streaming;
using ServiceAgent = Swaksoft.Infrastructure.AntiCorruption;

namespace SocialMedia.DI.Registry.Unity
{
    public class RegistryForServiceAgents : IConfigureUnity
    {
        public void Configure(IUnityContainer container)
        {
            container.RegisterType<IMessageSenderAdapter, ServiceAgent.Twitter.Services.MessageSenderAdapter> ();

            container.RegisterType<IClientsRegistry, ClientsRegistry>();
        }
    }
}
