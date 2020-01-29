using System;
using System.Net.Sockets;
using PineappleLib.Networking.Protocol;
using PineappleLib.Logging;
using PineappleLib.Enums;
using static PineappleLib.General.Data.Values;
using PineappleLib.Networking.Servers;
using System.Collections.Generic;

namespace PineappleLib.Networking.Clients
{
    public class Client
    {
        public Client()
        {
            IsServer = false;

            Ip = stdIp;
            Port = stdPort;

            Tcp = new Client_Tcp(this);
            ClientSender = new ClientSender(this);
            ClientHandlers = new ClientHandlers(this);

            //udp = new UDP();

            Tcp.Connect();
        }

        public Client(Server server, int id)
        {
            IsServer = true;

            Id = id;
            Tcp = new Server_Tcp(server, id);
            //udp = new UDP(id);
        }

        private readonly bool IsServer;

        public int Id { get; set; }
        public bool IsConnected { get; set; }

        public string Ip { get; private set; }
        public int Port { get; private set; }

        public _Tcp Tcp { get; private set; }
        public ClientSender ClientSender { get; private set; }
        public ClientHandlers ClientHandlers { get; private set; }

        public void Disconnect()
        {
            if (IsServer == false)
            {
                Tcp.Socket.Close();
                //udp.socket.Close();

                PineappleLogger.PineappleLog(LogType.DEBUG, "Disconnected from server.");
            }
        }

    }//class
}//namespace
