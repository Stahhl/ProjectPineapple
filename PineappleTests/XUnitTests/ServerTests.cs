using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using PineappleLib.Networking;
using PineappleLib.Networking.Clients;
using PineappleLib.Networking.Server;
using System.Threading.Tasks;
using System.Threading;
using PineappleLib.Logging;
using static PineappleLib.General.Data.Values;

namespace XUnitTests
{
    public class ServerTests
    {
        [Fact]
        public void StartServerTest()
        {
            var lg = new AsyncLogger();
            var server = new Server(stdPort);

            var t2 = Task.Run(() => lg.WaitForAsyncExceptions());

            t2.Wait();
        }
        [Fact]
        public async Task ConnectToServerTest()
        {
            var server = new Server(55555);

            new Client();

            Thread.Sleep(10);

            Assert.Null(await Record.ExceptionAsync(() => new AsyncLogger().WaitForAsyncExceptions()));
        }
        [Fact]
        public async Task MultipleServerTest_Pass()
        {
            var s1 = new Server(11111);
            var s2 = new Server(22222);
            var s3 = new Server(33333);

            Assert.Null(await Record.ExceptionAsync(() => new AsyncLogger().WaitForAsyncExceptions()));
        }
        [Fact]
        public async Task MultipleServerTest_Fail()
        {
            var t1 = Task.Run(() => new Server(44444));
            var t2 = Task.Run(() => new Server(44444));

            Assert.NotNull(await Record.ExceptionAsync(() => new AsyncLogger().WaitForAsyncExceptions()));
        }
    }
}
