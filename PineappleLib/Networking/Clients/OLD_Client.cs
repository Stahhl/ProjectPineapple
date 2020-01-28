//using System.Collections;
//using System.Collections.Generic;
//using System.Net;
//using System.Net.Sockets;
//using System;
//using PineappleLib.Logging;
//using PineappleLib.General.Exceptions;
//using PineappleLib.Enums;
//using PineappleLib.Networking.TCP;

//namespace PineappleLib.Networking.Client
//{
//    public class Client : _Client
//    {
//        public string ip = "127.0.0.1";
//        public int port = 55555;
//        public int myId = 0;
//        //public UDP udp;

//        public bool isConnected = false;
//        //private delegate void PacketHandler(Packet _packet);
//        //private static Dictionary<int, PacketHandler> packetHandlers;

//        public Client()
//        {
//            Tcp = new TcpLocal(this);
//            //udp = new UDP();

//            ConnectToServer();
//        }

//        private void OnApplicationQuit()
//        {
//            Disconnect(); // Disconnect when the game is closed
//        }

//        /// <summary>Attempts to connect to the server.</summary>
//        public void ConnectToServer()
//        {
//            //try
//            //{
//            //    // Connect tcp, udp gets connected once tcp is done

//            //}
//            //catch (Exception e)
//            //{

//            //    throw e;
//            //}
//            ////InitializeClientData();

//            ////isConnected = true;
//            Tcp.Connect();
//        }



//        /// <summary>Disconnects from the server and stops all network traffic.</summary>
//        public void Disconnect()
//        {
//            if (isConnected)
//            {
//                isConnected = false;
//                Tcp.socket.Close();
//                //udp.socket.Close();

//                PineappleLogger.PineappleLog(LogType.DEBUG, "Disconnected from server.");
//            }
//        }

//    } //Class
//} // namespace
