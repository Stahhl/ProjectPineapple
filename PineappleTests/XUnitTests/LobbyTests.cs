using PineappleLib.Logging;
using PineappleLib.Networking.Servers;
using static PineappleLib.General.Data.Values;
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
        }
    }
}
