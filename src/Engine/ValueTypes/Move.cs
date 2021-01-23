using System;

namespace Engine.ValueTypes
{
    public readonly struct Move
    {
        public readonly Coordinates Coordinates { get; }
        public readonly PlayerNumber PlayerNumber { get; }

        public Move(int x, int y, PlayerNumber playerNumber)
        {
            Coordinates = new Coordinates(x, y);
            PlayerNumber = playerNumber;
        }

        public Move(Coordinates coordinates, PlayerNumber playerNumber)
        {
            Coordinates = coordinates;
            PlayerNumber = playerNumber;
        }

        public override string ToString()
        {
            return Coordinates + " : Player #" + PlayerNumber;
        }

        public bool Equals(Move other)
        {
            return Coordinates.Equals(other.Coordinates) && PlayerNumber == other.PlayerNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Coordinates, (int) PlayerNumber);
        }
    }
}
