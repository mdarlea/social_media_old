using System;
using System.Data.Entity.ModelConfiguration;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamingEventAgg;

namespace Swaksoft.Infrastructure.Data.SocialMedia.UnitOfWork.Mapping
{
    public class StreamingEventConfiguration : EntityTypeConfiguration<StreamingEvent>
    {
        public StreamingEventConfiguration()
        {
            Property(e => e.Code).IsRequired();
            Property(e => e.Description).HasMaxLength(250);

            HasMany(st => st.StreamFilters)
              .WithMany(p => p.StreamingEvents)
                .Map(map =>
                {
                    map.MapLeftKey("StreamingEventId");
                    map.MapRightKey("StreamFilterId");
                    map.ToTable("StreamingEventsToStreamFilters");
                });
        }
    }
}
