namespace RogueLoise
{
    public class DrawArgs
    {
        public double GlobalTime;
        public double ElapsedTime;
        private readonly IDrawer _drawer;

        public DrawArgs(IDrawer drawer)
        {
            _drawer = drawer;
        }

        public void Draw(int x, int y, char c)
        {
            _drawer.Draw(x,y,c);
        }

        public void Draw(Vector point, char c)
        {
            _drawer.Draw(point, c);
        }
    }
}