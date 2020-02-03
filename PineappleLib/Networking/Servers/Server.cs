﻿using System;
using System.Net;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using PineappleLib.Logging;
using PineappleLib.Enums;
using PineappleLib.Networking.Clients;
using System.Net.NetworkInformation;
using PineappleLib.Models.Controllers;
using PineappleLib.Serialization;
using static PineappleLib.General.Data.Values;
using System.Threading;
using PineappleLib.Networking.Loopers;
using System.Threading.Tasks;

namespace PineappleLib.Networking.Servers
{
    public class Server
    {
        public Server()
        {
            Clients = new Dictionary<int, Client>();
            ServerHandlers = new ServerHandlers(this);
            ServerSender = new ServerSender(this);
            serverLooper = new ServerLooper(this);
            Serializer = new PineappleSerializer();
        }

        public int MaxPlayers { get; private set; }
        public int Port { get; private set; }
        public bool IsRunning { get; private set; }

        public PineappleSerializer Serializer { get; private set; }
        public ServerSender ServerSender { get; private set; }
        public ServerHandlers ServerHandlers { get; private set; }
        public Dictionary<int, Client> Clients { get; private set; }

        private ServerLooper serverLooper;
        private TcpListener tcpListener;
        //private static UdpClient udpListener;

        public void Start(int port, int _maxPlayers = 5)
        {
            try
            {
                IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
                TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

                foreach (var tcpi in tcpConnInfoArray)
                {
                    if (tcpi.LocalEndPoint.Port == port)
                        throw new SocketException();
                }

                Port = port;
                MaxPlayers = _maxPlayers;
                IsRunning = true;

                PineappleLogger.PineappleLog(LogType.INFO, "Starting server...");
                InitializeServerData();

                serverLooper.Start();

                tcpListener = new TcpListener(IPAddress.Any, Port);
                tcpListener.Start();
                tcpListener.BeginAcceptTcpClient(TCPConnectCallback, null);
                PineappleLogger.PineappleLog(LogType.INFO, $"Server started on port {Port}.");
            }
            catch (Exception e)
            {
                PineappleLogger.HandleException(e, false);
            }
        }
        public void Stop()
        {
            try
            {
                PineappleLogger.PineappleLog(LogType.WARNING, "Stopping server...");
                IsRunning = false;

                ThreadManager.Stop();
                serverLooper.Stop();
                tcpListener.Stop();

                Thread.Sleep(50);
            }
            catch (Exception e)
            {
                PineappleLogger.HandleException(e, true);
            }
        }
        /// <summary>Handles new TCP connections.</summary>
        private void TCPConnectCallback(IAsyncResult _result)
        {
            if(IsRunning == false)
            {
                PineappleLogger.PineappleLog(LogType.WARNING, "Server - TCPConnectCallback - IsRunning == false");
                return;
            }

            TcpClient socket = tcpListener.EndAcceptTcpClient(_result);
            tcpListener.BeginAcceptTcpClient(TCPConnectCallback, null);
            PineappleLogger.PineappleLog(LogType.INFO, $"Incoming connection from {socket.Client.RemoteEndPoint}...");

            ThreadManager.QueueClient(socket);
            //PineappleLogger.PineappleLog(LogType.WARNING, $"{_client.Client.RemoteEndPoint} failed to connect: Server full!");
        }
        /// <summary>Initializes all necessary server data.</summary>
        private void InitializeServerData()
        {
            for (int i = 1; i <= MaxPlayers; i++)
            {
                Clients.Add(i, null);
            }
        }
    }
}
