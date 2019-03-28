using System;

namespace Swaksoft.Application.SocialMedia.Dto
{
    public class TweetSent : MessageSent
    {
        public long StatusId { get; set; }

        public long SentByUserId { get; set; }
        
        public string SentByUserName { get; set; }

        public long? InReplyToStatusId { get; set; }

        public long? SentToUserId { get; set; }

        public string SentToUserName { get; set; }

        public int? StreamedTweetId { get; set; }
    }
}
