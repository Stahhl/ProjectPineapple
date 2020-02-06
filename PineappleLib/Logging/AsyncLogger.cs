using PineappleLib.Networking.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static PineappleLib.General.Values;

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
            var ex = PineappleLogger.ex;

            int time = 0;
            int wait = 100;
            int current = 0;

            while (time < ms)
            {
                if (ex != PineappleLogger.ex)
                    throw PineappleLogger.ex;

                current = 0;
                time += wait;

                for (int i = 0; i <= serverMaxPlayers; i++)
                {
                    var client = server.Clients[i];

                    if (client != null && client.Player != null)
                        current++;

                    if (current == expected)
                        return;
                }

                await Task.Delay(wait);
            }

            if (current != expected)
                throw new Exception("AsyncLogger - WaitForConnectionsServer()");
        }
        public async Task WaitForLobbys(Server server, int expected, int ms = 1000)
        {
            var ex = PineappleLogger.ex;

            int time = 0;
            int wait = 100;
            int current = 0;

            while (time < ms)
            {
                if (ex != PineappleLogger.ex)
                    throw PineappleLogger.ex;

                current = 0;
                time += wait;

                for (int i = 0; i <= serverMaxLobbys; i++)
                {
                    var lobby = server.Lobbys[i];

                    if (lobby != null)
                        current++;

                    if (current == expected)
                        return;
                }

                await Task.Delay(wait);
            }

            if (current != expected)
                throw new Exception("AsyncLogger - WaitForLobbys()");
        }
        public async Task WaitForClientsInLobbys(Server server, Dictionary<int, int> clientsPerLobby, int ms = 1000)
        {
            var ex = PineappleLogger.ex;

            int time = 0;
            int wait = 100;

            bool[] result = new bool[clientsPerLobby.Count];

            while (time < ms)
            {
                if (ex != PineappleLogger.ex)
                    throw PineappleLogger.ex;

                int current = 0;
                time += wait;

                foreach (var item in clientsPerLobby)
                {
                    var key = item.Key;
                    var value = item.Value;
                    var lobby = server.Lobbys[key];

                    if (lobby != null && lobby.Clients.Count == value)
                        result[current] = true;

                    current++;
                }

                if (result.All(x => x == true))
                    break;

                await Task.Delay(wait);
            }

            if (result.Any(x => x == false))
                throw new Exception("AsyncLogger - WaitForClientsInLobbys()");
        }
        public async Task WaitForClientValues(Server server, int ms = 1000)
        {
            var ex = PineappleLogger.ex;

            int time = 0;
            int wait = 100;

            bool result = false;

            while(time < ms)
            {
                if (ex != PineappleLogger.ex)
                    throw PineappleLogger.ex;

                time += wait;

                result = checkClientValues(server);

                if (result == true)
                    break;

                await Task.Delay(wait);
            }

            if (result == false)
                throw new Exception("AsyncLogger - CheckClientValues()");
        }
        private bool checkClientValues(Server server)
        {
            foreach (var client in server.Clients.Values)
            {
                if (client != null && client.Player == null)
                    return false;
            }

            return true;
        }
    }
}
