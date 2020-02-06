using PineappleLib.Logging;
using PineappleLib.Networking.Servers;
using static PineappleLib.General.Values;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using PineappleLib.Models.Players;
using PineappleLib.Enums;
using PineappleLib.Controllers;
using System.Threading.Tasks;

namespace XUnitTests
{
    public class LobbyTests
    {
        [Fact]
        public async Task CreateLobbyTest()
        {
            var lg = new AsyncLogger();
            var server = new Server();

            server.Start(stdPort);

            var player = new Player(PlayerType.PLAYER);

            var game = new GameController(player);

            game.StartOnline();

            game.Client.ClientSender.CreateLobby(stdPwd, false);

            Assert.Null(await Record.ExceptionAsync(() => lg.WaitForLobbys(server, 1)));
            Assert.Equal(server.Lobbys[0].Password, stdPwd);

            server.Stop();
        }
        [Fact]
        public async Task CreateJoinLobbyTest()
        {
            var lg = new AsyncLogger();
            var server = new Server();

            server.Start(stdPort);

            var player = new Player(PlayerType.PLAYER);

            var game = new GameController(player);

            game.StartOnline();

            game.Client.ClientSender.CreateLobby(stdPwd, true);

            Assert.Null(await Record.ExceptionAsync(() => lg.WaitForLobbys(server, 1)));
            Assert.Null(await Record.ExceptionAsync(() => lg.WaitForClientsInLobbys(server, new Dictionary<int, int> { { 0, 1 } })));
            Assert.Equal(server.Lobbys[0].Password, stdPwd);

            server.Stop();
        }
        [Fact]
        public async Task TwoClientsOneLobbyTest()
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

            game1.Client.ClientSender.CreateLobby(stdPwd, true);
            game2.Client.ClientSender.JoinLobby(stdPwd);

            Assert.Null(await Record.ExceptionAsync(() => lg.WaitForLobbys(server, 1)));
            Assert.Null(await Record.ExceptionAsync(() => lg.WaitForClientsInLobbys(server, new Dictionary<int, int> { { 0, 2 } })));
            Assert.Equal(server.Lobbys[0].Password, stdPwd);

            server.Stop();
        }
        [Fact]
        public async Task ThreeClientsTwoLobbysTest()
        {
            var lg = new AsyncLogger();
            var server = new Server();

            server.Start(stdPort);

            var player1 = new Player(PlayerType.PLAYER);
            var player2 = new Player(PlayerType.PLAYER);
            var player3 = new Player(PlayerType.PLAYER);

            var game1 = new GameController(player1);
            var game2 = new GameController(player2);
            var game3 = new GameController(player3);

            game1.StartOnline();
            game2.StartOnline();
            game3.StartOnline();

            game1.Client.ClientSender.CreateLobby("111", true);
            game2.Client.ClientSender.CreateLobby("222", true);
            game3.Client.ClientSender.JoinLobby("111");

            //Assert.Null(await Record.ExceptionAsync(() => lg.WaitForLobbys(server, 1)));
            Assert.Null(await Record.ExceptionAsync(() => lg.WaitForClientsInLobbys(server, new Dictionary<int, int> { { 0, 2 }, { 1, 1 } })));
            Assert.Equal("111", server.Lobbys[0].Password);
            Assert.Equal("222", server.Lobbys[1].Password);

            server.Stop();
        }
    }
}
