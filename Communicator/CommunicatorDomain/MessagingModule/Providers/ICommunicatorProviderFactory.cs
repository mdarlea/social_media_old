using Swaksoft.Domain.Seedwork.Aggregates.ProfileAgg;

namespace Swaksoft.Domain.Communicator.MessagingModule.Providers
{
    public interface ICommunicatorProviderFactory
    {
        T Create<T>(Profile profile) where T : IProvider;
    }
}