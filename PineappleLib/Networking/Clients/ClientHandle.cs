using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PineappleLib.Enums;
using PineappleLib.Logging;

namespace PineappleLib.Networking.Clients
{
    public class ClientHandle
    {
        public ClientHandle(Client client)
        {
            this.client = client;
        }

        private Client client;
        private const string type = "[Client]";

        public void WelcomeFromServer(Packet packet)
        {
            string msg = packet.ReadString();
            int id = packet.ReadInt();

            client.IsConnected = true;
            client.Id = id;

            PineappleLogger.PineappleLog(LogType.INFO, $"{type} I received welcome from server my new ID is: {client.Id}, Message: {msg}");
            client.ClientSender.WelcomeReceived();
        }
    }
}
