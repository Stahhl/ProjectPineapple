using PineappleLib.Models.Players;
using PineappleLib.Networking.Clients;
using PineappleLib.Serialization;
using PineappleLib.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using PineappleLib.Networking;
using PineappleLib.Enums;
using PineappleLib.Networking.Servers;

namespace PineappleLib.Controllers
{
    public class GameController
    {
        public GameController(Player player)
        {
            Players = new List<Player>();

            Server = null;

            Players.Add(player);
        }
        public GameController(Server server)
        {
            Players = new List<Player>();

            Server = server;
        }

        public GameType GameType { get; private set; }
        public Server Server { get; private set; }
        public CombatController CombatController { get; private set; }
        public List<Player> Players { get; private set; }
        public List<EnemyController> Enemies { get; private set; }
        public Client Client { get; private set; }

        public void StartServer(bool simulate = false)
        {
            if (Server.Clients.Count > 0 ||  Server == null || Client != null)
                PineappleLogger.HandleException(new Exception("GameController - StartServer()"), true);

            GameType = GameType.ONLINE;
        }
        public void StartOnline(bool simulate = false)
        {
            if (Players.Count <= 0 || Server != null || Client != null)
                PineappleLogger.HandleException(new Exception("GameController - StartOnline()"), true);

            GameType = GameType.ONLINE;

            if (simulate == false)
            {
                Client = new Client(this);
            }
        }
        public void StartOffline(bool simulate = false)
        {
            if (Players.Count <= 0 || Server != null || Client != null)
                PineappleLogger.HandleException(new Exception("GameController - StartOffline()"), true);

            Enemies = new List<EnemyController>();

            GameType = GameType.OFFLINE;

            Enemies.Add(new EnemyController(this));
        }

    }
}
