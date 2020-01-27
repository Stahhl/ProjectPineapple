using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PineappleLib.Logging
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

            string path = $"C:/Utveckling/ProjectPineapple/Server/Logger/log_{date}.txt";

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine($"{time} > {cat} > {msg}");
            }
        }
    //    private static bool WaitForFile(string fullPath)
    //    {
    //        int numTries = 0;
    //        while (true)
    //        {
    //            ++numTries;
    //            try
    //            {
    //                // Attempt to open the file exclusively.
    //                using (FileStream fs = new FileStream(fullPath,
    //                    FileMode.Open, FileAccess.ReadWrite,
    //                    FileShare.None, 100))
    //                {
    //                    fs.ReadByte();

    //                    // If we got this far the file is ready
    //                    break;
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                Log.LogWarning(
    //                   "WaitForFile {0} failed to get an exclusive lock: {1}",
    //                    fullPath, ex.ToString());

    //                if (numTries > 10)
    //                {
    //                    Log.LogWarning(
    //                        "WaitForFile {0} giving up after 10 tries",
    //                        fullPath);
    //                    return false;
    //                }

    //                // Wait for the lock to be released
    //                System.Threading.Thread.Sleep(500);
    //            }
    //        }

    //        Log.LogTrace("WaitForFile {0} returning true after {1} tries",
    //            fullPath, numTries);
    //        return true;
    //    }
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
