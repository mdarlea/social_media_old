using System;
using System.Data.Entity.ModelConfiguration;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg;

namespace Swaksoft.Infrastructure.Data.SocialMedia.UnitOfWork.Mapping
{
    public class BlockedUserConfiguration : EntityTypeConfiguration<BlockedUser>
    {
        public BlockedUserConfiguration()
        {
            HasRequired(e => e.UserProfile)
                .WithMany(p => p.BlockedUsers)
                .HasForeignKey(e => e.UserProfileId);

            Property(e => e.UserName).IsRequired().HasMaxLength(50);
        }
    }
}
