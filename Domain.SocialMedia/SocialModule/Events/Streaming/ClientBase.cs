using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swaksoft.Domain.SocialMedia.SocialModule.Contracts;
using Swaksoft.Domain.SocialMedia.SocialModule.Contracts.Streaming;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Events.Streaming
{
    public abstract class ClientBase : IClient
    {
        private ConcurrentBag<string> tracks;

        private static readonly object thisObject = new object();

        protected ClientBase(ClientSettings clientSettings, int userProfileId, string userName)
            : this(clientSettings, userProfileId, userName, new List<string>())
        {
        }

        protected ClientBase(ClientSettings clientSettings, int userProfileId, string userName, List<string> tracks)
        {
            if (clientSettings == null) throw new ArgumentNullException("clientSettings");
            if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentNullException("userName");
            if (tracks == null) throw new ArgumentNullException("tracks");
            
            this.tracks = new ConcurrentBag<string>(tracks);
            Settings = clientSettings;
            UserProfileId = userProfileId;
            UserName = userName;
            ClientState = ClientState.NotStarted;
        }

        public ClientSettings Settings { get; private set; }
        public int UserProfileId { get; private set; }
        public string UserName { get; private set; }

        public ClientState ClientState { get; protected set; }

        public List<string> GetTracks()
        {
            return tracks.ToList();
        }

        public event EventHandler<StreamingArgs> MessageReceived;

        public async Task Start()
        {
            Task result=null;

            lock (thisObject)
            {
                if (ClientState != ClientState.Started)
                {
                    result = StartStream();
                    ClientState = ClientState.Started;
                }
            }

            if (result == null) return;
            await result;
        }

        public void Stop()
        {
            lock (thisObject)
            {
                StopCurrentStream();
            }
        }

        private void StopCurrentStream()
        {
            StopStream();
            ClientState = ClientState.Stopped;
        }

        protected virtual void OnMessageReceived(StreamingArgs e)
        {
            var handler = MessageReceived;
            if (handler != null) handler(this, e);
        }


        public abstract Task StartStream();

        public abstract void StopStream();

        public async Task Restart(List<string> queries)
        {
            lock (thisObject)
            {
                StopCurrentStream();

                tracks = new ConcurrentBag<string>(queries);
            }
            
            await Start();
        }
    }
}
