using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.ValueTypes
{
    public struct Move
    {
        public Coordinates Coordinates;
        public PlayerNumber PlayerNumber;

        public Move(int x, int y, PlayerNumber playerNumber)
        {
            Coordinates = new Coordinates(x, y);
            PlayerNumber = playerNumber;
        }

        public override string ToString()
        {
            return Coordinates.ToString() + " : Player #" + PlayerNumber;
        }
    }
}
