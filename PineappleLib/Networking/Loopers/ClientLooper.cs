using PineappleLib.Networking.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PineappleLib.Networking.Loopers
{
    public class ClientLooper : _Looper
    {
        public ClientLooper(Client client)
        {
            base.type = $"[{this.GetType().Name}]";
            this.client = client;
        }

        private Client client;

        public override void Update()
        {
            ThreadManager.UpdateActions();
        }
    }
}
