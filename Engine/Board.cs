using System;
using System.Collections.Generic;
using Engine.ValueTypes;

namespace Engine
{
    public class Board
    {
        public List<Hex> Hexes;
        public int Size;

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
    }
}
