using System;

namespace RogueLoise
{
    public struct UpdateArgs
    {
        public double ElapsedGameTime;
        public double ElapsedGlobalTime;
        public double GameTime;
        public double GlobalTime;
        public bool IsGamePaused;
        public ConsoleKey Key;
    }
}