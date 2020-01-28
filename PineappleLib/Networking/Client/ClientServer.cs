using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using PineappleLib.Logging;
using PineappleLib.Enums;
using PineappleLib.Networking.TCP;

namespace PineappleLib.Networking.Client
{
    public class ClientServer : _Client
    {
        public int id;
        //public Player player;
        //public UDP udp;

        public ClientServer(int _clientId)
        {
            id = _clientId;
            Tcp = new TcpServer(id);
            //udp = new UDP(id);
        }
    }
}
