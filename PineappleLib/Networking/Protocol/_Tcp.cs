﻿using PineappleLib.Networking.Clients;
using System;
using System.Net.Sockets;
using static PineappleLib.General.Values;
using PineappleLib.Logging;
using PineappleLib.Enums;
using PineappleLib.Networking.Servers;

namespace PineappleLib.Networking.Protocol
{
    public abstract class _Tcp
    {
        protected int? Id;
        protected NetworkStream stream;
        protected Packet receivedData;
        protected byte[] receiveBuffer;
        protected bool IsServer;

        public TcpClient Socket { get; protected set; }
        public Client Client { get; protected set; }
        public Server Server { get; protected set; }
        //public int Id { get; private set; }

        public virtual void ConnectServerToClient(int clientId, TcpClient _socket)
        {
            Socket = _socket;
            Socket.ReceiveBufferSize = dataBufferSize;
            Socket.SendBufferSize = dataBufferSize;

            stream = Socket.GetStream();

            receivedData = new Packet();
            receiveBuffer = new byte[dataBufferSize];

            stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);

            Server.ServerSender.WelcomeClient(clientId);
        }
        public virtual void ConnectClientToServer()
        {
            Socket = new TcpClient
            {
                ReceiveBufferSize = dataBufferSize,
                SendBufferSize = dataBufferSize
            };

            receiveBuffer = new byte[dataBufferSize];
            Socket.BeginConnect(Client.Ip, Client.Port, ConnectCallback, Socket);
        }

        protected virtual void ConnectCallback(IAsyncResult _result)
        {
            throw new NotImplementedException();
        }
        public virtual void ReceiveCallback(IAsyncResult _result)
        {
            throw new NotImplementedException();
        }
        public virtual bool HandleData(byte[] _data)
        {
            throw new NotImplementedException();
        }
        public virtual void Disconnect()
        {
            throw new NotImplementedException();
        }
        public void SendData(Packet _packet)
        {
            try
            {
                if (Socket != null)
                {
                    //PineappleLogger.PineappleLog(LogType.INFO, Socket.Client.LocalEndPoint.ToString() + " " + Socket.Client.RemoteEndPoint.ToString());
                    stream.BeginWrite(_packet.ToArray(), 0, _packet.Length(), null, null);
                }
            }
            catch (Exception _ex)
            {
                PineappleLogger.HandleException(_ex, true);
            }
        }

    }//class
}//namespace
