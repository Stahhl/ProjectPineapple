using PineappleLib.Models.Units;
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
    }
}
