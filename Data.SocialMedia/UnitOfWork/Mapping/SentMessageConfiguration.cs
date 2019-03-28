using System;
using System.Data.Entity.ModelConfiguration;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.SentMessageAgg;

namespace Swaksoft.Infrastructure.Data.SocialMedia.UnitOfWork.Mapping
{
    public class SentMessageConfiguration : EntityTypeConfiguration<SentMessage>
    {
        public SentMessageConfiguration()
        {
            HasRequired(e => e.UserProfile)
                .WithMany(e => e.SentMessages)
                .HasForeignKey(e => e.UserProfileId);

            Property(e => e.DateSent).IsRequired();

            Property(e => e.MessageSent).IsRequired().HasMaxLength(250);

            Map<SentTweet>(m => m.Requires("Type").HasValue("Twitter"));
        }
    }

    public class SentTweetConfiguration : EntityTypeConfiguration<SentTweet>
    {
        public SentTweetConfiguration()
        {
            Property(e => e.StatusId).IsRequired();
            Property(e => e.SentByUserId).IsRequired();
            Property(e => e.SentByUserName).IsRequired().HasMaxLength(50);
            Property(e => e.SentToUserName).HasMaxLength(50);

            HasOptional(e => e.StreamedTweet)
                .WithMany(e => e.SentTweets)
                .HasForeignKey(e => e.StreamedTweetId);
        }
    }
}
