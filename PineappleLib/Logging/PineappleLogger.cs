using System;
using PineappleLib.Enums;
using Serilog;
using Serilog.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace PineappleLib.Logging
{
    public static class PineappleLogger
    {
        public static Exception ex { get; private set; }
        private static string path = "C:/Utveckling/ProjectPineapple/PineappleServer/Logger/"; //TODO move to file
        private static string date = DateTime.Now.ToString("yyyy_MM_dd");
        private static string fileName = $"{path}PineappleLog-{date}.log";
        public static readonly string fullPath = Path.Combine(path, fileName);
        private static Serilog.Core.Logger _logger;

        public static void HandleException(Exception _ex, bool doThrow, string msg = null)
        {
            ex = _ex;
            PineappleLog(LogType.FATAL, msg != null ? msg : ex.ToString());

            if (doThrow)
                throw _ex;
        }
        public static void PineappleLog(LogType logType, string msg)
        {
            if (_logger == null)
                Setup();

            switch (logType)
            {
                case LogType.INFO:
                    Log.Information(msg);
                    break;
                case LogType.DEBUG:
                    Log.Debug(msg);
                    break;
                case LogType.WARNING:
                    Log.Warning(msg);
                    break;
                case LogType.ERROR:
                    Log.Error(msg);
                    break;
                case LogType.FATAL:
                    Log.Fatal(msg);
                    break;
                //case LogType.COMBAT:
                //    //TODO
                //    break;
                default:
                    throw new NotImplementedException();
            }
        }
        /// <summary>
        /// Use if you want to read from the log.
        /// Probably only for testing.
        /// </summary>
        public static void CloseLog(int ms = 1000)
        {
            Thread.Sleep(ms);
            PineappleLog(LogType.DEBUG, "Closing and flushing log!");
            Log.CloseAndFlush();
        }
        private static void Setup()
        {
            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Async(a => a
                    .File(fullPath))
                .CreateLogger();

            Log.Logger = _logger;
        }
        public static void CreateDeployLog()
        {
            string date = DateTime.Now.ToString("yyyy_MM_dd");

            var tmpLogger = new LoggerConfiguration()
                .WriteTo.File($"{path}Deploy.log",
                outputTemplate: "{Message:lj}{NewLine}")
                .CreateLogger();

            tmpLogger.Information($"{date} - Deployed");
            tmpLogger.Dispose();
        }
    }
}
