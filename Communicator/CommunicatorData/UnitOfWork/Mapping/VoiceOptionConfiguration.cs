using System.Data.Entity.ModelConfiguration;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.MessageOperationAgg;

namespace Swaksoft.Infrastructure.Data.Communicator.UnitOfWork.Mapping
{
    public class VoiceOptionConfiguration : EntityTypeConfiguration<VoiceOption>
    {
        public VoiceOptionConfiguration()
        {
            Property(e => e.Key).HasMaxLength(5);
            Property(e => e.Description).HasMaxLength(100);
            
            HasMany(vo => vo.VoiceOperations)
               .WithMany(m => m.VoiceOptions)
               .Map(map =>
               {
                   map.MapLeftKey("VoiceOptionId");
                   map.MapRightKey("VoiceOperationId");
                   map.ToTable("VoiceOperationToVoiceOptions", CommunicatorUnitOfWork.Schema);
               });

            ToTable("VoiceOptions", CommunicatorUnitOfWork.Schema);
        }
    }
}
