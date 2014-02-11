namespace RogueLoise
{
    public class DrawArgs
    {
        public double GlobalTime;
        public double ElapsedTime;
        private readonly IDrawer _drawer;
        public Vector CameraPositionAtMap;

        public DrawArgs(IDrawer drawer)
        {
            _drawer = drawer;
        }

        public void DrawAtAbsolutePoint(int x, int y, char c)
        {
            _drawer.DrawAtAbsolutePoint(x,y,c);
        }

        public void DrawAtAbsolutePoint(Vector point, char c)
        {
            _drawer.DrawAtAbsolutePoint(point, c);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">X position at map</param>
        /// <param name="y">Y position at map</param>
        /// <param name="c">Tile</param>
        public void DrawInGameZone(int x, int y, char c)
        {
            DrawInGameZone(new Vector(x, y), c);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point">Position at map</param>
        /// <param name="c">Tile</param>
        public void DrawInGameZone(Vector point, char c)
        {
            _drawer.DrawInGameZone(point - CameraPositionAtMap, c);
        }

        public void DrawInGameZone(DrawableGameObject obj)
        {
            DrawInGameZone(obj.Position, obj.Tile);
        }
    }
}