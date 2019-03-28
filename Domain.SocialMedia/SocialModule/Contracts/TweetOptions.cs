using System;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Contracts
{
    public class TweetOptions
    {
        public string Query { get; set; }
        public string Message { get; set; }
        public int? StreamedTweetId { get; set; }
        public long? InReplyToStatusId { get; set; }
    }
}
