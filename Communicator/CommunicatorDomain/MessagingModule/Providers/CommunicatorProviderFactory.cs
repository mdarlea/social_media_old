using System;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.Seedwork.Aggregates.ProfileAgg;

namespace Swaksoft.Domain.Communicator.MessagingModule.Providers
{
    public class CommunicatorProviderFactory : ICommunicatorProviderFactory
    {
        private readonly ActionsFactory<Profile, IProvider> _providersFactory;

        public CommunicatorProviderFactory(ActionsFactory<Profile, IProvider> providersFactory)
        {
            if (providersFactory == null) throw new ArgumentNullException("providersFactory");
            _providersFactory = providersFactory;
        }

        public T Create<T>(Profile profile) where T:IProvider
        {
            if (profile == null) throw new ArgumentNullException("profile");
            return _providersFactory.Create<T>(profile);
        }
    }
}
