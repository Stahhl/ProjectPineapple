using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System;
using Domain.General.Logging;
using Domain.General.Data;

namespace Domain.Networking
{
    public class ClientLocal
    {
        public static ClientLocal instance;
        public static int dataBufferSize = 4096;

        public string ip = "127.0.0.1";
        public int port = 55555;
        public int myId = 0;
        public TCP tcp;
        //public UDP udp;

        public bool isConnected = false;
        //private delegate void PacketHandler(Packet _packet);
        //private static Dictionary<int, PacketHandler> packetHandlers;

        public void Init()
        {
            //try
            //{

            //}
            //catch (Exception e)
            //{

            //    throw e;
            //}


            if (instance != null || (instance != null && instance != this))
                Logger.HandleException(new SingletonException(), true);


            instance = this;
            tcp = new TCP();
            //udp = new UDP();

            ConnectToServer();
        }

        private void OnApplicationQuit()
        {
            Disconnect(); // Disconnect when the game is closed
        }

        /// <summary>Attempts to connect to the server.</summary>
        public void ConnectToServer()
        {
            //try
            //{
            //    // Connect tcp, udp gets connected once tcp is done

            //}
            //catch (Exception e)
            //{

            //    throw e;
            //}
            ////InitializeClientData();

            ////isConnected = true;
            tcp.Connect();
        }

        public class TCP
        {
            public TcpClient socket;

            private NetworkStream stream;
            //private Packet receivedData;
            private byte[] receiveBuffer;

            /// <summary>Attempts to connect to the server via TCP.</summary>
            public void Connect()
            {
                socket = new TcpClient
                {
                    ReceiveBufferSize = dataBufferSize,
                    SendBufferSize = dataBufferSize
                };

                receiveBuffer = new byte[dataBufferSize];
                socket.BeginConnect(instance.ip, instance.port, ConnectCallback, socket);
            }

            /// <summary>Initializes the newly connected client's TCP-related info.</summary>
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
                    instance.isConnected = true;
                }
                catch (Exception e)
                {
                    Logger.HandleException(e, false);
                }

            }

            /// <summary>Sends data to the client via TCP.</summary>
            /// <param name="_packet">The packet to send.</param>
            //public void SendData(Packet _packet)
            //{
            //    try
            //    {
            //        if (socket != null)
            //        {
            //            stream.BeginWrite(_packet.ToArray(), 0, _packet.Length(), null, null); // Send data to server
            //        }
            //    }
            //    catch (Exception _ex)
            //    {
            //        Logger.Throw(_ex);
            //    }
            //}

            /// <summary>Reads incoming data from the stream.</summary>
            private void ReceiveCallback(IAsyncResult _result)
            {
                try
                {
                    int _byteLength = stream.EndRead(_result);
                    if (_byteLength <= 0)
                    {
                        instance.Disconnect();
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

            ///// <summary>Prepares received data to be used by the appropriate packet handler methods.</summary>
            ///// <param name="_data">The recieved data.</param>
            //private bool HandleData(byte[] _data)
            //{
            //    int _packetLength = 0;

            //    receivedData.SetBytes(_data);

            //    if (receivedData.UnreadLength() >= 4)
            //    {
            //        // If client's received data contains a packet
            //        _packetLength = receivedData.ReadInt();
            //        if (_packetLength <= 0)
            //        {
            //            // If packet contains no data
            //            return true; // Reset receivedData instance to allow it to be reused
            //        }
            //    }

            //    while (_packetLength > 0 && _packetLength <= receivedData.UnreadLength())
            //    {
            //        // While packet contains data AND packet data length doesn't exceed the length of the packet we're reading
            //        byte[] _packetBytes = receivedData.ReadBytes(_packetLength);
            //        ThreadManager.ExecuteOnMainThread(() =>
            //        {
            //            using (Packet _packet = new Packet(_packetBytes))
            //            {
            //                int _packetId = _packet.ReadInt();
            //                packetHandlers[_packetId](_packet); // Call appropriate method to handle the packet
            //            }
            //        });

            //        _packetLength = 0; // Reset packet length
            //        if (receivedData.UnreadLength() >= 4)
            //        {
            //            // If client's received data contains another packet
            //            _packetLength = receivedData.ReadInt();
            //            if (_packetLength <= 0)
            //            {
            //                // If packet contains no data
            //                return true; // Reset receivedData instance to allow it to be reused
            //            }
            //        }
            //    }

            //    if (_packetLength <= 1)
            //    {
            //        return true; // Reset receivedData instance to allow it to be reused
            //    }

            //    return false;
            //}

            /// <summary>Disconnects from the server and cleans up the TCP connection.</summary>
            private void Disconnect()
            {
                instance.Disconnect();

                stream = null;
                //receivedData = null;
                receiveBuffer = null;
                socket = null;
            }
        }
        ///// <summary>Initializes all necessary client data.</summary>
        //private void InitializeClientData()
        //{
        //    packetHandlers = new Dictionary<int, PacketHandler>()
        //{
        //    { (int)ServerPackets.welcome, ClientHandle.Welcome },
        //    { (int)ServerPackets.spawnPlayer, ClientHandle.SpawnPlayer },
        //    { (int)ServerPackets.playerPosition, ClientHandle.PlayerPosition },
        //    { (int)ServerPackets.playerRotation, ClientHandle.PlayerRotation },
        //};
        //    Debug.Log("Initialized packets.");
        //}

        /// <summary>Disconnects from the server and stops all network traffic.</summary>
        private void Disconnect()
        {
            if (isConnected)
            {
                isConnected = false;
                tcp.socket.Close();
                //udp.socket.Close();

                Logger.Log("Disconnected from server.", false);
            }
        }

    } //Class
} // namespace
