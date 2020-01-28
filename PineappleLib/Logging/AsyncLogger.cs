using System.Threading.Tasks;

namespace PineappleLib.Logging
{
    public class AsyncLogger
    {
        /// <summary>
        /// Waits for 3s and throws and exception if one was thrown elsewhere in that timespan.
        /// </summary>
        public async Task WaitForAsyncExceptions()
        {
            var ex = PineappleLogger.ex;

            await Task.Delay(3000);

            if (ex != PineappleLogger.ex)
                throw PineappleLogger.ex;
        }
    }
}
