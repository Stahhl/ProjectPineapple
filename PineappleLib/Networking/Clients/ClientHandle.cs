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
            try
            {
                string msg = packet.ReadString();
                int id = packet.ReadInt();

                PineappleLogger.Log(LogType.INFO, $"{type} [{client.Id}] I received welcome from server my new ID is: {id}");

                client.IsConnected = true;
                client.Id = id;

                client.ClientSender.WelcomeReceived();
            }
            catch (Exception ex)
            {
                PineappleLogger.HandleException(ex, true, "ClientHandle - WelcomeFromServer()");
            }
        }
    }
}
