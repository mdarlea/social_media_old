using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

namespace Client.Console
{
    public class MessageProcessor
    {
        /// <summary>
        /// This name is simply added to sent messages to identify the user; this 
        /// sample does not include authentication.
        /// </summary>
        const string ServerUri = "http://www.swaksoft.com/signalr";

        private readonly object _lockObject = new object();
        private static CancellationTokenSource _cancellationSource;

        public HubConnection Connection { get; set; }
        public IHubProxy HubProxy { get; set; }
        
        public async Task Start()
        {
            lock (_lockObject)
            {
                if (_cancellationSource != null) return;
                _cancellationSource = new CancellationTokenSource();
            }

            await ConnectAsync();

            System.Console.WriteLine("Start server pinging");

            await Task.Factory.StartNew(
                   () => ProcessMessages(_cancellationSource.Token),
                   _cancellationSource.Token,
                   TaskCreationOptions.LongRunning,
                   TaskScheduler.Current);

            System.Console.WriteLine("Stopped server pinging");
        }

        public virtual void StopListener()
        {
            lock (_lockObject)
            {
                using (_cancellationSource)
                {
                    if (_cancellationSource == null) return;
                    _cancellationSource.Cancel();
                    _cancellationSource = null;
                }
            }
        }

        /// <summary>
        /// Processes the messages in an endless loop.
        /// </summary> 
        private void ProcessMessages(CancellationToken cancellationToken)
        {
            //starts the endless loop (long process)
            while (!cancellationToken.IsCancellationRequested)
            {
                System.Console.WriteLine("Pinging Swaksoft ...");
                var result = HubProxy.Invoke("Send", "Neo", "Pingged Swaksoft");
                Thread.Sleep(TimeSpan.FromMinutes(15));
            }
            System.Console.WriteLine("Closed processor ...");
        }

        /// <summary>
        /// Creates and connects the hub connection and hub proxy. This method
        /// is called asynchronously from SignInButton_Click.
        /// </summary>
        private async Task ConnectAsync()
        {
            if (Connection != null)
            {
                return;
            }

            Connection = new HubConnection(ServerUri);
            HubProxy = Connection.CreateHubProxy("MessageHub");

            //Handle incoming event from server: use Invoke to write to console from SignalR's thread
            HubProxy.On<string, string>("AddMessage", (name, message) =>
            {
                System.Console.WriteLine("{0}: {1} ({2})", name, message, DateTime.Now);
            });

            System.Console.WriteLine("Connecting...");

            try
            {
                await Connection.Start();
            }
            catch (HttpRequestException)
            {
                System.Console.WriteLine("Unable to connect to server: Start server before connecting clients.");
                //No connection: Don't enable Send button or show chat UI
                return;
            }
            System.Console.WriteLine("Connected to server at " + ServerUri);
        }
    }
}
