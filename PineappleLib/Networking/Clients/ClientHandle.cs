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

        public void WelcomeFromServer(Packet packet)
        {
            string msg = packet.ReadString();
            int id = packet.ReadInt();

            client.Id = id;

            PineappleLogger.PineappleLog(LogType.INFO, $"I received welcome from server my new ID is: {client.Id}");
            client.ClientSender.WelcomeReceived();
        }
    }
}
