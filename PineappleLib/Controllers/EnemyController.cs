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
        }

        public GameController GameController { get; private set; }
    }
}
