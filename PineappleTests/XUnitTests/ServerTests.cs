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
using static PineappleLib.General.Values;
using PineappleLib.Models.Players;
using PineappleLib.Controllers;
using PineappleLib.Enums;

namespace XUnitTests
{
    public class ServerTests
    {
        [Fact]
        public async Task StartServerTest()
        {
            var lg = new AsyncLogger();
            var server = new Server();

            server.Start(00000);

            Assert.Null(await Record.ExceptionAsync(() => lg.WaitForAsyncExceptions()));

            server.Stop();
        }
        [Fact]
        public async Task StartStopServerTest()
        {
            var lg = new AsyncLogger();
            var server = new Server();

            server.Start(00001);

            Thread.Sleep(100);

            server.Stop();

            Assert.Null(await Record.ExceptionAsync(() => lg.WaitForAsyncExceptions()));
        }
        [Fact]
        public async Task ConnectToServerTest()
        {
            var lg = new AsyncLogger();
            var server = new Server();

            server.Start(stdPort);

            var player = new Player(PlayerType.PLAYER);

            var game = new GameController(player);

            game.StartOnline();

            Assert.Null(await Record.ExceptionAsync(() => lg.WaitForConnectionsServer(server, 1)));

            Assert.Equal(player.Name, server.Clients[0].Player.Name);

            server.Stop();
        }
        [Fact]
        public async Task MultipleServerTest_Pass()
        {
            var lg = new AsyncLogger();

            var s1 = new Server();
            var s2 = new Server();
            var s3 = new Server();

            s1.Start(11111);
            s1.Start(22222);
            s1.Start(33333);

            Assert.Null(await Record.ExceptionAsync(() => lg.WaitForAsyncExceptions()));
        }
        [Fact]
        public async Task MultipleServerTest_Fail()
        {
            var lg = new AsyncLogger();

            var t1 = Task.Run(() => new Server().Start(44444));
            var t2 = Task.Run(() => new Server().Start(44444));

            Assert.NotNull(await Record.ExceptionAsync(() => lg.WaitForAsyncExceptions(3000)));
        }
        [Fact]
        public async Task ConnectTwoPlayersTest()
        {
            var lg = new AsyncLogger();
            var server = new Server();

            server.Start(stdPort);

            var player1 = new Player(PlayerType.PLAYER);
            var player2 = new Player(PlayerType.PLAYER);

            var game1 = new GameController(player1);
            var game2 = new GameController(player2);

            game1.StartOnline();
            game2.StartOnline();

            Assert.Null(await Record.ExceptionAsync(() => lg.WaitForConnectionsServer(server, 2)));

            var player1Copy = server.Clients[0].Player;
            var player2Copy = server.Clients[1].Player;

            var e1 = player1.Name;
            var e2 = player2.Name;

            var a1 = server.Clients[0].Player.Name;
            var a2 = server.Clients[1].Player.Name;

            Assert.Equal(e1, a1);
            Assert.Equal(e2, a2);

            server.Stop();
        }
    }
}
