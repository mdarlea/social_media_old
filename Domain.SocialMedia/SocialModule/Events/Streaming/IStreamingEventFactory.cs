using System;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamedTweetAgg;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Events.Streaming
{
    public interface IStreamingEventFactory
    {
        StreamingEvent Create(StreamedTweet streamedTweet, Location location, DateTime dateSent);
    }

    public class StreamingEventFactory<T> : IStreamingEventFactory
        where T:StreamingEvent
    {
        private readonly Func<StreamedTweet, Location, DateTime, T> _factory;

        public StreamingEventFactory(Func<StreamedTweet,Location, DateTime,T> factory)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            _factory = factory;
        }

        public StreamingEvent Create(StreamedTweet streamedTweet, Location location, DateTime dateSent)
        {
            return _factory(streamedTweet, location, dateSent);
        }   
    }
}
