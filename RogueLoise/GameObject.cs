namespace RogueLoise
{
    public class GameObject
    {
        protected GameObject() {}

        public Vector Position { get; set; }

        public int X
        {
            get
            {
                return Position.X;
            }
            set
            {
                Position = new Vector(value, Position.Y);
            }
        }

        public int Y
        {
            get
            {
                return Position.Y;
            }
            set
            {
                Position = new Vector(Position.X, value);
            }
        }

        public string Name { get; set; }

        public Game Game { get; set; }

        public virtual void Update(UpdateArgs args)
        {
            
        }
    }
}