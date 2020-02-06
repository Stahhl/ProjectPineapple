using Xunit;
using PineappleLib.Logging;
using static PineappleLib.Logging.PineappleLogger;
using PineappleLib.Enums;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace XUnitTests
{
    public class LogTests
    {
        [Fact]
        public void WriteTest()
        {
            Log(LogType.INFO, "Can write test");
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

            string content = PineappleReader.Read(path);

            Assert.Equal($"{date} - Deployed", content);
        }
        [Fact]
        public void MassWriteTest()
        {
            var id = DateTime.Now;

            for (int i = 0; i < 100; i++)
            {
                Log(LogType.INFO, $"MassWriteTest - Write {i} - {id}");
            }

            Assert.True(File.Exists(PineappleLogger.fullPath), "File.Exists");

            string content = PineappleReader.Read(PineappleLogger.fullPath);

            for (int i = 0; i < 100; i++)
            {
                Assert.Contains($"MassWriteTest - Write {i} - {id}", content);
            }
        }
    }
}
