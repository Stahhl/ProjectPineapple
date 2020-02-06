using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PineappleLib.Logging.PineappleLogger;
using PineappleLib.Enums;
using System.IO;

namespace PineappleLib.Logging
{
    public class WaitForFile
    {
        public async Task<bool> Wait()
        {
            int tries = 1;

            while (true)
            {
                try
                {
                    using (FileStream fs = new FileStream(PineappleLogger.fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 100))
                    {
                        fs.ReadByte();

                        // If we got this far the file is ready
                        break;
                    }
                }
                catch
                {
                    if (tries > 10)
                    {
                        Log(LogType.ERROR, $"WaitForFile - Giving up after {tries} tries");

                        return false;
                    }

                    Log(LogType.WARNING, $"WaitForFile - Failed to get a lock after {tries} tries");
                    await Task.Delay(333);
                }
                finally
                {
                    tries++;
                }
            }

            Log(LogType.INFO, $"WaitForFile - Returning true after {tries} tries");
            return true;
        }
    }
}
