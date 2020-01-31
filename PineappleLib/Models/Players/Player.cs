using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PineappleLib.Models.Units;
using PineappleLib.Models.Controllers;
using PineappleLib.General.Data;
using System.Runtime.Serialization;
using PineappleLib.Serialization;

namespace PineappleLib.Models.Players
{
    [Serializable]
    public class Player
    {
        public Player(PlayerController pc)
        {
            Units = new List<Unit>();

            pC = pc;

            Name = "Player_" + PineappleRandom.GetRandomOfDigits(4);

            Units.Add(new Unit());
        }

        [NonSerialized]
        public readonly PlayerController pC;

        public string Name { get; private set; }
        public List<Unit> Units { get; private set; }
    }
}
