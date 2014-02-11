using System;

namespace RogueLoise
{
    public struct UpdateArgs
    {
        public double GlobalTime;
        public double GameTime;
        public double ElapsedTime;
        public bool IsGamePaused;
        public ConsoleKey Key;
    }
}