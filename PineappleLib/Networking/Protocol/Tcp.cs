using PineappleLib.Networking.Clients;
using System;
using System.Net.Sockets;
using static PineappleLib.General.Data.Values;
using PineappleLib.Logging;
using PineappleLib.Enums;

namespace PineappleLib.Networking.Protocol
{
    public class Tcp
    {
        public Tcp(Client client)
        {
            IsServer = false;

            this.Client = client;
        }
        public Tcp(int _id)
        {
            IsServer = true;

            Id = _id;
        }

        public TcpClient socket;

        //protected int Id;
        protected NetworkStream stream;
        //private Packet receivedData;
        protected byte[] receiveBuffer;

        private readonly bool IsServer;

        public TcpClient Socket { get; private set; }
        public Client Client { get; private set; }
        public int Id { get; private set; }

        //Local
        public void Connect()
        {
            socket = new TcpClient
            {
                ReceiveBufferSize = dataBufferSize,
                SendBufferSize = dataBufferSize
            };

            receiveBuffer = new byte[dataBufferSize];
            socket.BeginConnect(Client.Ip, Client.Port, ConnectCallback, socket);
        }
        //Server
        public void Connect(TcpClient _socket)
        {
            socket = _socket;
            socket.ReceiveBufferSize = dataBufferSize;
            socket.SendBufferSize = dataBufferSize;

            stream = socket.GetStream();

            //receivedData = new Packet();
            receiveBuffer = new byte[dataBufferSize];

            stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
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
                Client.IsConnected = true;
            }
            catch (Exception e)
            {
                PineappleLogger.HandleException(e, false);
            }

        }
        public void ReceiveCallback(IAsyncResult _result)
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

                //receivedData.Reset(HandleData(_data)); // Reset receivedData if all data was handled
                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
            }
            catch (Exception _ex)
            {
                PineappleLogger.PineappleLog(LogType.ERROR, $"Error receiving TCP data: {_ex}");
                //Server.Clients[id].Disconnect();
            }
        }

        public void SendData(Packet _packet)
        {
            try
            {
                if (socket != null)
                {
                    //stream.BeginWrite(_packet.ToArray(), 0, _packet.Length(), null, null);
                }
            }
            catch (Exception _ex)
            {
                throw new Exception(_ex.Message);
            }
        }
        public bool HandleData(byte[] _data)
        {
            //int _packetLength = 0;

            //receivedData.SetBytes(_data);

            //if (receivedData.UnreadLength() >= 4)
            //{
            //    // If client's received data contains a packet
            //    _packetLength = receivedData.ReadInt();
            //    if (_packetLength <= 0)
            //    {
            //        // If packet contains no data
            //        return true; // Reset receivedData instance to allow it to be reused
            //    }
            //}

            //while (_packetLength > 0 && _packetLength <= receivedData.UnreadLength())
            //{
            //    // While packet contains data AND packet data length doesn't exceed the length of the packet we're reading
            //    byte[] _packetBytes = receivedData.ReadBytes(_packetLength);
            //    ThreadManager.ExecuteOnMainThread(() =>
            //    {
            //        using (Packet _packet = new Packet(_packetBytes))
            //        {
            //            int _packetId = _packet.ReadInt();
            //            Server.packetHandlers[_packetId](id, _packet); // Call appropriate method to handle the packet
            //        }
            //    });

            //    _packetLength = 0; // Reset packet length
            //    if (receivedData.UnreadLength() >= 4)
            //    {
            //        // If client's received data contains another packet
            //        _packetLength = receivedData.ReadInt();
            //        if (_packetLength <= 0)
            //        {
            //            // If packet contains no data
            //            return true; // Reset receivedData instance to allow it to be reused
            //        }
            //    }
            //}

            //if (_packetLength <= 1)
            //{
            //    return true; // Reset receivedData instance to allow it to be reused
            //}

            return false;
        }
        public void Disconnect()
        {
            if (IsServer == true)
                socket.Close();
            else
                Client.Disconnect();

            stream = null;
            //receivedData = null;
            receiveBuffer = null;
            socket = null;
        }


    }//class
}//namespace
