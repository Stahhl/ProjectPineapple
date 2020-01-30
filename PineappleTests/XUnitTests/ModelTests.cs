using System;
using Xunit;
using PineappleLib.Models.Units;
using PineappleLib.Models.Players;
using PineappleLib.General.Data;

namespace XUnitTests
{
    public class ModelTests
    {
        [Fact]
        public void UnitTest01()
        {
            var peon = new Unit();

            Assert.Equal("Peon", peon.Name);
            Assert.Equal(Values.BaseHealthPoints, peon.HealthPoints);
            Assert.Equal(Values.BaseActionPoints, peon.ActionPoints);
        }
        [Fact]
        public void PlayerTest01()
        {
            var player = new Player();

            Assert.Contains("Player_", player.Name);
            Assert.Single(player.Units);
        }
    }
}
