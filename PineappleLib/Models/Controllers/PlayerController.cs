using PineappleLib.Models.Players;
using PineappleLib.Networking.Clients;
using PineappleLib.Serialization;
using PineappleLib.Logging;
using PineappleLib.General.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using PineappleLib.Networking;

namespace PineappleLib.Models.Controllers
{
    public class PlayerController
    {
        public PlayerController()
        {
            Player = new Player(this);
            Serializer = new PineappleSerializer();
            ThreadManager = new ThreadManager();
        }

        public Player Player { get; private set; }
        public Client Client { get; private set; }
        public PineappleSerializer Serializer { get; private set; }
        public ThreadManager ThreadManager { get; private set; }

        public void GoOnline()
        {
            Thread.Sleep(50);

            if (Client != null)
                PineappleLogger.HandleException(new SingletonException(), true);

            Client = new Client(this);
        }

    }
}
