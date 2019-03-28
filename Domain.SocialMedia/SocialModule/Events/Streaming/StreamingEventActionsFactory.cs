using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamingEventAgg;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Events.Streaming
{
    public class StreamingEventActionsFactory
        : ActionsFactory<IStreamingEventFactory>
    {
        public StreamingEventActionsFactory()
        {
            Register(
                StreamingEventType.DisplayActivityOnMap.ToString(),
                () 
                    => new StreamingEventFactory<DisplayActivityOnMap>(
                        (streamedTweet, location,dateSent) => 
                            new DisplayActivityOnMap(
                                streamedTweet.StatusId,
                                streamedTweet.UserId,
                                streamedTweet.UserName,
                                location,
                                dateSent,
                                streamedTweet.Text)));
        }
    }
}
