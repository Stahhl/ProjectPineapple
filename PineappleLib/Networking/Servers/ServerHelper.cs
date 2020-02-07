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
using PineappleLib.Models.Units;
using PineappleLib.Models.Abilities;

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
                if (server.Lobbys[i] == null)
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
                client.LobbyId = lobby.Id;
                result = true;
            }

            return result;
        }
        public bool CombatCalc(int clientId, string packetId, int length, int order, byte[] data)
        {
            if (packetSeries.ContainsKey(packetId) == false)
            {
                newPacketSeries(packetId, length);
            }
            if (isPacketSeriesComplete(packetId, length, order, data) == false)
            {
                return false;
            }

            byte[][] series = packetSeries[packetId];

            Unit attacker = (Unit)server.Serializer.Deserialize(series[0]);
            Unit defender = (Unit)server.Serializer.Deserialize(series[1]);
            _Ability ability = (_Ability)server.Serializer.Deserialize(series[2]);

            server.Lobbys[server.Clients[clientId].LobbyId].GameController.CombatController.CombatCalcRedirect(attacker, defender, ability);

            return true;
        }
        private void newPacketSeries(string packetId, int length)
        {
            packetSeries.Add(packetId, new byte[length][]);
        }
        private bool isPacketSeriesComplete(string packetId, int length, int order, byte[] data)
        {
            packetSeries[packetId][order] = data;

            if (packetSeries[packetId].All(x => x != null))
                return true;

            return false;
        }
    }
}
