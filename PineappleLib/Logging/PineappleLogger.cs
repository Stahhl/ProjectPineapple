﻿using System;
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

        public static void HandleException(Exception _ex, bool doThrow, string msg = "")
        {
            ex = _ex;
            msg = msg != "" ? msg + ": " + _ex.ToString() : _ex.ToString();
            Log(LogType.FATAL, msg + " : " + _ex.Message);

            throw _ex;
        }
        public static void Log(LogType logType, string msg)
        {
            if (_logger == null)
                Setup();

            switch (logType)
            {
                case LogType.INFO:
                    Serilog.Log.Information(msg);
                    break;
                case LogType.DEBUG:
                    Serilog.Log.Debug(msg);
                    break;
                case LogType.WARNING:
                    Serilog.Log.Warning(msg);
                    break;
                case LogType.ERROR:
                    Serilog.Log.Error(msg);
                    break;
                case LogType.FATAL:
                    Serilog.Log.Fatal(msg);
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
            Log(LogType.DEBUG, "Closing and flushing log!");
            Serilog.Log.CloseAndFlush();
        }
        private static void Setup()
        {
            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Async(a => a
                    .File(fullPath))
                .CreateLogger();

            Serilog.Log.Logger = _logger;
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
