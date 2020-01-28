using System;
using PineappleLib.Enums;
using Serilog;
using Serilog.Configuration;
using System.Threading;

namespace PineappleLib.Logging
{
    public static class PineappleLogger
    {
        public static Exception ex { get; private set; }
        private static string path = "C:/Utveckling/ProjectPineapple/PineappleServer/Logger/";
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
                    throw new Exception($"Wrong LogType: {logType.ToString()}");
            }
        }
        public static void CloseLog()
        {
            Log.CloseAndFlush();
            Thread.Sleep(100);
        }
        private static void Setup()
        {
            string date = DateTime.Now.ToString("yyyy_MM_dd");

            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Async(a => a
                    .File($"{path}PineappleLog-{date}.log"))
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
