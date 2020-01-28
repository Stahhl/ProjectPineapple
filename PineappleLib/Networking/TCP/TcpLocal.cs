using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System;
using PineappleLib.Logging;
using PineappleLib.General.Exceptions;
using PineappleLib.Enums;
using static PineappleLib.General.Data.Values;
using PineappleLib.Networking.Client;


namespace PineappleLib.Networking.TCP
{
    public class TcpLocal : _Tcp
    {
        public TcpLocal(ClientLocal client)
        {
            this.client = client;
        }

        private ClientLocal client;

        public override void Connect()
        {
            socket = new TcpClient
            {
                ReceiveBufferSize = dataBufferSize,
                SendBufferSize = dataBufferSize
            };

            receiveBuffer = new byte[dataBufferSize];
            socket.BeginConnect(client.ip, client.port, ConnectCallback, socket);
        }
        private void ConnectCallback(IAsyncResult _result)
        {
            try
            {
                socket.EndConnect(_result);

                if (!socket.Connected)
                {
                    return;
                }

                stream = socket.GetStream();

                //receivedData = new Packet();
                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
                client.isConnected = true;
            }
            catch (Exception e)
            {
                PineappleLogger.HandleException(e, false);
            }

        }
        public override void ReceiveCallback(IAsyncResult _result)
        {
            try
            {
                int _byteLength = stream.EndRead(_result);
                if (_byteLength <= 0)
                {
                    client.Disconnect();
                    return;
                }

                byte[] _data = new byte[_byteLength];
                Array.Copy(receiveBuffer, _data, _byteLength);

                //receivedData.Reset(HandleData(_data)); // Reset receivedData if all data was handled
                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
            }
            catch
            {
                Disconnect();
            }
        }
        public override void Disconnect()
        {
            client.Disconnect();

            stream = null;
            //receivedData = null;
            receiveBuffer = null;
            socket = null;
        }
    }
}
