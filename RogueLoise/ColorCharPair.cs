using System;

namespace RogueLoise
{
    internal struct ColorCharPair
    {
        public ConsoleColor Color;
        public char Tile;

        public ColorCharPair(char tile, ConsoleColor color)
        {
            Color = color;
            Tile = tile;
        }

        public bool Equals(ColorCharPair other)
        {
            return Color == other.Color && Tile == other.Tile;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is ColorCharPair && Equals((ColorCharPair) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) Color*397) ^ Tile.GetHashCode();
            }
        }

        public static bool operator ==(ColorCharPair a, ColorCharPair b)
        {
            return a.Color == b.Color && a.Tile == b.Tile;
        }

        public static bool operator !=(ColorCharPair a, ColorCharPair b)
        {
            return !(a == b);
        }
    }
}