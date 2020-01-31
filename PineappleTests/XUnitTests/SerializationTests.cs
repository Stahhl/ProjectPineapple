using PineappleLib.Models.Units;
using PineappleLib.Models.Players;
using PineappleLib.Models.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using PineappleLib.Serialization;

namespace XUnitTests
{
    public class SerializationTests
    {
        [Fact]
        public void UnitTest01()
        {
            var serializer = new PineappleSerializer();
            var peon = new Unit();

            var data = serializer.Serialize(peon);
            var peonClone = (Unit)serializer.Deserialize(data);

            Assert.Equal(peon.Id, peonClone.Id);
            Assert.Equal(peon.Name, peonClone.Name);
            Assert.Equal(peon.HealthPoints, peonClone.HealthPoints);
            Assert.Equal(peon.ActionPoints, peonClone.ActionPoints);
        }
        [Fact]
        public void PlayerTest01()
        {
            var pC = new PlayerController();
            var player = pC.Player;
            var serializer = pC.Serializer;

            var data = serializer.Serialize(player);
            var playerClone = (Player)serializer.Deserialize(data);

            Assert.Equal(player.Name, playerClone.Name);
            Assert.Equal(player.Units[0].Name, playerClone.Units[0].Name);
        }
    }
}
