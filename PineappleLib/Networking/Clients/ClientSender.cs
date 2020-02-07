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
using PineappleLib.Logging;
using static PineappleLib.General.PacketIdentity;

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
        private const string type = "[Client]";

        private void SendTCPData(Packet _packet)
        {
            PineappleLogger.Log(LogType.INFO, $"{type} [{client.Id}] SendTCPData()");
            _packet.WriteLength();
            client.Tcp.SendData(_packet);
        }
        public void WelcomeReceived()
        {
            //PineappleLogger.Log(LogType.INFO, $"{type} [{client.Id}] WelcomeReceived()");

            using (Packet _packet = new Packet((int)PacketType.WelcomeReceived))
            {
                _packet.Write(client.Id);
                _packet.Write(serializer.Serialize(client.Player));

                SendTCPData(_packet);
            }
        }
        public void CreateLobby(string password, bool join = true)
        {
            //PineappleLogger.Log(LogType.INFO, $"{type} [{client.Id}] CreateLobby()");

            using (Packet packet = new Packet((int)PacketType.CreateLobby))
            {
                packet.Write(client.Id);
                packet.Write(password);
                packet.Write(join);

                SendTCPData(packet);
            }
        }
        public void JoinLobby(string password, int lobbyId = -1)
        {
            //PineappleLogger.Log(LogType.INFO, $"{type} [{client.Id}] JoinLobby()");

            using (Packet packet = new Packet((int)PacketType.JoinLobby))
            {
                packet.Write(client.Id);
                packet.Write(password);
                packet.Write(lobbyId);

                SendTCPData(packet);
            }
        }
        public void CombatCalc(Unit origin, Unit affected, _Ability ability)
        {
            //PineappleLogger.Log(LogType.INFO, $"{type} [{client.Id}] CombatCalc()");

            string id = GetPacketId(PacketType.CombactCalc);
            int length = 3;

            using (Packet packet = new Packet((int)PacketType.CombactCalc))
            {
                packet.Write(client.Id);
                packet.Write(id);
                packet.Write(length);
                packet.Write(0);
                packet.Write(serializer.Serialize(origin));

                SendTCPData(packet);
            }
            using (Packet packet = new Packet((int)PacketType.CombactCalc))
            {
                packet.Write(client.Id);
                packet.Write(id);
                packet.Write(length);
                packet.Write(1);
                packet.Write(serializer.Serialize(affected));

                SendTCPData(packet);
            }
            using (Packet packet = new Packet((int)PacketType.CombactCalc))
            {
                packet.Write(client.Id);
                packet.Write(id);
                packet.Write(length);
                packet.Write(2);
                packet.Write(serializer.Serialize(ability));

                SendTCPData(packet);
            }
        }
    }
}
