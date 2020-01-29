using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PineappleLib.Enums
{
    public enum PacketType
    {
        Welcome, //Server welcomes client assigns id
        WelcomeReceived, //Client responds it let into game
    }
}
