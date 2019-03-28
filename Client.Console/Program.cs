namespace Client.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var processor = new MessageProcessor();
            var result = processor.Start();
            System.Console.WriteLine("Listener started");

            System.Console.WriteLine("Enter stop to stop the server");

            var msg = System.Console.ReadLine();
            if (msg == "stop")
            {
                processor.StopListener();
                System.Console.ReadLine();
            }
        }
    }
}
