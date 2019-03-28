using System;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Events
{
    public class MessageSent : RealTimeEvent
    {
        public MessageSent(long statusId, long sentByUserId, string sentByUserName, long? sentToUserId, string sentToUserName, DateTime dateSent) 
            : base(statusId, sentByUserId, sentByUserName, sentToUserId, sentToUserName, dateSent)
        {
        }

        public int? StreamedTweetId { get; set; }
    }
}
