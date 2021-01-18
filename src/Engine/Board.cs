using System;
using System.Collections.Generic;
using System.Linq;
using Engine.ValueTypes;

namespace Engine
{
    public class Board
    {
        public List<Hex> Hexes { get; private set; }
        public int Size { get; private set; }


        public Board(int size)
        {
            if (size < 5)
            {
                throw new ArgumentException("Size must be at least 5.", nameof(size));
            }

            Size = size;
            Hexes = new List<Hex>();
            SetUp();
        }

        private void SetUp()
        {
            for (var y = 1; y <= Size; y++)
            {
                for (var x = 1; x <= Size; x++)
                {
                    Hexes.Add(new Hex(x, y, PlayerNumber.Unowned));
                }
            }
        }
        public Hex HexAt(Coordinates coordinates)
        {
            return Hexes.FirstOrDefault(x => x.Coordinates.X == coordinates.X && x.Coordinates.Y == coordinates.Y);
        }

    }
}
