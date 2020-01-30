using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PineappleLib.Enums;

namespace PineappleLib.Networking.Clients
{
    public class ClientHandlers
    {
        public ClientHandlers(Client client)
        {
            clientHandle = new ClientHandle(client);

            Handlers = new Dictionary<int, PacketHandler>()
            {
                {(int)PacketType.Welcome, clientHandle.WelcomeFromServer }
            };
        }

        public delegate void PacketHandler(Packet _packet);
        public Dictionary<int, PacketHandler> Handlers;

        private ClientHandle clientHandle;
    }
}
    