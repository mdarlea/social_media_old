using System;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamingEventAgg;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Events.Streaming
{
    public abstract class StreamingEvent : RealTimeEvent
    {
        private readonly StreamingEventType _code;

        protected StreamingEvent(
            StreamingEventType code,
            long statusId,
            long sentByUserId,
            string sentByUserName,
            DateTime dateSent)
            : base(statusId,sentByUserId,sentByUserName,null,null,dateSent)
        {
            _code = code;
        }

        public StreamingEventType Code
        {
            get { return _code; }
        }
    }
}
