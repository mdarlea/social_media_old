using System;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamedTweetAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.SentMessageAgg
{
    public class SentTweetFactory : SentMessageFactory<SentTweet>
    {
        private readonly long _statusId;
        private readonly long _sentByUserId;
        private readonly string _sentByUserName;
        private readonly long? _inReplyToStatusId;
        private readonly long? _sentToUserId;
        private readonly string _sentToUserName;
        private readonly int? _streamedTweetId;
        
        public SentTweetFactory(UserProfile userProfile, 
            DateTime dateSent, string messageSent,
            long statusId,
            long sentByUserId,
            string sentByUserName,
            long? inReplyToStatusId,
            long? sentToUserId,
            string sentToUserName,
            int? streamedTweetId) 
            : base(userProfile, dateSent, messageSent)
        {
            if (statusId < 1) throw new ArgumentNullException("statusId");
            if (sentByUserId < 1) throw new ArgumentNullException();
            if (string.IsNullOrWhiteSpace(sentByUserName)) throw new ArgumentNullException("sentByUserName");
     
            _statusId = statusId;
            _sentByUserId = sentByUserId;
            _sentByUserName = sentByUserName;
            _inReplyToStatusId = inReplyToStatusId;
            _sentToUserId = sentToUserId;
            _sentToUserName = sentToUserName;
            _streamedTweetId = streamedTweetId;
        }

        public override SentTweet CreateSentMessage()
        {
            var sentMessage = base.CreateSentMessage();
           
            sentMessage.InReplyToStatusId = _inReplyToStatusId;
            sentMessage.SentByUserId = _sentByUserId;
            sentMessage.SentByUserName = _sentByUserName;
            sentMessage.SentToUserId = _sentToUserId;
            sentMessage.SentToUserName = _sentToUserName;
            sentMessage.StatusId = _statusId;
            sentMessage.StreamedTweetId = _streamedTweetId;

            return sentMessage;
        }
    }
}
