using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PineappleLib.Enums;
using PineappleLib.Models.Players;
using PineappleLib.Controllers;
using PineappleLib.Serialization;

namespace PineappleLib.Networking.Clients
{
    public class ClientSender
    {
        public ClientSender(Client client)
        {
            this.client = client;
            this.serializer = client.Serializer;
        }

        private Client client;
        private PineappleSerializer serializer;

        private void SendTCPData(Packet _packet)
        {
            _packet.WriteLength();
            client.Tcp.SendData(_packet);
        }
        public void WelcomeReceived()
        {
            using (Packet _packet = new Packet((int)PacketType.WelcomeReceived))
            {
                _packet.Write(client.Id);
                _packet.Write(serializer.Serialize(client.Player));

                SendTCPData(_packet);
            }
        }
    }
}
