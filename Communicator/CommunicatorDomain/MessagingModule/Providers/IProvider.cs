using Swaksoft.Domain.Seedwork.Aggregates.ProfileAgg;

namespace Swaksoft.Domain.Communicator.MessagingModule.Providers
{
    public interface IProvider
    {
        Profile Profile { get; }
    }
}
