using PineappleLib.Models.Abilities;
using PineappleLib.Models.Units;
using PineappleLib.Networking.Servers;
using PineappleLib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PineappleLib.Controllers
{
    public class CombatController
    {
        public CombatController(GameController game)
        {
            this.gameController = game;
        }

        private List<Unit> affectedUnits;

        private GameController gameController;

        public void CombatCalc(Unit origin, Unit affected, _Ability ability)
        {
            affectedUnits = new List<Unit>();

            foreach (var effect in ability.AbilityEffects)
            {
                switch(effect)
                {
                    case AbilityEffect.DAMAGE:
                        DamageCalc(origin, affected, ability);
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }

            if(gameController.GameType == GameType.SERVER && affectedUnits.Count > 0)
            {
                //TODO send updated units to all clients
            }
        }
        private void DamageCalc(Unit origin, Unit affected, _Ability ability)
        {
            affected.AdjustHealth(ability.Damage, true);

            affectedUnits.Add(affected);
        }
    }
}
