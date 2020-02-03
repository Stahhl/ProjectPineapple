using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PineappleLib.General.Helpers
{
    public static class Disposer
    {
        public static void NullMe(object obj)
        {
            obj = null;
        }
    }
}
