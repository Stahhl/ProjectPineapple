using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PineappleLib.Enums;
using PineappleLib.Models.Players;
using PineappleLib.Controllers;
using PineappleLib.Serialization;
using PineappleLib.Models.Units;
using PineappleLib.Models.Abilities;

namespace PineappleLib.Networking.Clients
{
    public class ClientSender
    {
        public ClientSender(Client client)
        {
            this.client = client;
            this.serializer = client.Serializer;

            rnd = new Random();
        }

        private Client client;
        private PineappleSerializer serializer;
        private Random rnd;

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
        public void CreateLobby(string password, bool join = true)
        {
            using(Packet packet = new Packet((int)PacketType.CreateLobby))
            {
                packet.Write(client.Id);
                packet.Write(password);
                packet.Write(join);

                SendTCPData(packet);
            }
        }
        public void JoinLobby(string password, int lobbyId = -1)
        {
            using(Packet packet = new Packet((int)PacketType.JoinLobby))
            {
                packet.Write(client.Id);
                packet.Write(password);
                packet.Write(lobbyId);

                SendTCPData(packet);
            }
        }
        public void CombatCalc(Unit origin, Unit affected, _Ability ability)
        {
            string id = PacketType.CombactCalc + "_" + rnd.Next(1, 1000)+ "_" + DateTime.Now;
            int length = 3;

            using (Packet packet = new Packet((int)PacketType.CombactCalc))
            {
                packet.Write(client.Id);
                packet.Write(id);
                packet.Write(0);
                packet.Write(serializer.Serialize(origin));

                SendTCPData(packet);
            }
        }
    }
}
