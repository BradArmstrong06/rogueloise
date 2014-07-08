using System;

namespace RogueLoise
{
    public interface IDrawer
    {
        void DrawAtAbsolutePoint(int x, int y, char c, ConsoleColor color = ConsoleColor.White);

        void DrawAtAbsolutePoint(Vector point, char c, ConsoleColor color = ConsoleColor.White);

        void DrawInGameZone(int x, int y, char c, ConsoleColor color = ConsoleColor.White);

        void DrawInGameZone(Vector point, char c, ConsoleColor color = ConsoleColor.White);

        void DrawAtAbsolutePoint(int x, int y, string s, ConsoleColor color = ConsoleColor.White);

        void Flush();
    }

    public class ConsolePrinter : IDrawer
    {
        private Vector _cameraPositionAtScreen;
        private ColorCharPair[,] _newPresent;
        private ColorCharPair[,] _oldPresent;

        public ConsolePrinter()
        {
            Settings settings = LastSettings;
            _oldPresent = new ColorCharPair[settings.DrawzoneEnd.X, settings.DrawzoneEnd.Y];
            _newPresent = new ColorCharPair[settings.DrawzoneEnd.X, settings.DrawzoneEnd.Y];

            SetCameraPosition();
        }

        private Settings LastSettings
        {
            get { return _settingsProvider.Setting; }
        }

        private int XLength
        {
            get { return _newPresent.GetLength(0); }
        }

        private int YLength
        {
            get { return _newPresent.GetLength(1); }
        }

        private SettingsProvider _settingsProvider
        {
            get { return ServiceLocator.GetService<SettingsProvider>(); }
        }

        public void DrawAtAbsolutePoint(int x, int y, char c, ConsoleColor color = ConsoleColor.White)
        {
            if(x >= 0 && x < XLength && y >= 0 && y < YLength)
                _newPresent[x, y] = new ColorCharPair(c, color);
        }

        public void DrawAtAbsolutePoint(int x, int y, string s, ConsoleColor color = ConsoleColor.White)
        {
            if(s == null)
                return;

            int i = 0;
            for (int newX = x; newX < (x + s.Length > XLength ? XLength : x + s.Length); newX++)
            {
                _newPresent[newX, y] = new ColorCharPair(s[i], color);
                i++;
            }
        }

        public void DrawAtAbsolutePoint(Vector point, char c, ConsoleColor color = ConsoleColor.White)
        {
            DrawAtAbsolutePoint(point.X, point.Y, c, color);
        }

        public void DrawInGameZone(int x, int y, char c, ConsoleColor color = ConsoleColor.White)
        {
            DrawInGameZone(new Vector(x, y), c, color);
        }

        /// <summary>
        /// </summary>
        /// <param name="point">Позиция относительно активной камеры</param>
        /// <param name="c"></param>
        /// <param name="color"></param>
        public void DrawInGameZone(Vector point, char c, ConsoleColor color = ConsoleColor.White)
        {
            Settings settings = _settingsProvider.Setting;
            Vector absolutePoint = point + _cameraPositionAtScreen;

            //вне пределов зоны отрисовки карты
            if (absolutePoint.X < settings.UIGamezoneBegin.X || absolutePoint.Y < settings.UIGamezoneBegin.Y ||
                absolutePoint.X > settings.UIGamezoneEnd.X || absolutePoint.Y > settings.UIGamezoneEnd.Y)
                return;
            DrawAtAbsolutePoint(absolutePoint, c, color);
        }

        public void Flush()
        {
            ColorCharPair?[,] toDraw = GetChanges();
            for (int x = 0; x < XLength; x++)
            {
                for (int y = 0; y < YLength; y++)
                {
                    if (!toDraw[x, y].HasValue)
                        continue;

                    SetPosition(x, y);
                    ColorCharPair pair = toDraw[x, y].Value;
                    Console.ForegroundColor = pair.Color;
                    Console.Write(pair.Tile);
                }
            }
            SetPosition(0, YLength);
            _oldPresent = (ColorCharPair[,]) _newPresent.Clone();
            _newPresent = new ColorCharPair[XLength, YLength];
        }

        private void SetCameraPosition()
        {
            Settings settings = LastSettings;
            Vector gameZoneCenter = settings.UIGamezoneEnd - settings.UIGamezoneBegin;
            gameZoneCenter.X /= 2;
            gameZoneCenter.Y /= 2;
            _cameraPositionAtScreen = settings.UIGamezoneBegin + gameZoneCenter;
        }

        private void SetCameraPosition(int x, int y)
        {
            SetCameraPosition(new Vector(x, y));
        }

        private void SetCameraPosition(Vector point)
        {
            Settings settings = LastSettings;
            _cameraPositionAtScreen.X = point.X < settings.UIGamezoneBegin.X
                ? settings.UIGamezoneBegin.X
                : point.X > settings.UIGamezoneEnd.X ? settings.UIGamezoneEnd.X : point.X;

            _cameraPositionAtScreen.X = point.X < settings.UIGamezoneBegin.X
                ? settings.UIGamezoneBegin.X
                : point.X > settings.UIGamezoneEnd.X ? settings.UIGamezoneEnd.X : point.X;
        }

        private void SetPosition(int x, int y)
        {
            if (Console.CursorLeft != x || Console.CursorTop != y)
                Console.SetCursorPosition(x, y);
        }

        private ColorCharPair?[,] GetChanges()
        {
            var changes = new ColorCharPair?[XLength, YLength];
            for (int x = 0; x < XLength; x++)
            {
                for (int y = 0; y < YLength; y++)
                {
                    if (_oldPresent[x, y] == _newPresent[x, y])
                        continue;

                    changes[x, y] = _oldPresent[x, y] != _newPresent[x, y] ? _newPresent[x, y] : (ColorCharPair?) null;
                }
            }
            return changes;
        }
    }
}