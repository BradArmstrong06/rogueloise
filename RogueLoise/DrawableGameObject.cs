using System;

namespace RogueLoise
{
    public class DrawableGameObject : GameObject
    {
        public bool IsVisible;
        public Vector Offset;
        private short _frameIndex;
        private char[] _tiles = new char[1];

        public DrawableGameObject(Game game) :
            base(game)
        {
            IsVisible = true;
        }

        public char Tile
        {
            get
            {
                return IsAnimuted && _tiles.Length > 0 && _frameIndex < _tiles.Length
                    ? _tiles[_frameIndex]
                    : _tiles.Length > 0 ? _tiles[0] : ' ';
            }
            set
            {
                if (_tiles.Length > 0)
                    _tiles[0] = value;
                else
                    _tiles = new[] {value};
            }
        }

        public char[] Frames
        {
            get { return _tiles; }
            set { _tiles = value; }
        }

        public Vector VisualPoint
        {
            get { return new Vector {X = VisualX, Y = VisualY}; }
        }

        public int VisualX
        {
            get { return X + Offset.X; }
        }

        public int VisualY
        {
            get { return Y + Offset.Y; }
        }

        public bool IsAnimuted { get; set; }

        public bool IsAnimationStopped { get; set; }

        public virtual void Draw(DrawArgs args)
        {
            if (!IsVisible)
                return;

            if (IsAnimuted)
            {
                throw new NotImplementedException();
            }
            args.DrawAtAbsolutePoint(VisualPoint, Tile);
        }

        public override GameObject Clone()
        {
            var clone = new DrawableGameObject(Game);
            SetClone(clone);
            return clone;
        }

        protected override void SetClone(GameObject obj)
        {
            base.SetClone(obj);
            var drawable = (DrawableGameObject) obj;
            drawable.Frames = (char[]) Frames.Clone();
            drawable.IsAnimationStopped = IsAnimationStopped;
            drawable.IsAnimuted = IsAnimuted;
            drawable.IsVisible = IsVisible;
            drawable.Offset = Offset;
        }
    }
}