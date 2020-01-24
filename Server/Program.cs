using System;
using Domain.Networking;
using Domain.General.Logging;

namespace Server
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hello World!");

                new Domain.Networking.Server().Start(5, 55555);

                var client = new ClientLocal();

                client.Init();
            }
            catch (Exception e)
            {
                Logger.HandleException(e, false);
            }

            Console.ReadKey();
        }
    }
}
