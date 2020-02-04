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
        public const int BaseHealthPoints = 100;
        public const int BaseActionPoints = 12;
        public const int BaseAbilityAmount = 10;

        //Backend
        public const int serverMaxLobbys = 5; //Starts at 0
        public const int serverMaxPlayers = 25; //Starts at 0
        public const int lobbyMaxPlayers = 5; //Starts at 0
        public const int dataBufferSize = 4096; //4MB
        public const int stdPort = 55555;
        public const string stdIp = "127.0.0.1";
        public const string stdPwd = "-_$$_asdF!.1234";
        public const int TICKS_PER_SEC = 30; // How many ticks per second
        public const float MS_PER_TICK = 1000f / TICKS_PER_SEC; // How many milliseconds per tick -> 33.3...
    }
}


