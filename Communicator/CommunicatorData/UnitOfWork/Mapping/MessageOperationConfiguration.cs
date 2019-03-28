using System.Data.Entity.ModelConfiguration;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.MessageOperationAgg;

namespace Swaksoft.Infrastructure.Data.Communicator.UnitOfWork.Mapping
{
    public class MessageOperationConfiguration : EntityTypeConfiguration<MessageOperation>
    {
        public MessageOperationConfiguration()
        {
            Property(e => e.Description).HasMaxLength(150);
            Property(e => e.Action).IsRequired().HasMaxLength(50);

            Map<SmsOperation>(m => m.Requires("MessageType").HasValue("SMS"));
            Map<VoiceOperation>(m => m.Requires("MessageType").HasValue("VOICE"));

            ToTable("MessageOperations", CommunicatorUnitOfWork.Schema);
        }
    }

    public class SmsOperationConfiguration : EntityTypeConfiguration<SmsOperation>
    {
        public SmsOperationConfiguration()
        {
            
        }
    }

    public class VoiceOperationConfiguration : EntityTypeConfiguration<VoiceOperation>
    {
        public VoiceOperationConfiguration()
        {
            Property(e => e.FinishOnKey).HasMaxLength(5);
            HasMany(e => e.SourceVoiceOptions).WithOptional(vo => vo.NextVoiceOperation).HasForeignKey(vo => vo.NextVoiceOperationId);
            HasOptional(e => e.SelectOptionMessage).WithMany().HasForeignKey(e => e.SelectOptionMessageId);
            HasOptional(e => e.InvalidOptionMessage).WithMany().HasForeignKey(e => e.InvalidOptionMessageId);
            HasRequired(e => e.FromPhoneNumber).WithMany().HasForeignKey(e => e.FromPhoneNumberId);
            //ToTable("VoiceOperations", CommunicatorUnitOfWork.Schema);
        }
    }
}
