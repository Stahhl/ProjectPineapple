using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PineappleLib.Logging;
using PineappleLib.Models.Players;
using PineappleLib.Networking.Clients;
using PineappleLib.Serialization;

namespace PineappleLib.Networking.Servers
{
    public class ServerHandle
    {
        public ServerHandle(Server server)
        {
            this.server = server;
            this.serializer = server.Serializer;
            this.serverHelper = server.ServerHelper;
        }

        private Server server;
        private ServerHelper serverHelper;
        private PineappleSerializer serializer;
        private const string type = "[Server]";

        public void WelcomeReceived(int clientId, Packet packet)
        {
            try
            {
                Client client = server.Clients[clientId];
                Player clientPlayer = (Player)serializer.Deserialize(packet.ReadBytes(packet.UnreadLength()));

                client.IsConnected = true;
                client.AssignPlayerToClient(clientPlayer);
            }
            catch (Exception ex)
            {
                PineappleLogger.HandleException(ex, true, "ServerHandle - WelcomeReceived()");
            }
        }
        public void CreateLobby(int clientId, Packet packet)
        {
            try
            {
                string password = packet.ReadString();
                bool join = packet.ReadBool();

                bool result1 = serverHelper.CreateLobby(clientId, password, join);
            }
            catch (Exception ex)
            {
                PineappleLogger.HandleException(ex, true, "ServerHandle - CreateLobby()");
            }
        }
        public void JoinLobby(int clientId, Packet packet)
        {
            try
            {
                string password = packet.ReadString();
                int lobbyId = packet.ReadInt();

                bool result = serverHelper.JoinLobby(clientId, lobbyId, password);
            }
            catch (Exception ex)
            {
                PineappleLogger.HandleException(ex, true, "ServerHandle - JoinLobby()");
            }
        }
        public void ClientIdCheck(int expected, int actual)
        {
            throw new NotImplementedException();
        }

    }
}
