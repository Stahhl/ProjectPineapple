using System;
using Xunit;
using PineappleLib.Models.Units;
using PineappleLib.Models.Players;
using PineappleLib.Controllers;
using PineappleLib.General.Data;
using PineappleLib.Enums;
using static PineappleLib.General.Values;

namespace XUnitTests
{
    public class ModelTests
    {
        [Fact]
        public void UnitTest01()
        {
            var peon = new Unit();

            Assert.Equal("Peon", peon.Name);
            Assert.Equal(BaseHealthPoints, peon.HealthPoints);
            Assert.Equal(BaseActionPoints, peon.ActionPoints);
        }
        [Fact]
        public void PlayerTest01()
        {
            var player = new Player(PlayerType.PLAYER);

            Assert.Contains("PLAYER_", player.Name);
            Assert.Single(player.Units);
        }
    }
}
