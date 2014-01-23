using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace RogueLoise
{
    public class Game
    {
        private bool _exit;

        private double _updateTime;
        private double _updateElapsedTime;
        private double _updateGameTime;
        private bool _isGamePaused;
        private DateTime _updateLastTime;

        private double _drawTime;
        private double _drawElapsedTime;
        private DateTime _drawLastTime;

        private readonly Thread _drawThread;
        private readonly Thread _updateThread;
        private readonly UI _ui;
        private readonly IDrawer _drawer;

        private double _frame;
        private double _toDraw;

        public Settings Settings;

        private const string SettingsPath = "\\settings.cfg";

        public Game()
        {
            LoadSettings();
            
            _updateThread = new Thread(StartUpdate);
            _drawThread = new Thread(StartDraw);
            int fps = 60;
            _frame = 1000.0 / fps;
            _ui = new UI(Settings);
            _drawer = new ConsolePrinter();

            Run();
        }

        public void Run()
        {
            _updateThread.Start();
            _drawThread.Start();
        }

        private void LoadSettings()
        {
            var settingsPath = Environment.CurrentDirectory + SettingsPath;
            if (!File.Exists(settingsPath))
                return; //todo default settings

            List<string> settingLines = new List<string>();
            using (var reader = new StreamReader(settingsPath))
            {
                while (!reader.EndOfStream)
                {
                    settingLines.Add(reader.ReadLine());
                }
            }
            foreach (var settingLine in settingLines)
            {
                if (settingLine == null)
                    throw new Exception("WOOTUFUQUE");
                var line = settingLine.ToLower();
                var setting = line.Split('=');
                if (setting.Length != 2)
                    continue;
                setting[0] = setting[0].Trim();
                setting[1] = setting[1].Trim();
                try
                {
                    string[] vector;
                    switch (setting[0])
                    {
                        case "uiborders":
                            Settings.UITiles = setting[1];
                            break;
                        case "uigamezonebegin":
                            vector = setting[1].Split(',');
                            Settings.UIWorkzoneBegin = new Vector(int.Parse(vector[0]), int.Parse(vector[1]));
                            break;
                        case "uigamezoneend":
                            vector = setting[1].Split(',');
                            Settings.UIWorkzoneEnd = new Vector(int.Parse(vector[0]), int.Parse(vector[1]));
                            break;
                        default://todo log
                            break;
                    }
                }
                catch (Exception)
                {
                    //todo log?
                    throw;
                }
            }
        }

        private void StartUpdate()
        {
            _updateLastTime = DateTime.Now;
            while (!_exit)
            {
                _updateElapsedTime = (DateTime.Now - _updateLastTime).TotalMilliseconds;
                _updateLastTime = DateTime.Now;
                _updateTime += _updateElapsedTime;
                if (!_isGamePaused)
                    _updateGameTime += _updateElapsedTime;
                var args = new UpdateArgs
                {
                    GlobalTime = _updateTime,
                    GameTime = _updateGameTime,
                    ElapsedTime = _updateElapsedTime,
                    IsGamePaused = _isGamePaused
                };
                Update(args);
            }
        }

        private void StartDraw()
        {
            _drawLastTime = DateTime.Now;
            while (!_exit)
            {
                _drawElapsedTime = (DateTime.Now - _drawLastTime).TotalMilliseconds;
                _drawLastTime = DateTime.Now;
                _drawTime += _drawElapsedTime;
                _toDraw += _drawElapsedTime;
                if (_toDraw >= _frame)
                {
                    var args = new DrawArgs(_drawer) {GlobalTime = _drawTime, ElapsedTime = _toDraw};
                    Draw(args);
                    _toDraw = 0;
                }
            }
        }

        private void Update(UpdateArgs args)
        {
            //todo ui update

            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.Escape)
                _exit = true;

        }

        private void Draw(DrawArgs args)
        {
            //Console.Clear();
            _ui.Draw(args);

            _drawer.Flush();
        }
    }

    public struct Settings
    {
        public string UITiles;
        public Vector UIWorkzoneBegin;
        public Vector UIWorkzoneEnd;
    }
}