using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using PineappleLib.Networking;
using System.Threading.Tasks;
using System.Threading;
using PineappleLib.Logging;

namespace XUnitTests
{
    public class ServerTests
    {
        [Fact]
        public void StartServerTest()
        {
            var server = new Server();
            Task task = Task.Run(() => server.Start(5, 55555));

            task.Wait();
            server.Stop();
        }
        [Fact]
        public async Task ConnectToServerTest()
        {
            var server = new Server();

            new ClientLocal().Init();

            Assert.Null(await Record.ExceptionAsync(() => new AsyncLogger().WaitForAsyncExceptions()));

            server.Stop();
        }
    }
}
