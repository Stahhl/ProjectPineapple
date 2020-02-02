using PineappleLib.Networking.Servers;
using System.Threading.Tasks;

namespace PineappleLib.Logging
{
    public class AsyncLogger
    {
        /// <summary>
        /// Waits for ms and throws and exception if one was thrown elsewhere in that timespan.
        /// </summary>
        public async Task WaitForAsyncExceptions(int ms = 1000)
        {
            var ex = PineappleLogger.ex;

            await Task.Delay(ms);

            if (ex != PineappleLogger.ex)
                throw PineappleLogger.ex;
        }
        public async Task WaitForConnections(Server server, int expected)
        {
            int tries = 0;

            while (tries < 10)
            {
                int current = 0;
                tries++;

                for (int i = 1; i <= server.MaxPlayers; i++)
                {
                    var x = server.Clients[i];
                    if (x != null && x.Player != null)
                        current++;

                    if (current == expected)
                    {
                        return;
                    }
                }

                await Task.Delay(100);
            }
        }
    }
}
