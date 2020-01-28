using Xunit;
using PineappleLib.Logging;
using static PineappleLib.Logging.PineappleLogger;
using PineappleLib.Enums;
using System;
using System.IO;
using System.Threading;

namespace XUnitTests
{
    public class LogTests
    {
        [Fact]
        public void WriteTest()
        {
            PineappleLog(LogType.INFO, "Can write test");
        }
        [Fact]
        public void WriteReadTest()
        {
            string date = DateTime.Now.ToString("yyyy_MM_dd");
            string path = Path.Combine("C:/Utveckling/ProjectPineapple/PineappleServer/Logger", "Deploy.log");

            if (File.Exists(path))
                File.Delete(path);

            Assert.False(File.Exists(path));

            PineappleLogger.CreateDeployLog();

            Assert.True(File.Exists(path));

            string content = File.ReadAllText(path);

            Assert.Equal($"{date} - Deployed\r\n", content);
        }
        [Fact]
        public void MassWriteTest()
        {
            var d = DateTime.Now.ToString();

            string date = DateTime.Now.ToString("yyyy_MM_dd");
            string path = Path.Combine("C:/Utveckling/ProjectPineapple/PineappleServer/Logger", $"PineappleLog-{date}.log");

            for (int i = 0; i < 100; i++)
            {
                PineappleLog(LogType.INFO, $"MassWriteTest - Write {i} - {d}");
            }

            PineappleLogger.CloseLog();

            Assert.True(File.Exists(path));
            string content = File.ReadAllText(path);

            for (int i = 0; i < 100; i++)
            {
                Assert.Contains($"MassWriteTest - Write {i} - {d}", content);
            }
        }
    }
}
