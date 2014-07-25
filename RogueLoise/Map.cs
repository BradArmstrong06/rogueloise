using System;
using System.Collections.Generic;
using System.Linq;

namespace RogueLoise
{
    public class Map : DrawableGameObject
    {
        private readonly List<GameObject>[,] _map;
        public bool IsGlobalMap;

        public Map(Game game) : this(game, 10, 10)
        {
        }

        public Map(Game game, int x, int y)
            : base(game)
        {
            _map = new List<GameObject>[x, y];
        }

        public GameObject this[int x, int y, int z]
        {
            get
            {
                if (z >= GetPointCount(x, y))
                    return null;

                return this[x, y][z];
            }

            set
            {
                if (z >= GetPointCount(x, y))
                    return;

                this[x, y][z] = value;
            }
        }

        public GameObject this[Vector point, int z]
        {
            get { return this[point.X, point.Y, z]; }

            set { this[point.X, point.Y, z] = value; }
        }

        public IList<GameObject> this[int x, int y]
        {
            get { return _map[x, y] ?? (_map[x, y] = new List<GameObject>()); }
        }

        public IList<GameObject> this[Vector point]
        {
            get { return this[point.X, point.Y]; }
        }

        public int GetPointCount(int x, int y)
        {
            return this[x, y].Count;
        }

        public int GetPointCount(Vector point)
        {
            return GetPointCount(point.X, point.Y);
        }

        /// <summary>
        ///     Добавить объект на карту и поменять его координаты
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="point"></param>
        /// <param name="enableObject"></param>
        public void Add(GameObject gameObject, int x, int y, bool enableObject = true)
        {
            gameObject.X = x;
            gameObject.Y = y;
            this[x, y].Add(gameObject);
            gameObject.IsEnabled = enableObject;
        }

        /// <summary>
        ///     Добавить объект на карту и поменять его координаты
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="point"></param>
        /// <param name="enableObject"></param>
        public void Add(GameObject gameObject, Vector point, bool enableObject = true)
        {
            Add(gameObject, point.X, point.Y, enableObject);
        }

        /// <summary>
        ///     Добавить объект на координаты, указанные в самом объекте
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="enableObject"></param>
        public void Add(GameObject gameObject, bool enableObject = true)
        {
            Add(gameObject, gameObject.Position, enableObject);
        }

        public bool MoveObject(GameObject gameObject, Vector point)
        {
            if (!CanMoveObject(gameObject, point))
                return false;

            this[gameObject.Position].Remove(gameObject);
            this[point].Add(gameObject);
            return true;
        }

        private bool CanMoveObject(GameObject gameObject, Vector point)
        {
            return point.X >= 0 && point.Y >= 0 && point.X < _map.GetLength(0) && point.Y < _map.GetLength(1); //todo
        }

        public override void Update(UpdateArgs args)
        {
            base.Update(args);

            //var xLength = _map.GetLength(0);
            //var yLength = _map.GetLength(1);

            //for(int x = 0; x < xLength; x++)
            //    for (int y = 0; y < yLength; y++)
            //    {
            //        this[x, y].ToList().ForEach(obj => obj.Update(args));
            //    }
            DoForAll(obj => obj.Update(args));
            DoForAll(ResetUpdate);
        }

        public override void Draw(DrawArgs args)
        {
            int xLength = _map.GetLength(0);
            int yLength = _map.GetLength(1);

            for (int x = 0; x < xLength; x++)
                for (int y = 0; y < yLength; y++)
                {
                    var drawable =
                        (DrawableGameObject) this[x, y].LastOrDefault(obj => (obj as DrawableGameObject) != null);
                    if (drawable != null)
                        args.DrawInGameZone(drawable);
                }
        }

        private void DoForAll(Action<GameObject> action)
        {
            int xLength = _map.GetLength(0);
            int yLength = _map.GetLength(1);

            for (int x = 0; x < xLength; x++)
                for (int y = 0; y < yLength; y++)
                {
                    this[x, y].ToList().ForEach(action);
                }
        }

        private void ResetUpdate(GameObject gameObject)
        {
            gameObject.ResetUpdate();
        }
    }

    //todo map loader
}