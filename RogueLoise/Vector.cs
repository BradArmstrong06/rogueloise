namespace RogueLoise
{
    public struct Vector
    {
        public bool Equals(Vector other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Vector && Equals((Vector) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X*397) ^ Y;
            }
        }

        //public Vector()
        //{
        //    X = 0;
        //    Y = 0;
        //}
        public Vector(int x, int y) : this()
        {
            X = x;
            Y = y;
        }
        public int X { get; set; }

        public int Y { get; set; }

        public static bool operator ==(Vector a, Vector b)
        {
            //if (a == null || b == null)
            //    return false;
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(Vector a, Vector b)
        {
            //if (a == null || b == null)
            //    return true;
            return a.X != b.X || a.Y != b.Y;
        }
    }
}