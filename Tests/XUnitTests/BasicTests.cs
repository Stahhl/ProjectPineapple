using System;
using Xunit;
using Library.Models.Units;
using Library.General.Data;

namespace XUnitTests
{
    public class BasicTests
    {
        [Fact]
        public void PeonTest()
        {
            var peon = new Peon();

            Assert.Equal(Values.BaseHealthPoints, peon.HealthPoints);
            Assert.Equal(Values.BaseActionPoints, peon.ActionPoints);
        }
    }
}
