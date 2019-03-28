using System;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Contracts.Streaming
{
    public class StreamingArgs : EventArgs
    {
        public string Track { get; private set; }

        public StreamingArgs(string track)
        {
            if (string.IsNullOrWhiteSpace(track)) throw new ArgumentNullException("track");
            Track = track;
        }
    }
}
