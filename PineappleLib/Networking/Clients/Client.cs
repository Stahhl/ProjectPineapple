﻿using System;
using System.Net.Sockets;
using PineappleLib.Networking.Protocol;
using PineappleLib.Logging;
using PineappleLib.Enums;
using static PineappleLib.General.Data.Values;
using static PineappleLib.General.Helpers.Disposer;
using PineappleLib.Networking.Servers;
using System.Collections.Generic;
using PineappleLib.Models.Players;
using PineappleLib.Models.Controllers;
using PineappleLib.General.Exceptions;
using PineappleLib.Networking.Loopers;

namespace PineappleLib.Networking.Clients
{
    public class Client
    {
        public Client(PlayerController pc)
        {
            //throw new NotImplementedException();
            IsServer = false;

            Ip = stdIp;
            Port = stdPort;

            AssignPlayerToClient(pc.Player);

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
        public Client(Server server)
        {
            IsServer = true;

            Tcp = new Server_Tcp(server);
            //udp = new UDP(id);
        }


        private readonly bool IsServer;

        public int Id { get; set; }
        public bool IsConnected { get; set; }

        public string Ip { get; private set; }
        public int Port { get; private set; }

        public Player Player { get; private set; }
        public _Tcp Tcp { get; private set; }
        public ClientSender ClientSender { get; private set; }
        public ClientHandlers ClientHandlers { get; private set; }

        private ClientLooper clientLooper;

        public void AssignPlayerToClient(Player player)
        {
            if (this.Player != null)
                PineappleLogger.HandleException(new SingletonException(), true);

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
