using System.Data.Entity.ModelConfiguration;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicationLogAgg;

namespace Swaksoft.Infrastructure.Data.Communicator.UnitOfWork.Mapping
{
    public class CommunicationLogConfiguration : EntityTypeConfiguration<CommunicationLog>
    {
        public CommunicationLogConfiguration()
        {
            Property(c => c.CallId).HasMaxLength(100);
            Property(c => c.LogDate).IsRequired();

            HasRequired(c => c.MessageOperation)
                .WithMany()
                .HasForeignKey(d => d.MessageOperationId);

            HasRequired(c => c.FromPhoneNumber)
                          .WithMany(vpn => vpn.CommunicationLogs)
                          .HasForeignKey(c => c.FromPhoneNumberId);

            //HasRequired(c => c.ToPhoneNumber);

            Map<VoiceCallLog>(m => m.Requires("MessageType").HasValue("VOICE"));
            Map<SmsMessageLog>(m => m.Requires("MessageType").HasValue("SMS"));

            ToTable("CommunicationLogs", CommunicatorUnitOfWork.Schema);
        }
    }
}
