using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PineappleLib.Logging;
using System.Threading;
using PineappleLib.Enums;
using static PineappleLib.Logging.PineappleLogger;
using static PineappleLib.General.Values;
using PineappleLib.Networking.Servers;

namespace PineappleLib.Networking.Loopers
{
    public abstract class _Looper
    {
        protected string type;

        public bool IsRunning { get; private set; }

        private Task loop;

        public virtual async void Start()
        {
            if (IsRunning == true)
                HandleException(new Exception("_Looper - Start()"), true, $"Trying to start a {type} that is already running!");

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
                Log(LogType.WARNING, $"Stopping {type}");
            }
        }

        public virtual void Stop()
        {
            if (IsRunning == false)
                HandleException(new Exception("_Looper - Stop()"), true, $"Trying to stop a {type} that is already not running");


            IsRunning = false;
        }

        public virtual void Loop()
        {
            //Log(LogType.INFO, $"Starting {type} Loop");
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

        public virtual void Update()
        {
            throw new NotImplementedException();
        }
    }
}
