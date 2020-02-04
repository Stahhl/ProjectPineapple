using PineappleLib.Networking.Lobbys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PineappleLib.General.Data.Values;

namespace PineappleLib.Networking.Servers
{
    public class ServerHelper
    {
        public ServerHelper(Server server)
        {
            this.server = server;
        }

        private readonly Server server;

        public bool CreateLobby(int clientId, string password, bool join)
        {
            bool result = false;
            int lobbyId = -1;

            for (int i = 0; i <= serverMaxLobbys; i++)
            {
                if(server.Lobbys[i] == null)
                {
                    server.Lobbys[i] = new Lobby(server, clientId, password);
                    result = true;
                    lobbyId = i;
                    break;
                }
            }

            if (result == true && join == true)
                return JoinLobby(clientId, lobbyId, password);

            return result;
        }
        public bool JoinLobby(int clientId, int lobbyId, string password)
        {
            bool result = false;

            var client = server.Clients[clientId];
            var lobby = server.Lobbys[lobbyId];

            if (lobby.Password == password)
            {
                lobby.Clients.Add(client);
                result = true;
            }
            
            return result;
        }
    }
}
