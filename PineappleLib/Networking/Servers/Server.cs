using System;
using System.Net;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using PineappleLib.Logging;
using PineappleLib.Enums;
using PineappleLib.Networking.Clients;
using System.Net.NetworkInformation;
using static PineappleLib.General.Data.Values;

namespace PineappleLib.Networking.Servers
{
    public class Server
    {
        public Server(int port)
        {
            Clients = new Dictionary<int, Client>();
            ServerHandlers = new ServerHandlers(this);
            ServerSender = new ServerSender(this);

            Start(port);
        }

        public int MaxPlayers { get; private set; }
        public int Port { get; private set; }

        public ServerSender ServerSender { get; private set; }
        public ServerHandlers ServerHandlers { get; private set; }
        public Dictionary<int, Client> Clients { get; private set; }
        //public Dictionary<int, PacketHandler> PacketHandlers { get; private set; }

        //public delegate void PacketHandler(int _fromClient, Packet _packet);

        private TcpListener tcpListener;
        //private static UdpClient udpListener;

        private void Start(int port, int _maxPlayers = 5)
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
        public void Stop()
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
                if (Clients[i].Tcp.Socket == null)
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
                Clients.Add(i, new Client(this, i));
            }

            //packetHandlers = new Dictionary<int, PacketHandler>()
            //{
            //    { (int)ClientPackets.welcomeReceived, ServerHandle.WelcomeReceived },
            //    { (int)ClientPackets.playerMovement, ServerHandle.PlayerMovement },
            //};
        }
    }
}
