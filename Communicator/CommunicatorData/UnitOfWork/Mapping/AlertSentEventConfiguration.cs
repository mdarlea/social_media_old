using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicationLogAgg;

namespace Swaksoft.Infrastructure.Data.Communicator.UnitOfWork.Mapping
{
    public partial class AlertSentEventConfiguration : EntityTypeConfiguration<AlertSentEvent>
    {
        public AlertSentEventConfiguration()
        {
            HasKey(e => e.Id);
            Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            HasRequired(e => e.CommunicationLog)
                .WithRequiredDependent(dm => dm.AlertSentEvent);
            
            Map<VerificationCodeSent>(m => m.Requires("EventType").HasValue("VerificationCodeSent"));

            ToTable("AlertSentEvents", CommunicatorUnitOfWork.Schema);
            ConfigureMapping();
        }

        partial void ConfigureMapping();
    }

    public partial class VerificationCodeSentConfiguration : EntityTypeConfiguration<VerificationCodeSent>
    {
        public VerificationCodeSentConfiguration()
        {
            Property(c => c.MemberNbr).IsRequired();
        }
    }
}
