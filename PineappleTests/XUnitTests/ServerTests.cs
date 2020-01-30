using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using PineappleLib.Networking;
using PineappleLib.Networking.Clients;
using PineappleLib.Networking.Servers;
using System.Threading.Tasks;
using System.Threading;
using PineappleLib.Logging;
using static PineappleLib.General.Data.Values;
using PineappleLib.Models.Players;

namespace XUnitTests
{
    public class ServerTests
    {
        [Fact]
        public async Task StartServerTest()
        {
            var lg = new AsyncLogger();
            var server = new Server(00000);

            Assert.Null(await Record.ExceptionAsync(() => lg.WaitForAsyncExceptions()));
        }
        [Fact]
        public async Task StartStopServerTest()
        {
            var lg = new AsyncLogger();
            var server = new Server(00001);

            Thread.Sleep(100);

            server.Stop();

            Assert.Null(await Record.ExceptionAsync(() => lg.WaitForAsyncExceptions()));
        }
        [Fact]
        public async Task ConnectToServerTest()
        {
            var lg = new AsyncLogger();
            var server = new Server(stdPort);

            new Client(new Player());

            Assert.Null(await Record.ExceptionAsync(() => lg.WaitForAsyncExceptions()));
        }
        [Fact]
        public async Task MultipleServerTest_Pass()
        {
            var lg = new AsyncLogger();

            var s1 = new Server(11111);
            var s2 = new Server(22222);
            var s3 = new Server(33333);

            Assert.Null(await Record.ExceptionAsync(() => lg.WaitForAsyncExceptions()));
        }
        [Fact]
        public async Task MultipleServerTest_Fail()
        {
            var lg = new AsyncLogger();

            var t1 = Task.Run(() => new Server(44444));
            var t2 = Task.Run(() => new Server(44444));

            Assert.NotNull(await Record.ExceptionAsync(() => lg.WaitForAsyncExceptions()));
        }
    }
}
