using System;

namespace PineappleLib.General.Data
{
    /// <summary>
    /// Static readonly values
    /// </summary>
    //TODO move to a file
    public static class Values
    {
        //Game
        public static readonly int BaseHealthPoints = 100;
        public static readonly int BaseActionPoints = 12;

        //Backend
        public static readonly int dataBufferSize = 4096; //4MB
        public static readonly int stdPort = 55555;
        public static readonly string stdIp = "127.0.0.1";

    }
}


