using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PineappleLib.Logging;
using PineappleLib.General.Exceptions;
using System.Threading;
using PineappleLib.Enums;
using static PineappleLib.Logging.PineappleLogger;
using static PineappleLib.General.Data.Values;

namespace PineappleLib.Networking.Servers
{
    public class ServerLogic
    {
        public ServerLogic(Server server)
        {
            this.server = server;

            Start();
        }

        public bool IsRunning { get; private set; }

        private Server server;
        private Task loop;

        private async void Start()
        {
            if (IsRunning == true)
                HandleException(new ServerException(), true, "Trying to start server that is already running!");

            IsRunning = true;

            try
            {
                loop = Task.Run(() => Loop());

                await loop;
            }
            catch (Exception ex)
            {
                HandleException(ex, true);
            }
            finally
            {
                //CLEANUP
                PineappleLog(LogType.WARNING, "Stopping Server!");
            }
        }

        internal void Stop()
        {
            if (IsRunning == false)
                HandleException(new ServerException(), true, "Trying to stop a server that is already not running");


            IsRunning = false;
        }

        private void Loop()
        {
            PineappleLog(LogType.INFO, "Starting Server Loop");
            var nextLoop = DateTime.Now;

            while(IsRunning)
            {
                while(nextLoop < DateTime.Now)
                {
                    Update();

                    nextLoop = nextLoop.AddMilliseconds(MS_PER_TICK);

                    if (nextLoop > DateTime.Now)
                    {
                        Thread.Sleep(nextLoop - DateTime.Now);
                    }
                }
            }
        }

        private void Update()
        {
            ThreadManager.UpdateMain();
        }
    }
}
