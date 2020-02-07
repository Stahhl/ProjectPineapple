using PineappleLib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PineappleLib.General
{
    public static class PacketIdentity
    {
        private static readonly Random rnd = new Random();

        public static string GetPacketId(PacketType packetType)
        {
            return (int)packetType + "_" + rnd.Next(1, 1000) + "_" + DateTime.Now.ToString("HH:mm:ss.fff");
        }
    }
}
