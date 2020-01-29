using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PineappleLib.Networking.Clients
{
    public class ClientHandle
    {
        public ClientHandle(Client client)
        {
            this.client = client;
        }

        private Client client;

        public void Welcome(Packet packet)
        {
            string msg = packet.ReadString();
            int id = packet.ReadInt();

            client.Id = id;
        }
    }
}
