using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PineappleLib.Networking.Servers
{
    public class ServerHandle
    {
        public ServerHandle(Server server)
        {
            this.server = server;
        }

        private Server server;

        public void WelcomeReceived(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string _username = _packet.ReadString();
        }

    }
}
