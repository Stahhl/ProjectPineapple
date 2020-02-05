using PineappleLib.Controllers;
using PineappleLib.Enums;
using PineappleLib.Models.Players;
using PineappleLib.Models.Units;
using static PineappleLib.General.Values;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTests
{
    public class CombatTests
    {
        [Fact]
        public void UnitAdjustHpTest01()
        {
            var p1 = new Unit();
            var p2 = new Unit();

            int amount = 5;

            int e1 = p1.HealthPoints - amount;
            int e2 = p2.HealthPoints + amount;

            p1.AdjustHealth(amount, true);
            p2.AdjustHealth(amount, false);

            Assert.Equal(e1, p1.HealthPoints);
            Assert.Equal(e2, p2.HealthPoints);
        }
        [Fact]
        public void SlapLocallyTest()
        {
            var game = new GameController(new Player(PlayerType.PLAYER));

            game.StartOffline();

            var attacker = game.Players[0].Units[0];
            var defender = game.Enemies[0].Player.Units[0];

            int expected = BaseHealthPoints - BaseAbilityAmount;

            game.CombatController.CombatCalc(attacker, defender, attacker.Abilities[0]);

            Assert.Equal(expected, defender.HealthPoints);
        }
    }
}
