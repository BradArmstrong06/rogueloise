using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace RogueLoise
{
    public interface IDrawer
    {
        void DrawAtAbsolutePoint(int x, int y, char c);

        void DrawAtAbsolutePoint(Vector point, char c);

        void DrawInGameZone(int x, int y, char c);

        void DrawInGameZone(Vector point, char c);

        void DrawAtAbsolutePoint(int x, int y, string s);

        void Flush();
    }

    public class ConsolePrinter : IDrawer
    {
        private char[,] _oldPresent;

        private char[,] _newPresent;

        private Vector _cameraPositionAtScreen;

        private Settings LastSettings
        {
            get
            {
                return _settingsProvider.Setting;
            }
        }

        private int XLength
        {
            get
            {
                return _newPresent.GetLength(0);
            }
        }

        private int YLength
        {
            get
            {
                return _newPresent.GetLength(1);
            }
        }

        private SettingsProvider _settingsProvider
        {
            get
            {
                return ServiceLocator.GetService<SettingsProvider>();
            }
        }

        public ConsolePrinter()
        {
            var settings = LastSettings;
            _oldPresent = new char[settings.DrawzoneEnd.X, settings.DrawzoneEnd.Y];
            _newPresent = new char[settings.DrawzoneEnd.X, settings.DrawzoneEnd.Y];

            SetCameraPosition();
        }

        private void SetCameraPosition()
        {
            var settings = LastSettings;
            var gameZoneCenter = settings.UIGamezoneEnd - settings.UIGamezoneBegin;
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
            var settings = LastSettings;
            _cameraPositionAtScreen.X = point.X < settings.UIGamezoneBegin.X
                ? settings.UIGamezoneBegin.X
                : point.X > settings.UIGamezoneEnd.X ? settings.UIGamezoneEnd.X : point.X;

            _cameraPositionAtScreen.X = point.X < settings.UIGamezoneBegin.X
                ? settings.UIGamezoneBegin.X
                : point.X > settings.UIGamezoneEnd.X ? settings.UIGamezoneEnd.X : point.X;
        }

        public void DrawAtAbsolutePoint(int x, int y, char c)
        {
            _newPresent[x, y] = c;
        }

        public void DrawAtAbsolutePoint(int x, int y, string s)
        {
            int i = 0;
            for (int newX = x; newX < (x + s.Length > XLength ? XLength : x + s.Length); newX++)
            {
                _newPresent[newX, y] = s[i];
                i++;
            }
        }

        public void DrawAtAbsolutePoint(Vector point, char c)
        {
            DrawAtAbsolutePoint(point.X, point.Y, c);
        }

        public void DrawInGameZone(int x, int y, char c)
        {
            DrawInGameZone(new Vector(x,y), c);
        }
        ///<summary>
        ///</summary>
        /// <param name="point">Позиция относительно активной камеры</param>
        /// <param name="c"></param>
        public void DrawInGameZone(Vector point, char c)
        {
            var settings = _settingsProvider.Setting;
            var absolutePoint = point + _cameraPositionAtScreen;

            //вне пределов зоны отрисовки карты
            if (absolutePoint.X < settings.UIGamezoneBegin.X || absolutePoint.Y < settings.UIGamezoneBegin.Y ||
                absolutePoint.X > settings.UIGamezoneEnd.X || absolutePoint.Y > settings.UIGamezoneEnd.Y) 
                return;
            DrawAtAbsolutePoint(absolutePoint, c);
        }

        public void Flush()
        {
            var toDraw = GetChanges();
            for (var x = 0; x < XLength; x++)
            {
                for (var y = 0; y < YLength; y++)
                {
                    if(!toDraw[x,y].HasValue)
                        continue;

                    SetPosition(x,y);
                    Console.Write(toDraw[x, y]);
                }
            }
            SetPosition(0, YLength);
            _oldPresent = (char[,]) _newPresent.Clone();
            _newPresent = new char[XLength,YLength];
        }

        private void SetPosition(int x, int y)
        {
            if(Console.CursorLeft != x || Console.CursorTop != y)
                Console.SetCursorPosition(x, y);
        }

        private char?[,] GetChanges()
        {

            var changes = new char?[XLength, YLength];
            for (var x = 0; x < XLength; x++)
            {
                for (var y = 0; y < YLength; y++)
                {
                    var old = _oldPresent[x, y];
                    var newc = _newPresent[x, y];
                    if (_oldPresent[x, y] == _newPresent[x, y])
                        continue;

                    
                    changes[x, y] = _oldPresent[x, y] != _newPresent[x, y] ? _newPresent[x, y] : (char?) null;
                    var c = changes[x, y];
                }
            }
            return changes;
        }
    }
}