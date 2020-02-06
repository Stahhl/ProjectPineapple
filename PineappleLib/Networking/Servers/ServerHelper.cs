using PineappleLib.Networking.Clients;
using PineappleLib.Networking.Lobbys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PineappleLib.General.Values;
using PineappleLib.Logging;
using PineappleLib.Enums;

namespace PineappleLib.Networking.Servers
{
    public class ServerHelper
    {
        public ServerHelper(Server server)
        {
            this.server = server;
            packetSeries = new Dictionary<string, byte[][]>();
        }

        private readonly Server server;
        private readonly Dictionary<string, byte[][]> packetSeries;


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

            if (server.Lobbys.All(x => x.Value == null))
                return result;

            Client client = server.Clients[clientId];
            //Server browser is low prio just enter password in a textfield to join initially 
            //could exist multiple lobbys with the same password in prod... 
            Lobby lobby = lobbyId != -1 ? server.Lobbys[lobbyId] : server.Lobbys.Select(x => x.Value).Where(x => x.Password == password).FirstOrDefault();

            if (lobby.Password == password)
            {
                lobby.Clients.Add(client);
                result = true;
            }

            return result;
        }
        public bool CombatCalc(int clientId, string packetId, int length, int order, byte[] data)
        {
            var packet = packetSeries[packetId];

            return false;
        }
    }
}
