using System;
using System.Collections.Generic;

namespace RogueLoise
{
    public class Map : DrawableGameObject
    {
        public bool IsGlobalMap;

        GameObject this[int x, int y, int z]
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

        GameObject this[Vector point, int z]
        {
            get
            {
                return this[point.X, point.Y, z];
            }

            set
            {
                this[point.X, point.Y, z] = value;
            }
        }

        IList<GameObject> this[int x, int y]
        {
            get { return _map[x, y] ?? (_map[x, y] = new List<GameObject>()); }
        }

        IList<GameObject> this[Vector point]
        {
            get
            {
                return this[point.X, point.Y];
            }
        }

        public int GetPointCount(int x, int y)
        {
            return this[x, y].Count;
        }

        public int GetPointCount(Vector point)
        {
            return GetPointCount(point.X, point.Y);
        }

        private List<GameObject>[,] _map;

        public Map()
        {
            _map = new List<GameObject>[10,10];
        }



        public void MoveObject(GameObject gameObject, Vector point)
        {
            
        }

    }

    //todo map loader
}