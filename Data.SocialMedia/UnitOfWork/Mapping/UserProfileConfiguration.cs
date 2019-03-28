using System;
using System.Data.Entity.ModelConfiguration;
using Swaksoft.Core;
using Swaksoft.Core.External;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg;

namespace Swaksoft.Infrastructure.Data.SocialMedia.UnitOfWork.Mapping
{
    public class UserProfileConfiguration : EntityTypeConfiguration<UserProfile>
    {
        public UserProfileConfiguration()
        {
            Property(e => e.ExternalUserId).HasMaxLength(150);
            Property(e => e.UserName).HasMaxLength(50).IsRequired();

            HasOptional(e => e.User)
                .WithMany(e => e.UserProfiles)
                .HasForeignKey(e => e.UserId);

            Map<FoursquareUserProfile>(m => m.Requires("Provider").HasValue(ExternalProvider.Foursquare.ToString()));
        }
    }

    public class TwitterUserProfileConfiguration : EntityTypeConfiguration<TwitterUserProfile>
    {
        public TwitterUserProfileConfiguration()
        {
            ToTable("TwitterUserProfiles");

            Property(e => e.TwitterUserId).IsRequired();
            Property(e => e.Name).HasMaxLength(100);
        }
    }
}
