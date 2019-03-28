using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Contracts.Streaming
{
    public enum ClientState
    {
        NotStarted, Started, Stopped
    }
    
    public interface IClient
    {
        ClientSettings Settings { get; }

        int UserProfileId { get; }

        string UserName { get; }

        ClientState ClientState { get; }

        List<string> GetTracks();

        event EventHandler<StreamingArgs> MessageReceived;
        Task Start();
        void Stop();
    }
}
