using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.MessageOperationAgg;
using System.Data.Entity.ModelConfiguration;

namespace Swaksoft.Infrastructure.Data.Communicator.UnitOfWork.Mapping
{
    public class MessageConfiguration : EntityTypeConfiguration<Message>
    {
        public MessageConfiguration()
        {
            HasMany(vo => vo.MessageOperations)
                .WithMany(m => m.Messages)
                .Map(map =>
                {
                    map.MapLeftKey("MessageId");
                    map.MapRightKey("MessageOperationId");
                    map.ToTable("MessageOperationToMessages", CommunicatorUnitOfWork.Schema);
                });

            ToTable("Messages", CommunicatorUnitOfWork.Schema);
        }
    }
}
