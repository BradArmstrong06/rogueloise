using System;

namespace RogueLoise
{
    public struct UpdateArgs
    {
        public double GlobalTime;
        public double GameTime;
        public double ElapsedGlobalTime;
        public double ElapsedGameTime;
        public bool IsGamePaused;
        public ConsoleKey Key;
    }
}