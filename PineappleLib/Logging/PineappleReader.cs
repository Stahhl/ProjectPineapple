using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PineappleLib.Logging
{
    public static class PineappleReader
    {
        public static string Read(string path)
        {
            Thread.Sleep(500);

            string content = "";

            using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        content += reader.ReadLine();
                    }
                }
            }

            return content;
        }
    }
}
