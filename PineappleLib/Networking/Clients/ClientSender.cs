using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PineappleLib.Networking.Clients
{
    public class ClientSender
    {
        public ClientSender(Client client)
        {
            Client = client;
        }

        public Client Client { get; private set; }
    }
}
