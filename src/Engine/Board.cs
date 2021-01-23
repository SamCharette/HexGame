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
            for (var y = 0; y < Size; y++)
            {
                for (var x = 0; x < Size; x++)
                {
                    Hexes.Add(new Hex(x, y));
                }
            }
        }
        public Hex HexAt(Coordinates coordinates)
        {
            return HexAt(coordinates.X, coordinates.Y);
        }

        public Hex HexAt(int x, int y)
        {
            return Hexes.FirstOrDefault(hex => hex.X == x && hex.Y == y);
        }

        public List<Hex> GetNeighboursOf(Coordinates coordinates)
        {
            return GetNeighboursOf(coordinates.X, coordinates.Y);
        }
        public List<Hex> GetNeighboursOf(int x, int y)
        {
            var neighbours = new List<Hex>();
            var hex = HexAt(x, y);

            if (hex == null) return neighbours; 

            AddNeighbourIfItExists(
                hex.X, 
                hex.Y - 1, 
                neighbours);
            AddNeighbourIfItExists(
                hex.X + 1, 
                hex.Y - 1, 
                neighbours);
            AddNeighbourIfItExists(
                hex.X + 1, 
                hex.Y, 
                neighbours);
            AddNeighbourIfItExists(
                hex.X, 
                hex.Y + 1, 
                neighbours);
            AddNeighbourIfItExists(
                hex.X - 1, 
                hex.Y + 1, 
                neighbours);
            AddNeighbourIfItExists(
                hex.X - 1, 
                hex.Y, 
                neighbours);

            return neighbours;

        }

        public List<Hex> GetFriendlyNeighboursOf(int x, int y)
        {
            var hex = HexAt(x, y);

            if (hex == null) return new List<Hex>();

            return GetNeighboursOf(x, y).Where(otherHex => otherHex.Owner == hex.Owner).ToList();

        }

        private void AddNeighbourIfItExists(int x, int y, List<Hex> neighbours)
        {
            var hex = HexAt(x, y);
            if (hex != null)
            {
                neighbours.Add(hex);
            }
        }

    }
}
