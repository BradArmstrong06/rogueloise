using System;

namespace RogueLoise
{
    public class DrawArgs
    {
        private readonly IDrawer _drawer;
        public Vector CameraPositionAtMap;
        public double ElapsedTime;
        public double GlobalTime;

        public DrawArgs(IDrawer drawer)
        {
            _drawer = drawer;
        }

        public void DrawAtAbsolutePoint(int x, int y, char c, ConsoleColor color = ConsoleColor.White)
        {
            _drawer.DrawAtAbsolutePoint(x, y, c, color);
        }

        public void DrawAtAbsolutePoint(Vector point, char c, ConsoleColor color = ConsoleColor.White)
        {
            _drawer.DrawAtAbsolutePoint(point, c, color);
        }

        /// <summary>
        /// </summary>
        /// <param name="x">X position at map</param>
        /// <param name="y">Y position at map</param>
        /// <param name="c">Tile</param>
        /// <param name="color"></param>
        public void DrawInGameZone(int x, int y, char c, ConsoleColor color = ConsoleColor.White)
        {
            DrawInGameZone(new Vector(x, y), c, color);
        }

        /// <summary>
        /// </summary>
        /// <param name="point">Position at map</param>
        /// <param name="c">Tile</param>
        /// <param name="color"></param>
        public void DrawInGameZone(Vector point, char c, ConsoleColor color = ConsoleColor.White)
        {
            _drawer.DrawInGameZone(point - CameraPositionAtMap, c, color);
        }

        public void DrawInGameZone(DrawableGameObject obj)
        {
            DrawInGameZone(obj.Position, obj.Tile, obj.Color);
        }
    }
}