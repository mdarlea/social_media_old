using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace Twitter.Server
{
    class Program
    {
        const string ServerUri = "http://localhost:20035";

        private IDisposable SignalR { get; set; }

        static void Main(string[] args)
        {
            //Console.ReadLine();
            Console.WriteLine("Starting server...");
            Task.Run(() => StartServer());
            Console.ReadLine();
        }

        /// <summary>
        /// Starts the server and checks for error thrown when another server is already 
        /// running. This method is called asynchronously from Button_Start.
        /// </summary>
        private static void StartServer()
        {
            try
            {
                WebApp.Start(ServerUri);
            }
            catch (TargetInvocationException)
            {
                WriteToConsole("Server failed to start. A server is already running on " + ServerUri);
                return;
            }
            WriteToConsole("Server started at " + ServerUri);
        }

        private static void WriteToConsole(string message)
        {
            Console.WriteLine(message);
        }
    }
}
