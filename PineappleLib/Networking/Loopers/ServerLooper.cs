using PineappleLib.Networking.Clients;
using PineappleLib.Networking.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PineappleLib.Networking.Loopers
{
    public class ServerLooper : _Looper
    {
        public ServerLooper(Server server)
        {
            base.type = $"[{this.GetType().Name}]";
            this.server = server;

            ThreadManager.Server = server;
        }

        private Server server;

        public override void Update()
        {
            if (server == null)
                return;

            ThreadManager.UpdateActions();
            ThreadManager.AddQueuedClients();
        }
    }
}
