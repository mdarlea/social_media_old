using System;
using System.Data.Entity.ModelConfiguration;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.MessageOperationAgg;

namespace Swaksoft.Infrastructure.Data.SocialMedia.UnitOfWork.Mapping
{
    public class MessageOperationConfiguration
        : EntityTypeConfiguration<MessageOperation>
    {
        public MessageOperationConfiguration()
        {
            Map<StreamFilterMessageOperation>(m => m.Requires("OperationType").HasValue("FILTER"));
            Map<DirectMessageOperation>(m => m.Requires("OperationType").HasValue("DM"));

            HasRequired(e => e.Message)
                .WithMany(e => e.MessageOperations)
                .HasForeignKey(e => e.MessageId);
        }

        public class StreamFilterMessageOperationConfiguration : EntityTypeConfiguration<StreamFilterMessageOperation>
        {
            public StreamFilterMessageOperationConfiguration()
            {
                HasMany(st => st.StreamFilters)
                    .WithMany(p => p.StreamFilterMessageOperations)
                        .Map(map =>
                        {
                            map.MapLeftKey("MessageOperationId");
                            map.MapRightKey("StreamFilterId");
                            map.ToTable("MessageOperationsToStreamFilters");
                        });
            }
        }
    }
}
