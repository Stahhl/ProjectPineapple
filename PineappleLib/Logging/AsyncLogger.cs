using System.Threading.Tasks;

namespace PineappleLib.Logging
{
    public class AsyncLogger
    {
        /// <summary>
        /// Waits for ms and throws and exception if one was thrown elsewhere in that timespan.
        /// </summary>
        public async Task WaitForAsyncExceptions(int ms = 1000)
        {
            var ex = PineappleLogger.ex;

            await Task.Delay(ms);

            if (ex != PineappleLogger.ex)
                throw PineappleLogger.ex;
        }
    }
}
