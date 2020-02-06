using PineappleLib.Controllers;
using PineappleLib.Enums;
using PineappleLib.Models.Players;
using PineappleLib.Models.Units;
using static PineappleLib.General.Values;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using PineappleLib.Logging;
using PineappleLib.Networking.Servers;
using System.Threading.Tasks;

namespace XUnitTests
{
    public class CombatTests
    {
        [Fact]
        public void UnitAdjustHpTest01()
        {
            var p1 = new Unit();
            var p2 = new Unit();

            int amount = 5;

            int e1 = p1.HealthPoints - amount;
            int e2 = p2.HealthPoints + amount;

            p1.AdjustHealth(amount, true);
            p2.AdjustHealth(amount, false);

            Assert.Equal(e1, p1.HealthPoints);
            Assert.Equal(e2, p2.HealthPoints);
        }
        [Fact]
        public void SlapLocallyTest()
        {
            var game = new GameController(new Player(PlayerType.PLAYER));

            game.StartOffline();

            var attacker = game.Players[0].Units[0];
            var defender = game.Enemies[0].Player.Units[0];

            int expected = BaseHealthPoints - BaseAbilityAmount;

            game.CombatController.CombatCalcRedirect(attacker, defender, attacker.Abilities[0]);

            Assert.Equal(expected, defender.HealthPoints);
        }
        [Fact]
        public async Task SlapOnlineTest()
        {
            var rnd = new Random();
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

            if (server.Clients[0] == null)
            {

            }

            Assert.Null(await Record.ExceptionAsync(() => lg.WaitForLobbys(server, 1)));
            Assert.Null(await Record.ExceptionAsync(() => lg.WaitForClientsInLobbys(server, new Dictionary<int, int> { { 0, 2 } })));
            Exception x = await Record.ExceptionAsync(() => lg.WaitForClientValues(server));
            //Assert.Null(x);
            if (x != null)
            {
                //while (x != null)
                //{
                //    x = await Record.ExceptionAsync(() => lg.WaitForClientValues(server));
                //}
            }
        }

        //var attacker = game1.Players[0].Units[0];
        //var defender = game2.Players[0].Units[0];

        //game1.CombatController.CombatCalcRedirect(attacker, defender, attacker.Abilities[0]);
        //string id = PacketType.CombactCalc + "_" + rnd.Next(1, 1000)+ "_" + DateTime.Now;
    }
}
