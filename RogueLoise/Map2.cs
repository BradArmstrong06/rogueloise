using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace RogueLoise
{
    public class Map2 : DrawableGameObject
    {
        public List<GameObject> GameObjects { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public Map2(Game game) : this(game, 10, 10)
        {
        }

        public Map2(Game game, int width, int height)
            : base(game)
        {
            GameObjects = new List<GameObject>();
            Width = width;
            Height = height;
        }

        public int GetPointCount(int x, int y)
        {
            return GameObjects.Count(obj => obj.X == x && obj.Y == y);
        }

        public void Add(GameObject gameObject)
        {
            var count = GetPointCount(gameObject.X, gameObject.Y);
            if(gameObject.Z != -1 )
            GameObjects.Add(gameObject);
        }

        private GameObject GetTopObject(int x, int y)
        {
            return GameObjects.Where(obj => obj.X == x && obj.Y == y).OrderBy(obj => obj.Z).LastOrDefault();
        }

        private DrawableGameObject GetTopDrawableObject(int x, int y)
        {
            return (DrawableGameObject)GameObjects.Where(obj => obj is DrawableGameObject && obj.X == x && obj.Y == y).OrderBy(obj => obj.Z).LastOrDefault();
        }

        public bool CanMoveObject(GameObject gameObject, Vector point)
        {
            return point.X >= 0 && point.Y >= 0 && point.X < Width && point.Y < Height 
                && !HasCollision(gameObject, point); //todo
        }

        private bool HasCollision(GameObject gameObject, Vector point)
        {
            return false; //todo
        }

        public override void Draw(DrawArgs args)
        {
            //foreach (var gameObject in GameObjects)
            //{
            //    var drawable = gameObject as DrawableGameObject;
            //    if (drawable != null)
            //        args.DrawInGameZone(drawable);
            //}

            var groupsByCoords = GameObjects.OrderBy(obj => obj.Z).GroupBy(obj => obj.X, obj => obj.Y);


        }
    }
}