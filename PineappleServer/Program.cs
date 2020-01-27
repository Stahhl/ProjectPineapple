using System;
using PineappleLib.Networking;
using PineappleLib.Logging;

namespace Server
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hello World!");

                new PineappleLib.Networking.Server().Start(5, 55555);

                var client = new ClientLocal();

                client.Init();
            }
            catch (Exception e)
            {
                PineappleLogger.HandleException(e, false);
            }

            Console.ReadKey();
        }
    }
}
