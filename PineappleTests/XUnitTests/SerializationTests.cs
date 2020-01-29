using PineappleLib.Models.Units;
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
        public void PeonTest()
        {
            var serializer = new SerializationController();
            var peon = new Unit();

            var data = serializer.UnitSerializer.Serialize(peon);
            var peonClone = (Unit)serializer.UnitSerializer.Deserialize(data);

            Assert.Equal(peon.Id, peonClone.Id);
            Assert.Equal(peon.Name, peonClone.Name);
            Assert.Equal(peon.HealthPoints, peonClone.HealthPoints);
            Assert.Equal(peon.ActionPoints, peonClone.ActionPoints);
        }
    }
}
