using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PineappleLib.General.Data
{
    public static class PineappleRandom
    {
        private static Random random = new Random();

        public static int GetRandomOfDigits(int digits)
        {
            string minStr = "1";
            string maxStr = "";

            for (int i = 0; i < digits - 1; i++)
            {
                minStr += "0";
            }
            for (int i = 0; i < digits; i++)
            {
                maxStr += "9";
            }

            return random.Next(int.Parse(minStr), int.Parse(maxStr) + 1);
        }
    }
}
