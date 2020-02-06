using PineappleLib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PineappleLib.Networking.Servers
{
    public class ServerHandlers
    {
        public ServerHandlers(Server server)
        {
            serverHandle = new ServerHandle(server);

            Handlers = new Dictionary<int, PacketHandler>()
            {
                {(int)PacketType.WelcomeReceived, serverHandle.WelcomeReceived },

                {(int)PacketType.CreateLobby, serverHandle.CreateLobby },
                {(int)PacketType.JoinLobby, serverHandle.JoinLobby },

                {(int)PacketType.CombactCalc, serverHandle.CombatCalc },
            };
        }

        public delegate void PacketHandler(int clientId, Packet packet);
        public Dictionary<int, PacketHandler> Handlers;

        private ServerHandle serverHandle;
    }
}
