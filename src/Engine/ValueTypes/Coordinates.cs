using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.ValueTypes
{
    public readonly struct Coordinates
    {
        public readonly int X;
        public readonly int Y;

        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Coordinates(Coordinates coordsToCopy)
        {
            X = coordsToCopy.X;
            Y = coordsToCopy.Y;
        }

        public override string ToString()
        {
            return "(" + X + "," + Y + ")";
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Coordinates))
                return false;

            Coordinates otherCoordinates = (Coordinates) obj;
            return X == otherCoordinates.X && Y == otherCoordinates.Y;
        }

        public bool Equals(Coordinates other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}
