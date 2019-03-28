using System;
using System.Data.Entity.ModelConfiguration;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamedTweetAgg;

namespace Swaksoft.Infrastructure.Data.SocialMedia.UnitOfWork.Mapping
{
    public class StreamedTweetConfiguration : EntityTypeConfiguration<StreamedTweet>
    {
        public StreamedTweetConfiguration()
        {
            Property(e => e.UserId).IsRequired();
            Property(e => e.UserName).HasMaxLength(50).IsRequired();
            Property(e => e.Text).IsRequired().HasMaxLength(250);
            Property(e => e.Query).HasMaxLength(200);

            HasMany(st => st.UserProfiles)
                .WithMany(p => p.StreamedTweets)
                .Map(map =>
                {
                    map.MapLeftKey("StreamedTweetId");
                    map.MapRightKey("UserProfileId");
                    map.ToTable("StreamedTweetToUserProfiles");
                });
        }
    }
}
