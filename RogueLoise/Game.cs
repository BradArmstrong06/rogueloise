using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
        private readonly int _updateRate;

        private Map _currentMap;
        public Settings Settings;

        public readonly ObjectDictionary ObjectsDictionary;

        private DrawableGameObject _player;

        private Vector _acticeCameraPositionAtMap;

        public Game()
        {
            LoadSettings();

            
            int fps = 60;
            _frame = 1000.0 / fps;
            _updateRate = 200;
            _ui = new UI(Settings);
            _drawer = ServiceLocator.GetService<IDrawer>();

            ObjectsDictionary = new ObjectDictionary();

            var floor = new DrawableGameObject(this) {Tile = '.', Name = "Floor", Key = "floor1"};
            ObjectsDictionary.Add(floor);

            _currentMap = new Map(this, 50, 50);

            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    _currentMap.Add(ObjectsDictionary["floor1"], x,y);
                }
            }
            _player = new Creature(this) {X = 2, Y = 2, IsPlayer = true, Tile = '@', Map = _currentMap};
            _currentMap.Add(_player);

            _updateThread = new Thread(StartUpdate);
            _drawThread = new Thread(StartDraw);

            Run();
        }

        private void Initialize()
        {
            
        }

        public void Run()
        {
            _updateThread.Start();
            _drawThread.Start();
        }

        private void LoadSettings()
        {
            var settingsProvider = ServiceLocator.GetService<SettingsProvider>();
            settingsProvider.LoadSettings();
            Settings = settingsProvider.Setting;
        }

        private void StartUpdate()
        {
            _updateLastTime = DateTime.Now;
            var keyCooler = 0.0;
            while (!_exit)
            {
                _updateElapsedTime = (DateTime.Now - _updateLastTime).TotalMilliseconds;
                _updateLastTime = DateTime.Now;
                _updateTime += _updateElapsedTime;
                
                if (!_isGamePaused)
                {
                    _updateGameTime += _updateElapsedTime;
                    keyCooler += _updateElapsedTime;
                }
                var key = Console.ReadKey(true).Key;

                var args = new UpdateArgs
                {
                    GlobalTime = _updateTime,
                    GameTime = _updateGameTime,
                    ElapsedTime = _updateElapsedTime,
                    IsGamePaused = _isGamePaused
                };

                if (keyCooler >= _updateRate)
                {
                    keyCooler = 0;
                    args.Key = key;
                }
                Update(args);
            }
        }

        private void StartDraw()
        {
            _drawLastTime = DateTime.Now;
            while (!_exit)
            {
                UpdateCamera();

                _drawElapsedTime = (DateTime.Now - _drawLastTime).TotalMilliseconds;
                _drawLastTime = DateTime.Now;
                _drawTime += _drawElapsedTime;
                _toDraw += _drawElapsedTime;
                if (_toDraw >= _frame)
                {
                    var args = new DrawArgs(_drawer)
                    {
                        GlobalTime = _drawTime,
                        ElapsedTime = _toDraw,
                        CameraPositionAtMap = _acticeCameraPositionAtMap
                    };
                    Draw(args);
                    _toDraw = 0;
                }
            }
        }

        private void Update(UpdateArgs args)
        {
            //todo ui update

            if (args.Key == ConsoleKey.Escape)
                _exit = true;

            _currentMap.Update(args);
        }

        private void UpdateCamera()
        {
            //todo camera movement
            if(_player != null && _player.IsEnabled)
            _acticeCameraPositionAtMap = _player.Position;
        }

        private void Draw(DrawArgs args)
        {
            _currentMap.Draw(args);
            _ui.Draw(args);

            _drawer.Flush();
        }
    }

    public struct Settings
    {
        public string UITiles;
        public Vector UIGamezoneBegin;
        public Vector UIGamezoneEnd;
        public Vector DrawzoneEnd;
    }
}