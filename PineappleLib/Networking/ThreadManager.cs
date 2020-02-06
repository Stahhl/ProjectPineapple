using PineappleLib.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PineappleLib.Enums;
using System.Net.Sockets;
using PineappleLib.Networking.Servers;
using PineappleLib.Networking.Clients;
using static PineappleLib.General.Values;

namespace PineappleLib.Networking
{
    public class ThreadManager
    {
        public static Server Server;

        private static readonly Queue<TcpClient> clientQueue = new Queue<TcpClient>();

        private static readonly List<Action> executeOnMainThread = new List<Action>();
        private static readonly List<Action> executeCopiedOnMainThread = new List<Action>();
        private static bool actionToExecuteOnMainThread = false;

        public static void Stop()
        {
            actionToExecuteOnMainThread = false;
            Server = null;

            lock (clientQueue)
                clientQueue.Clear();

            lock (executeOnMainThread)
                executeOnMainThread.Clear();

            lock (executeCopiedOnMainThread)
                executeCopiedOnMainThread.Clear();
        }
        public static void QueueClient(TcpClient client)
        {
            if (client == null)
            {
                PineappleLogger.Log(LogType.DEBUG, "ClientConnectQueue - Null");
                return;
            }

            lock (clientQueue)
            {
                clientQueue.Enqueue(client);
            }
        }
        /// <summary>Sets an action to be executed on the main thread.</summary>
        /// <param name="_action">The action to be executed on the main thread.</param>
        public static void ExecuteOnMainThread(Action _action)
        {
            if (_action == null)
            {
                PineappleLogger.Log(LogType.DEBUG, "ExecuteOnMainThread - Null");
                return;
            }

            lock (executeOnMainThread)
            {
                executeOnMainThread.Add(_action);
                actionToExecuteOnMainThread = true;
            }
        }

        /// <summary>Executes all code meant to run on the main thread. NOTE: Call this ONLY from the main thread.</summary>
        public static void UpdateActions()
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
        public static void AddQueuedClients()
        {
            if (clientQueue.Count <= 0)
                return;

            lock (clientQueue)
            {
                var client = clientQueue.Dequeue();

                for (int i = 0; i <= serverMaxPlayers; i++)
                {
                    if (Server.Clients[i] == null)
                    {
                        Server.Clients[i] = new Client(Server, i);
                        Server.Clients[i].Tcp.ConnectServerToClient(i, client);
                        break;
                    }
                }
            }
        }
    }
}
