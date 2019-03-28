using System;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamedTweetAgg
{
    public class StreamedTweetsFilter
    {
        public StreamedTweet StreamedTweet { get; set; }
        public int UserProfileId { get; set; }
    }
}
