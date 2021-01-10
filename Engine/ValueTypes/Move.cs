using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.ValueTypes
{
    public struct Move
    {
        public Coordinates Coordinates { get; private set; }
        public PlayerNumber PlayerNumber { get; private set; }

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
    }
}
