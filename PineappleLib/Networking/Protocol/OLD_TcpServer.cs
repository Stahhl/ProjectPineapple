//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Net;
//using System.Net.Sockets;
//using System.Numerics;
//using PineappleLib.Logging;
//using PineappleLib.Enums;
//using static PineappleLib.General.Data.Values;

//namespace PineappleLib.Networking.TCP
//{
//    public class TcpServer : _Tcp
//    {
//        public TcpServer(int _id)
//        {
//            Id = _id;
//        }
//        public override void Connect(TcpClient _socket)
//        {
//            socket = _socket;
//            socket.ReceiveBufferSize = dataBufferSize;
//            socket.SendBufferSize = dataBufferSize;

//            stream = socket.GetStream();

//            //receivedData = new Packet();
//            receiveBuffer = new byte[dataBufferSize];

//            stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
//        }

//        public override void ReceiveCallback(IAsyncResult _result)
//        {
//            try
//            {
//                int _byteLength = stream.EndRead(_result);
//                if (_byteLength <= 0)
//                {
//                    //Server.clients[id].Disconnect();
//                    return;
//                }

//                byte[] _data = new byte[_byteLength];
//                Array.Copy(receiveBuffer, _data, _byteLength);

//                //receivedData.Reset(HandleData(_data)); // Reset receivedData if all data was handled
//                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
//            }
//            catch (Exception _ex)
//            {
//                PineappleLogger.PineappleLog(LogType.ERROR, $"Error receiving TCP data: {_ex}");
//                //Server.Clients[id].Disconnect();
//            }
//        }
//        public override void Disconnect()
//        {
//            socket.Close();
//            stream = null;
//            //receivedData = null;
//            receiveBuffer = null;
//            socket = null;
//        }
//    }
//}
