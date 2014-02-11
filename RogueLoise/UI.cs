using System;
using System.ComponentModel.Design;

namespace RogueLoise
{
    public class UI
    {
        private string _borders;
        private char _sideBorders;
        private char _topBottonBorder;
        private char _leftTopCorner;
        private char _leftBottomCorner;
        private char _rightTopCorner;
        private char _rightBottomCorner;




        public Vector WorkZone;
        public Vector GameZoneEnd;
        public Vector GameZoneBegin;

        public UI(Settings settings)
        {
            var tiles = settings.UITiles;
            if(tiles.Length < 6)
                throw new Exception(); //todo

            _sideBorders = tiles[0];
            _topBottonBorder = tiles[1];
            _leftTopCorner = tiles[2];
            _rightTopCorner = tiles[3];
            _leftBottomCorner = tiles[4];
            _rightBottomCorner = tiles[5];

            GameZoneBegin = settings.UIGamezoneBegin;
            GameZoneEnd = settings.UIGamezoneEnd;
        }



        public void Draw(DrawArgs args)
        {
            DrawBorders(args);

            //todo draw other
        }

        private void DrawBorders(DrawArgs args)
        {
            var topLeftCorner = GameZoneBegin - new Vector(1, 1);
            var bottomRightCorner = GameZoneEnd + new Vector(1, 1);

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
                        else
                            if (x == bottomRightCorner.X)
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
    }
}