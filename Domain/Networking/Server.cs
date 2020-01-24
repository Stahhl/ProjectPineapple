using System;
using System.Net;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using Domain.General.Logging;

namespace Domain.Networking
{
    public class Server
    {
        public static int MaxPlayers { get; private set; }
        public static int Port { get; private set; }
        public static Dictionary<int, ClientServer> Clients;
        //public delegate void PacketHandler(int _fromClient, Packet _packet);
        //public static Dictionary<int, PacketHandler> packetHandlers;

        private static TcpListener tcpListener;
        //private static UdpClient udpListener;

        public void Start(int _maxPlayers, int _port)
        {
            try
            {
                MaxPlayers = _maxPlayers;
                Port = _port;
                Clients = new Dictionary<int, ClientServer>();

                Logger.Log("Starting server...", false);
                InitializeServerData();

                tcpListener = new TcpListener(IPAddress.Any, Port);
                tcpListener.Stop();
                tcpListener.Start();
                tcpListener.BeginAcceptTcpClient(TCPConnectCallback, null);
                Logger.Log($"Server started on port {Port}.", false);
            }
            catch (Exception e)
            {
                Logger.HandleException(e, true);
            }
        }
        /// <summary>Handles new TCP connections.</summary>
        private void TCPConnectCallback(IAsyncResult _result)
        {
            TcpClient _client = tcpListener.EndAcceptTcpClient(_result);
            tcpListener.BeginAcceptTcpClient(TCPConnectCallback, null);
            Logger.Log($"Incoming connection from {_client.Client.RemoteEndPoint}...", false);

            for (int i = 1; i <= MaxPlayers; i++)
            {
                if (Clients[i].tcp.socket == null)
                {
                    Clients[i].tcp.Connect(_client);
                    return;
                }
            }

            Logger.Log($"{_client.Client.RemoteEndPoint} failed to connect: Server full!", false);
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
