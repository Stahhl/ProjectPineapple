using System;
using System.Net.Sockets;
using PineappleLib.Networking.Protocol;
using PineappleLib.Logging;
using PineappleLib.Enums;
using static PineappleLib.General.Values;
using PineappleLib.Networking.Servers;
using System.Collections.Generic;
using PineappleLib.Models.Players;
using PineappleLib.Controllers;
using PineappleLib.Networking.Loopers;
using PineappleLib.Serialization;

namespace PineappleLib.Networking.Clients
{
    public class Client
    {
        public Client(GameController gameController)
        {
            //throw new NotImplementedException();
            IsServer = false;
            GameController = gameController;

            Ip = stdIp;
            Port = stdPort;

            AssignPlayerToClient(gameController.Players[0]);

            Serializer = new PineappleSerializer();

            Tcp = new Client_Tcp(this);
            //udp = new UDP();
            ClientSender = new ClientSender(this);
            ClientHandlers = new ClientHandlers(this);
            clientLooper = new ClientLooper(this);

            clientLooper.Start();

            Tcp.ConnectClientToServer();
        }

        public Client(Server server, int id)
        {
            IsServer = true;
            Id = id;

            Tcp = new Server_Tcp(server);
            //udp = new UDP(id);
        }

        private readonly bool IsServer;
        private readonly ClientLooper clientLooper;
        //
        public int Id { get; set; }
        public bool IsConnected { get; set; }
        //
        public string Ip { get; private set; }
        public int Port { get; private set; }
        //
        public Player Player { get; private set; }
        public _Tcp Tcp { get; private set; }
        public ClientSender ClientSender { get; private set; }
        public ClientHandlers ClientHandlers { get; private set; }
        public GameController GameController { get; private set; }
        public PineappleSerializer Serializer { get; private set; }


        public void AssignPlayerToClient(Player player)
        {
            if (this.Player != null)
                PineappleLogger.HandleException(new Exception("Client - AssignPlayerToClient"), true);

            this.Player = player;
        }

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
