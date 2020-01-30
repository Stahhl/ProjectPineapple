﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PineappleLib.Models.Units;
using PineappleLib.General.Data;
using System.Runtime.Serialization;

namespace PineappleLib.Models.Players
{
    [Serializable]
    public class Player
    {
        public Player()
        {
            Units = new List<Unit>();

            Name = "Player_" + PineappleRandom.GetRandomOfDigits(4);

            Units.Add(new Unit());
        }
        public Player(SerializationInfo info)
        {
            Units = new List<Unit>();

        }

        public string Name { get; private set; }
        public List<Unit> Units { get; private set; }
    }
}