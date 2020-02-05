using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PineappleLib.Enums;
using static PineappleLib.General.Values;

namespace PineappleLib.Models.Abilities
{
    [Serializable]
    public class Slap : _Ability
    {
        public Slap()
        {
            AbilityEffects = new List<AbilityEffect>();

            AbilityType = AbilityType.ATTACK;
            Element = Element.PHYSICAL;

            Damage = BaseAbilityAmount;
            Healing = 0;

            AbilityEffects.Add(AbilityEffect.DAMAGE);
        }
    }
}
