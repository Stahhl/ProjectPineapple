using PineappleLib.Networking.Servers;
using System.Threading.Tasks;
using static PineappleLib.General.Data.Values;

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

        public async Task WaitForConnectionsServer(Server server, int expected, int ms = 1000)
        {
            int time = 0;
            int wait = 100;

            while (time < ms)
            {
                int current = 0;
                time += wait;

                for (int i = 0; i <= serverMaxPlayers; i++)
                {
                    var x = server.Clients[i];

                    if (x != null && x.Player != null)
                        current++;

                    if (current == expected)
                    {
                        return;
                    }
                }

                await Task.Delay(wait);
            }
        }
    }
}
