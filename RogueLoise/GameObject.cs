namespace RogueLoise
{
    public class GameObject
    {
        protected GameObject(Game game)
        {
            Game = game;
        }

        public Vector Position { get; set; }

        public int X
        {
            get { return Position.X; }
            set { Position = new Vector(value, Position.Y); }
        }

        public int Y
        {
            get { return Position.Y; }
            set { Position = new Vector(Position.X, value); }
        }

        public string Key { get; set; }

        public string Name { get; set; }

        protected readonly Game Game;

        public bool IsEnabled { get; set; }

        public bool Updated { get; set; }

        public virtual void Update(UpdateArgs args)
        {
            if (Updated)
                return;

            Updated = true;
        }

        public virtual GameObject Clone()
        {
            var clone = new GameObject(Game);
            SetClone(clone);
            return clone;
        }

        protected virtual void SetClone(GameObject obj)
        {
            obj.Position = Position;
            obj.Key = Key;
            obj.Name = Name;
            obj.IsEnabled = IsEnabled;
        }

        public void ResetUpdate()
        {
            Updated = false;
        }
    }
}