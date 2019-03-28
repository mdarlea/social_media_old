using System;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamingEventAgg;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Events.Streaming
{
    public class DisplayActivityOnMap : StreamingEvent
    {
        public DisplayActivityOnMap(
            long statusId,
            long sentByUserId,
            string sentByUserName,
            Location location,
            DateTime dateSent,
            string text) 
            : base(StreamingEventType.DisplayActivityOnMap,statusId,sentByUserId,sentByUserName,dateSent)
        {
            Location = location;
            Text = text;
        }
    }
}
