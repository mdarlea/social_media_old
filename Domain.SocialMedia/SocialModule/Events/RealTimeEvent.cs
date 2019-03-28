using System;
using Swaksoft.Domain.Seedwork.Events;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Events
{
    public abstract class RealTimeEvent : IDomainEvent
    {
        protected RealTimeEvent(
            long statusId,
            long sentByUserId,
            string sentByUserName,
            long? sentToUserId,
            string sentToUserName,
            DateTime dateSent)
        {
            if (statusId < 1) throw new ArgumentException("statusId");
            if (sentByUserId < 1) throw new ArgumentException("sentByUserId");
            if (string.IsNullOrWhiteSpace(sentByUserName)) throw new ArgumentNullException("sentByUserName");

            StatusId = statusId;
            SentByUserId = sentByUserId;
            SentByUserName = sentByUserName;
            SentToUserId = sentToUserId;
            SentToUserName = sentToUserName;
            DateSent = dateSent;
        }

        public int UserProfileId { get; set; }

        public long StatusId { get; private set; }

        public long SentByUserId { get; private set; }
        public string SentByUserName { get; private set; }

        public long? InReplyToStatusId { get; set; }

        public long? SentToUserId { get; private set; }
        public string SentToUserName { get; private set; }

        public DateTime DateSent { get; private set; }

        public string Text { get; set; }

        public Location Location { get; set; }
    }
}
