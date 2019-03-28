using System.ComponentModel.DataAnnotations;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamedTweetAgg;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.SentMessageAgg
{
    public class SentTweet : SentMessage
    {
        [Required]
        public long StatusId { get; set; }

        [Required]
        public long SentByUserId { get; set; }

        [Required]
        public string SentByUserName { get; set; }

        public long? InReplyToStatusId { get; set; }

        public long? SentToUserId { get; set; }

        public string SentToUserName { get; set; }

        public int? StreamedTweetId { get; set; }
        public virtual StreamedTweet StreamedTweet { get; set; }
    }
}
