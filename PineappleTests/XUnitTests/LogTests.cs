﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using PineappleLib.Logging;

namespace XUnitTests
{
    public class LogTests
    {
        [Fact]
        public void CanWriteTest()
        {
            Logger.Log("CanWriteTest", false);
        }
    }
}