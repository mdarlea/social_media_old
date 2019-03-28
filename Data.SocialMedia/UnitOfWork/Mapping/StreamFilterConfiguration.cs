using System;
using System.Data.Entity.ModelConfiguration;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamFilterAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserAgg;

namespace Swaksoft.Infrastructure.Data.SocialMedia.UnitOfWork.Mapping
{
    public class StreamFilterConfiguration : EntityTypeConfiguration<StreamFilter>
    {
        public StreamFilterConfiguration()
        {
            HasRequired(e => e.User)
             .WithMany(e => e.StreamFilters)
             .HasForeignKey(e => e.UserId);

            Property(e => e.Query).HasMaxLength(150).IsRequired();
        }
    }
}
