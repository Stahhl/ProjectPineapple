using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PineappleLib.Models.Units;
using PineappleLib.Controllers;
using PineappleLib.General.Data;
using System.Runtime.Serialization;
using PineappleLib.Serialization;
using PineappleLib.Enums;

namespace PineappleLib.Models.Players
{
    [Serializable]
    public class Player
    {
        public Player(PlayerType playerType)
        {
            Units = new List<Unit>();

            PlayerType = playerType;
            Name = PlayerType.ToString() + "_" + PineappleRandom.GetRandomOfDigits(4);

            Units.Add(new Unit());
        }

        public PlayerType PlayerType { get; private set; }
        public string Name { get; private set; }
        public List<Unit> Units { get; private set; }
    }
}
