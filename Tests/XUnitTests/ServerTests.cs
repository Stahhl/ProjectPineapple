using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Domain.Networking;
using System.Threading.Tasks;
using System.Threading;
using Domain.General.Logging;

namespace XUnitTests
{
    public class ServerTests
    {
        [Fact]
        public void StartServerTest()
        {
            Task task = Task.Run(() => new Server().Start(5, 55555));

            task.Wait();
        }
        [Fact]
        public async Task ConnectToServerTest()
        {
            new Server().Start(5, 55555);

            new ClientLocal().Init();

            Assert.Null(await Record.ExceptionAsync(() => new AsyncLogger().WaitForAsyncExceptions()));
        }
    }
}
