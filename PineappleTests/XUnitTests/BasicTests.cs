using System;
using Xunit;
using PineappleLib.Models.Units;
using PineappleLib.General.Data;

namespace XUnitTests
{
    public class BasicTests
    {
        [Fact]
        public void PeonTest()
        {
            var peon = new Unit();

            Assert.Equal("Peon", peon.Name);
            Assert.Equal(Values.BaseHealthPoints, peon.HealthPoints);
            Assert.Equal(Values.BaseActionPoints, peon.ActionPoints);
        }
    }
}
