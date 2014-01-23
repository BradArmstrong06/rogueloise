using System;

namespace RogueLoise
{
    public class DrawableGameObject : GameObject
    {
        private char[] _tiles = new char[1];
        private short _frameIndex;

        public DrawableGameObject() :
            base()
        {
        }

        public char Tile
        {
            get { return IsAnimuted && _tiles.Length > 0 && _frameIndex < _tiles.Length ? _tiles[_frameIndex] : _tiles.Length > 0 ? _tiles[0] : ' '; }
            set
            {
                if (_tiles.Length > 0)
                    _tiles[0] = value;
                else
                    _tiles = new[] {value};
            }
        }

        public bool IsVisible;

        public char[] Frames
        {
            get { return _tiles; } 
            set { _tiles = value; }
        }

        public Vector VisualPoint
        {
            get
            {
                return new Vector {X = VisualX, Y = VisualY};
            }
        }

        public int VisualX
        {
            get { return X + Offset.X; }
        }

        public int VisualY
        {
            get { return Y + Offset.Y; }
        }

        public Vector Offset;

        public bool IsAnimuted { get; set; }

        public bool IsAnimationStopped { get; set; }

        public virtual void Draw(DrawArgs args)
        {
            if(!IsVisible)
                return;

            if (IsAnimuted)
            {
                throw new NotImplementedException();
            }
            else
            {
                args.Draw(VisualPoint, Tile);
            }
        }
    }
}