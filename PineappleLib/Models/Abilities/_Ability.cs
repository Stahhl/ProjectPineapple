using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using PineappleLib.Enums;

namespace PineappleLib.Models.Abilities
{
    [Serializable]
    public abstract class _Ability
    {
        public List<AbilityEffect> AbilityEffects { get; protected set; }

        public AbilityType AbilityType { get; protected set; }
        public Element Element { get; protected set; }

        public int Damage { get; protected set; }
        public int Healing { get; protected set; }
    }
}
