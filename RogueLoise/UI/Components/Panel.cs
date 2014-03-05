using System;

namespace RogueLoise.UI.Components
{
    public class Panel : UIElement
    {
        private char _leftBottomCorner;
        private char _leftTopCorner;
        private char _rightBottomCorner;
        private char _rightTopCorner;
        private char _sideBorders;
        private char _topBottonBorder;
        private string _borderTiles;

        public Panel(Game game) : base(game)
        {}

        public string BorderTiles
        {
            get { return _borderTiles; }
            set
            {
                _borderTiles = value;
                if (_borderTiles.Length != 6)
                    throw new Exception(); //todo

                _sideBorders = _borderTiles[0];
                _topBottonBorder = _borderTiles[1];
                _leftTopCorner = _borderTiles[2];
                _rightTopCorner = _borderTiles[3];
                _leftBottomCorner = _borderTiles[4];
                _rightBottomCorner = _borderTiles[5];
            }
        }

        private void DrawBorders(DrawArgs args)
        {
            Vector topLeftCorner = AbsolutePosition;
            Vector bottomRightCorner = AbsolutePosition + Size;

            for (int y = topLeftCorner.Y; y <= bottomRightCorner.Y; y++)
            {
                if (y == topLeftCorner.Y || y == bottomRightCorner.Y)
                {
                    for (int x = topLeftCorner.X; x <= bottomRightCorner.X; x++)
                    {
                        var point = new Vector(x, y);

                        if (x == topLeftCorner.X)
                        {
                            if (y == topLeftCorner.Y)
                                args.DrawAtAbsolutePoint(point, _leftTopCorner);
                            if (y == bottomRightCorner.Y)
                                args.DrawAtAbsolutePoint(point, _leftBottomCorner);
                        }
                        else if (x == bottomRightCorner.X)
                        {
                            if (y == topLeftCorner.Y)
                                args.DrawAtAbsolutePoint(point, _rightTopCorner);
                            if (y == bottomRightCorner.Y)
                                args.DrawAtAbsolutePoint(point, _rightBottomCorner);
                        }
                        else
                            args.DrawAtAbsolutePoint(point, _topBottonBorder);
                    }
                }
                else
                {
                    args.DrawAtAbsolutePoint(topLeftCorner.X, y, _sideBorders);
                    args.DrawAtAbsolutePoint(bottomRightCorner.X, y, _sideBorders);
                }
            }
        }

        protected override void DoDraw(DrawArgs args)
        {
            DrawBorders(args);
        }
    }
}