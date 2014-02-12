using System;

namespace RogueLoise
{
    public class UI
    {
        private readonly char _leftBottomCorner;
        private readonly char _leftTopCorner;
        private readonly char _rightBottomCorner;
        private readonly char _rightTopCorner;
        private readonly char _sideBorders;
        private readonly char _topBottonCorner;

        private Vector _gameZoneBegin;
        private Vector _gameZoneEnd;
        private Vector _workZoneEnd;

        public UI(Settings settings)
        {
            string tiles = settings.UITiles;
            if (tiles.Length < 6)
                throw new Exception(); //todo

            _sideBorders = tiles[0];
            _topBottonCorner = tiles[1];
            _leftTopCorner = tiles[2];
            _rightTopCorner = tiles[3];
            _leftBottomCorner = tiles[4];
            _rightBottomCorner = tiles[5];

            _workZoneEnd = settings.DrawzoneEnd;
            _gameZoneBegin = settings.UIGamezoneBegin;
            _gameZoneEnd = settings.UIGamezoneEnd;
        }


        public void Draw(DrawArgs args)
        {
            DrawBorders(args);

            //todo draw other
        }

        private void DrawBorders(DrawArgs args)
        {
            Vector topLeftCorner = _gameZoneBegin - new Vector(1, 1);
            Vector bottomRightCorner = _gameZoneEnd + new Vector(1, 1);

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
                            args.DrawAtAbsolutePoint(point, _topBottonCorner);
                    }
                }
                else
                {
                    args.DrawAtAbsolutePoint(topLeftCorner.X, y, _sideBorders);
                    args.DrawAtAbsolutePoint(bottomRightCorner.X, y, _sideBorders);
                }
            }
        }
    }
}