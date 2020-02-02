using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PineappleLib.Networking;
using System.Net;
using System.Net.Sockets;
using PineappleLib.Enums;
using PineappleLib.Networking.Clients;

namespace PineappleLib.Networking.Servers
{
    public class ServerSender
    {
        public ServerSender(Server server)
        {
            this.server = server;
        }

        private Server server;

        public void WelcomeClient(int clientId,  string msg = "Welcome to the Server!")
        {
            using (Packet packet = new Packet((int)PacketType.Welcome))
            {
                packet.Write(msg);
                packet.Write(clientId);

                SendTcpData(clientId, packet);
            }
        }

        private void SendTcpData(int clientId, Packet packet)
        {
            packet.WriteLength();
            server.Clients[clientId].Tcp.SendData(packet);
        }

        private void SendTcpDataToAll(Packet packet)
        {
            packet.WriteLength();
            for (int i = 1; i <= server.MaxPlayers; i++)
            {
                server.Clients[i].Tcp.SendData(packet);
            }
        }
        private void SendTcpDataToAllExcept(int exceptClient, Packet packet)
        {
            packet.WriteLength();
            for (int i = 1; i <= server.MaxPlayers; i++)
            {
                if (i != exceptClient)
                    server.Clients[i].Tcp.SendData(packet);
            }
        }
    }
}
