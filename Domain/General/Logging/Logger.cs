using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.General.Logging
{
    public static class Logger
    {
        public static Exception ex { get; private set; }

        public static void HandleException(Exception _ex, bool doThrow, string msg = null)
        {
            ex = _ex;
            Log(msg != null ? msg : ex.ToString(), true);

            if (doThrow)
                throw _ex;
        }
        public static void Log(string msg, bool isError)
        {
            string date = DateTime.Now.ToString("yyyy_MM_dd");
            string time = DateTime.Now.ToString("HH:mm:ss.fff");
            string cat = isError == true ? "ERROR" : "DEBUG";

            string path = $"C:/Utveckling/ProjectPineapple/Server/Logs/log_{date}.txt";

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine($"{time} > {cat} > {msg}");
            }
        }
    }
    public class AsyncLogger
    {
        /// <summary>
        /// Waits for 3s and returns an exception if one was throws in that timespan.
        /// </summary>
        public async Task WaitForAsyncExceptions()
        {
            var ex = Logger.ex;

            await Task.Delay(3000);

            if (ex != Logger.ex)
                throw Logger.ex;
        }
    }
}
