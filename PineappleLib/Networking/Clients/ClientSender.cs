using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PineappleLib.Enums;

namespace PineappleLib.Networking.Clients
{
    public class ClientSender
    {
        public ClientSender(Client client)
        {
            this.client = client;
        }

        private Client client;

        public void WelcomeReceived()
        {
            using (Packet _packet = new Packet((int)PacketType.WelcomeReceived))
            {
                //_packet.Write(client.Id);
                //_packet.Write(UIManager.instance.usernameField.text);

                //SendTCPData(_packet);
            }
        }
    }
}
