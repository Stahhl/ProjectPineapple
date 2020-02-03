using PineappleLib.Logging;
using PineappleLib.Networking.Servers;
using static PineappleLib.General.Data.Values;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using PineappleLib.Models.Controllers;

namespace XUnitTests
{
    public class NetworkTests
    {
        [Fact]
        public async Task AdjustUnitValueTest01()
        {
            var lg = new AsyncLogger();
            var server = new Server(stdPort);

            var player1 = new PlayerController();
            var player2 = new PlayerController();

            player1.OnlineGame();
            player2.OnlineGame();

            Assert.Null(await Record.ExceptionAsync(() => lg.WaitForAsyncExceptions()));
        }
    }
}
