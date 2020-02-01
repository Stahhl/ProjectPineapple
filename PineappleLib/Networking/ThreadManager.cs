using PineappleLib.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PineappleLib.Enums;

namespace PineappleLib.Networking
{
    public class ThreadManager
    {
        public ThreadManager()
        {
            executeOnMainThread = new List<Action>();
            executeCopiedOnMainThread = new List<Action>();
        }
        private List<Action> executeOnMainThread;
        private List<Action> executeCopiedOnMainThread;
        private static bool actionToExecuteOnMainThread = false;

        /// <summary>Sets an action to be executed on the main thread.</summary>
        /// <param name="_action">The action to be executed on the main thread.</param>
        public void ExecuteOnMainThread(Action _action)
        {
            if (_action == null)
            {
                PineappleLogger.PineappleLog(LogType.DEBUG, "ExecuteOnMainThread - Null");
                return;
            }

            lock (executeOnMainThread)
            {
                executeOnMainThread.Add(_action);
                actionToExecuteOnMainThread = true;
            }
        }

        /// <summary>Executes all code meant to run on the main thread. NOTE: Call this ONLY from the main thread.</summary>
        public void Update()
        {
            if (actionToExecuteOnMainThread)
            {
                executeCopiedOnMainThread.Clear();
                lock (executeOnMainThread)
                {
                    executeCopiedOnMainThread.AddRange(executeOnMainThread);
                    executeOnMainThread.Clear();
                    actionToExecuteOnMainThread = false;
                }

                for (int i = 0; i < executeCopiedOnMainThread.Count; i++)
                {
                    executeCopiedOnMainThread[i]();
                }
            }
        }
    }
}
