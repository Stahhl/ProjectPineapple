using PineappleLib.Networking.Lobbys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PineappleLib.General.Data.Values;

namespace PineappleLib.Networking.Servers
{
    public class ServerHelper
    {
        public ServerHelper(Server server)
        {
            Server = server;
        }

        private readonly Server Server;

        public void CreateLobby(int clientId, string password)
        {
            for (int i = 0; i <= serverMaxLobbys; i++)
            {
                if(Server.Lobbys[i] == null)
                {
                    Server.Lobbys[i] = new Lobby(Server, clientId, password);
                    break;
                }
            }
        }
    }
}
