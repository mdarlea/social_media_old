using System;
using Swaksoft.Core.External;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Events
{
    public class TweetStreamed : RealTimeEvent
    {
        public TweetStreamed(
            long statusId, 
            long sentByUserId, 
            string sentByUserName, 
            long? sentToUserId, 
            string sentToUserName, 
            DateTime dateSent) 
            : base(statusId, sentByUserId, sentByUserName, sentToUserId, sentToUserName, dateSent)
        {
        }

        public ExternalProviderCredentials ClientCredentials { get; set; }
        public string Query { get; set; }

        public string Name { get; set; }
        public string ProfileImageUrl { get; set; }
        public string ProfileImageUrlHttps { get; set; }
    }
}
