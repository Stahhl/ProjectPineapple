using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PineappleLib.Networking
{
    public static class NetworkHandler
    {
        public static void Welcome(Packet packet)
        {
            string msg = packet.ReadString();
            int id = packet.ReadInt();

        }
    }
}
