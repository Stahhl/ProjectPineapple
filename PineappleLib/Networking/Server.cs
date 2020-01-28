using System;
using System.Net;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using PineappleLib.Logging;
using PineappleLib.Enums;
using PineappleLib.Networking.Client;
using System.Net.NetworkInformation;

namespace PineappleLib.Networking
{

    public class Server
    {
        public Server(int port)
        {
            Init(5, port);
        }

        public int MaxPlayers { get; private set; }
        public int Port { get; private set; }

        public Dictionary<int, ClientServer> Clients;
        //public delegate void PacketHandler(int _fromClient, Packet _packet);
        //public static Dictionary<int, PacketHandler> packetHandlers;

        private static TcpListener tcpListener;
        //private static UdpClient udpListener;

        private void Init(int _maxPlayers, int port)
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
                Clients = new Dictionary<int, ClientServer>();

                PineappleLogger.PineappleLog(LogType.DEBUG, "Starting server...");
                InitializeServerData();

                tcpListener = new TcpListener(IPAddress.Any, Port);
                tcpListener.Start();
                tcpListener.BeginAcceptTcpClient(TCPConnectCallback, null);
                PineappleLogger.PineappleLog(LogType.DEBUG, $"Server started on port {Port}.");
            }
            catch (Exception e)
            {
                PineappleLogger.HandleException(e, false);
            }
        }
        public void StopServer()
        {
            try
            {
                PineappleLogger.PineappleLog(LogType.DEBUG, "Stopping server...");
                tcpListener.Stop();
            }
            catch (Exception e)
            {
                PineappleLogger.HandleException(e, true);
            }
        }
        /// <summary>Handles new TCP connections.</summary>
        private void TCPConnectCallback(IAsyncResult _result)
        {
            TcpClient _client = tcpListener.EndAcceptTcpClient(_result);
            tcpListener.BeginAcceptTcpClient(TCPConnectCallback, null);
            PineappleLogger.PineappleLog(LogType.DEBUG, $"Incoming connection from {_client.Client.RemoteEndPoint}...");

            for (int i = 1; i <= MaxPlayers; i++)
            {
                if (Clients[i].Tcp.socket == null)
                {
                    Clients[i].Tcp.Connect(_client);
                    return;
                }
            }

            PineappleLogger.PineappleLog(LogType.WARNING, $"{_client.Client.RemoteEndPoint} failed to connect: Server full!");
        }
        /// <summary>Initializes all necessary server data.</summary>
        private void InitializeServerData()
        {
            for (int i = 1; i <= MaxPlayers; i++)
            {
                Clients.Add(i, new ClientServer(i));
            }

            //packetHandlers = new Dictionary<int, PacketHandler>()
            //{
            //    { (int)ClientPackets.welcomeReceived, ServerHandle.WelcomeReceived },
            //    { (int)ClientPackets.playerMovement, ServerHandle.PlayerMovement },
            //};
        }
    }
}
