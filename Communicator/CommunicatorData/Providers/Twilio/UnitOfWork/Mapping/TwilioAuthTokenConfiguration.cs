using System.Data.Entity.ModelConfiguration;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicatorProfileAgg;

namespace Swaksoft.Infrastructure.Data.Communicator.Providers.Twilio.UnitOfWork.Mapping
{
    public class TwilioAuthTokenConfiguration : ComplexTypeConfiguration<TwilioAuthToken>
    {
        public TwilioAuthTokenConfiguration()
        {
            Property(e => e.AccountSid).HasMaxLength(50);
            Property(e => e.AuthToken).HasMaxLength(50);
        }
    }
}
