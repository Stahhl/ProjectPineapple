using PineappleLib.Enums;
using PineappleLib.Models.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PineappleLib.Controllers
{
    public class EnemyController
    {
        public EnemyController(GameController gameController)
        {
            GameController = gameController;
            Player = new Player(PlayerType.NPC);
        }

        public GameController GameController { get; private set; }
        public Player Player { get; private set; }
    }
}
