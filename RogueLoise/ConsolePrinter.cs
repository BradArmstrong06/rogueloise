using System;

namespace RogueLoise
{
    public interface IDrawer
    {
        void Draw(int x, int y, char c);

        void Draw(Vector point, char c);

        void Flush();
    }

    public class ConsolePrinter : IDrawer
    {
        private char[,] _oldPresent;

        private char[,] _newPresent;

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
            var settings = _settingsProvider.Setting;
            _oldPresent = new char[settings.DrawzoneEnd.X, settings.DrawzoneEnd.Y];
            _newPresent = new char[settings.DrawzoneEnd.X, settings.DrawzoneEnd.Y];
        }

        public void Draw(int x, int y, char c)
        {
            _newPresent[x, y] = c;
        }

        public void Draw(Vector point, char c)
        {
            Draw(point.X, point.Y, c);
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

                    Console.SetCursorPosition(x,y);
                    Console.Write(toDraw[x, y]);
                }
            }
            Console.SetCursorPosition(0, YLength);
            _oldPresent = _newPresent;
        }

        private char?[,] GetChanges()
        {

            var changes = new char?[XLength, YLength];
            for (var x = 0; x < XLength; x++)
            {
                for (var y = 0; y < YLength; y++)
                {
                    changes[x, y] = _oldPresent[x, y] != _newPresent[x, y] ? _newPresent[x, y] : (char?) null;
                }
            }
            return changes;
        }
    }
}