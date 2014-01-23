using System;

namespace RogueLoise
{
    public class UI
    {
        private string _borders;
        private char _sideBorders;
        private char _topBottonBorder;
        private char _leftTopBorder;
        private char _leftBottomBorder;
        private char _rightTopBorder;
        private char _rightBottomBorder;




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
            _leftTopBorder = tiles[2];
            _rightTopBorder = tiles[3];
            _leftBottomBorder = tiles[4];
            _rightBottomBorder = tiles[5];

            GameZoneBegin = settings.UIWorkzoneBegin;
            GameZoneEnd = settings.UIWorkzoneEnd;

        }



        public void Draw(DrawArgs args)
        {
            DrawBorders(args);

            //todo draw other
        }

        private void DrawBorders(DrawArgs args)
        {
            for (int y = GameZoneBegin.Y; y <= GameZoneEnd.Y; y++)
            {
                if (y == GameZoneBegin.Y || y == GameZoneEnd.Y)
                {
                    for (int x = GameZoneBegin.X; x <= GameZoneEnd.X; x++)
                    {
                        var point = new Vector(x, y);

                        if (x == GameZoneBegin.X)
                        {
                            if (y == GameZoneBegin.Y)
                                args.Draw(point, _leftTopBorder);
                            if (y == GameZoneEnd.Y)
                                args.Draw(point, _leftBottomBorder);
                        }
                        else
                            if (x == GameZoneEnd.X)
                            {
                                if (y == GameZoneBegin.Y)
                                    args.Draw(point, _rightTopBorder);
                                if (y == GameZoneEnd.Y)
                                    args.Draw(point, _rightBottomBorder);
                            }
                            else
                                args.Draw(point, _topBottonBorder);
                    }
                }
                else
                {
                    args.Draw(GameZoneBegin.X, y, _sideBorders);
                    args.Draw(GameZoneEnd.X, y, _sideBorders);
                }
            }
        }
    }
}