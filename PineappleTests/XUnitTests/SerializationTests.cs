using PineappleLib.Models.Units;
using PineappleLib.Models.Players;
using PineappleLib.Enums;
using PineappleLib.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using PineappleLib.Serialization;
using PineappleLib.Models.Abilities;
using PineappleLib.Networking;

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
            Assert.Equal(peon.Abilities.Count, peonClone.Abilities.Count);
        }
        [Fact]
        public void PlayerTest01()
        {
            var serializer = new PineappleSerializer();
            var player = new Player(PlayerType.PLAYER);

            var data = serializer.Serialize(player);
            var playerClone = (Player)serializer.Deserialize(data);

            Assert.Equal(player.Name, playerClone.Name);
            Assert.Equal(player.Units[0].Name, playerClone.Units[0].Name);
            Assert.Equal(player.Units[0].Abilities.Count, playerClone.Units[0].Abilities.Count);
        }
        [Fact]
        public void AbilityTest01()
        {
            var serializer = new PineappleSerializer();
            var slap = new Slap();

            var data = serializer.Serialize(slap);
            var slapClone = (Slap)serializer.Deserialize(data);

            Assert.Equal(slap.Damage, slapClone.Damage);
            Assert.Equal(slap.AbilityEffects.Count, slapClone.AbilityEffects.Count);
        }
        [Fact]
        public void SerializeValuesTest()
        {
            int id = 1337;
            int expected = -1;

            var p1 = new Packet(id);

            p1.Write(expected);
            p1.WriteLength();

            var data = p1.ToArray();

            var p2 = new Packet(data);

            int len = p2.ReadInt();
            int aId = p2.ReadInt();
            int actual = p2.ReadInt();

            Assert.Equal(id, aId);
            Assert.Equal(expected, actual);

            p1.Dispose();
            p2.Dispose();
        }
    }
}
