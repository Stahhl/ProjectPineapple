using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System;
using PineappleLib.Logging;
using PineappleLib.General.Exceptions;
using PineappleLib.Enums;
using static PineappleLib.General.Data.Values;
using PineappleLib.Networking.Clients;


namespace PineappleLib.Networking.Protocol
{
    public class Client_Tcp : _Tcp
    {
        public Client_Tcp(Client client)
        {
            IsServer = false;

            Client = client;
        }

        public override void Connect()
        {
            Socket = new TcpClient
            {
                ReceiveBufferSize = dataBufferSize,
                SendBufferSize = dataBufferSize
            };

            receiveBuffer = new byte[dataBufferSize];
            Socket.BeginConnect(Client.Ip, Client.Port, ConnectCallback, Socket);
        }

        protected override void ConnectCallback(IAsyncResult _result)
        {
            try
            {
                Socket.EndConnect(_result);

                if (!Socket.Connected)
                {
                    return;
                }

                stream = Socket.GetStream();

                receivedData = new Packet();
                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
                Client.IsConnected = true;
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
                    if (IsServer == false)
                        Client.Disconnect();

                    //TODO if server is true disconnect that client
                    //Server.clients[id].Disconnect();
                    return;
                }

                byte[] _data = new byte[_byteLength];
                Array.Copy(receiveBuffer, _data, _byteLength);

                receivedData.Reset(HandleData(_data)); // Reset receivedData if all data was handled
                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
            }
            catch (Exception _ex)
            {
                PineappleLogger.PineappleLog(LogType.ERROR, $"Error receiving TCP data: {_ex}");
                //Server.Clients[id].Disconnect();
            }
        }

        public override bool HandleData(byte[] _data)
        {
            int _packetLength = 0;

            receivedData.SetBytes(_data);

            if (receivedData.UnreadLength() >= 4)
            {
                // If client's received data contains a packet
                _packetLength = receivedData.ReadInt();
                if (_packetLength <= 0)
                {
                    // If packet contains no data
                    return true; // Reset receivedData instance to allow it to be reused
                }
            }

            while (_packetLength > 0 && _packetLength <= receivedData.UnreadLength())
            {
                // While packet contains data AND packet data length doesn't exceed the length of the packet we're reading
                byte[] _packetBytes = receivedData.ReadBytes(_packetLength);
                ThreadManager.ExecuteOnMainThread(() =>
                {
                    using (Packet _packet = new Packet(_packetBytes))
                    {
                        int _packetId = _packet.ReadInt();
                        //Server.packetHandlers[_packetId](id, _packet); // Call appropriate method to handle the packet
                    }
                });

                _packetLength = 0; // Reset packet length
                if (receivedData.UnreadLength() >= 4)
                {
                    // If client's received data contains another packet
                    _packetLength = receivedData.ReadInt();
                    if (_packetLength <= 0)
                    {
                        // If packet contains no data
                        return true; // Reset receivedData instance to allow it to be reused
                    }
                }
            }

            if (_packetLength <= 1)
            {
                return true; // Reset receivedData instance to allow it to be reused
            }

            return false;
        }
        public override void Disconnect()
        {
            Client.Disconnect();
            stream = null;
            //receivedData = null;
            receiveBuffer = null;
            Socket = null;
        }
    }
}
