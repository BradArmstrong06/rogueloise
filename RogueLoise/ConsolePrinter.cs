namespace RogueLoise
{
    public interface IDrawer
    {
        void Draw(int x, int y, char c);

        void Draw(Vector point, char c);

        void Flush();
    }

    public class ConsolePrinter : IDrawer
    {
        public void Draw(int x, int y, char c)
        {
            throw new System.NotImplementedException();
        }

        public void Draw(Vector point, char c)
        {
            throw new System.NotImplementedException();
        }

        public void Flush()
        {
            throw new System.NotImplementedException();
        }
    }
}