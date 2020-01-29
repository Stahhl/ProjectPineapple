using System;
using System.Net.Sockets;
using PineappleLib.Networking.Protocol;
using PineappleLib.Logging;
using PineappleLib.Enums;
using static PineappleLib.General.Data.Values;
using PineappleLib.Networking.Servers;

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

        protected readonly bool IsServer;

        public bool IsConnected { get; set; }

        public _Tcp Tcp { get; protected set; }
        public int Id { get; protected set; }

        public string Ip { get; protected set; }
        public int Port { get; protected set; }

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
