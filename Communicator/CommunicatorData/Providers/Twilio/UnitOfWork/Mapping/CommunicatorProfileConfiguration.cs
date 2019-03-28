using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicatorProfileAgg;

namespace Swaksoft.Infrastructure.Data.Communicator.UnitOfWork.Mapping
{
    public partial class ProfileConfiguration
    {
        partial void MapProviderProfiles()
        {
            Map<TwilioProfile>(m => m.Requires("Type").HasValue("Twilio"));
        }
    }
}
