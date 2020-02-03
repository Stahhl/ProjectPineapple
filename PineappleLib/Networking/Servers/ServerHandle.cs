using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PineappleLib.General.Exceptions;
using PineappleLib.Logging;
using PineappleLib.Models.Players;
using PineappleLib.Serialization;

namespace PineappleLib.Networking.Servers
{
    public class ServerHandle
    {
        public ServerHandle(Server server)
        {
            this.server = server;
            this.serializer = server.Serializer;
        }

        private Server server;
        private PineappleSerializer serializer;
        private const string type = "[Server]";

        public void WelcomeReceived(int clientId, Packet packet)
        {
            var client = server.Clients[clientId];
            var clientPlayer = (Player)serializer.Deserialize(packet.ReadBytes(packet.UnreadLength()));

            client.IsConnected = true;
            client.AssignPlayerToClient(clientPlayer);
        }
        public void ClientIdCheck(int expected, int actual)
        {
            if (expected != actual)
                PineappleLogger.HandleException(new ClientIdException(), true);
        }

    }
}
