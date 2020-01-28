using System;
using System.Net.Sockets;
using PineappleLib.Networking.Protocol;
using PineappleLib.Logging;
using PineappleLib.Enums;
using static PineappleLib.General.Data.Values;

namespace PineappleLib.Networking.Clients
{
    public class Client
    {
        public Client()
        {
            IsServer = false;

            Ip = stdIp;
            Port = stdPort;

            Tcp = new Tcp(this);
            //udp = new UDP();

            Tcp.Connect();
        }
        public Client(int id)
        {
            IsServer = true;

            Id = id;
            Tcp = new Tcp(id);
            //udp = new UDP(id);
        }

        private readonly bool IsServer;

        public bool IsConnected { get; set; }

        public Tcp Tcp { get; private set; }
        public int Id { get; private set; }

        public string Ip { get; private set; }
        public int Port { get; private set; }

        public void Disconnect()
        {
            if (IsServer == false)
            {
                Tcp.socket.Close();
                //udp.socket.Close();

                PineappleLogger.PineappleLog(LogType.DEBUG, "Disconnected from server.");
            }
        }

    }//class
}//namespace
