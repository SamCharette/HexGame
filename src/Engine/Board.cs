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

        public List<Hex> GetNeighboursOf(Hex hex)
        {
            return GetNeighboursOf(hex.X, hex.Y);
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
            return GetFriendlyNeighboursOf(hex);

        }

        public List<Hex> GetFriendlyNeighboursOf(Hex hex)
        {
            if (hex == null) return new List<Hex>();

            return GetNeighboursOf(hex).Where(otherHex => otherHex.Owner == hex.Owner).ToList();

        }

        private List<Hex> GetHexesOwnedBy(PlayerNumber player)
        {
            return Hexes.Where(x => x.Owner == player).ToList();
        }

        public List<Hex> UnownedHexes()
        {
            return GetHexesOwnedBy(PlayerNumber.Unowned);
        }

        public List<Hex> Player1Hexes()
        {
            return GetHexesOwnedBy(PlayerNumber.FirstPlayer);
        }

        public List<Hex> Player2Hexes()
        {
            return GetHexesOwnedBy(PlayerNumber.SecondPlayer);
        }

        private void AddNeighbourIfItExists(int x, int y, List<Hex> neighbours)
        {
            var hex = HexAt(x, y);
            AddNeighbourIfItExists(hex, neighbours);
        }

        private void AddNeighbourIfItExists(Hex hex, List<Hex> neighbours)
        {
            if (hex != null)
            {
                neighbours.Add(hex);
            }
        }

        public void ClaimHex(int x, int y, PlayerNumber newOwner)
        {
            var hex = HexAt(x, y);
            hex.SetOwner(newOwner);
        }

        public void ReleaseHex(int x, int y)
        {
            var hex = HexAt(x, y);
            hex.SetOwner(PlayerNumber.Unowned);
        }

        public bool IsStartHexForPlayer(Hex hex, PlayerNumber player)
        {
            if (!Hexes.Contains(hex)) return false;

            if (player == PlayerNumber.FirstPlayer)
            {
                return hex.Y == 0;
            }

            return hex.X == 0;
        }

        public bool IsEndHexForPlayer(Hex hex, PlayerNumber player)
        {
            if (!Hexes.Contains(hex)) return false;

            if (player == PlayerNumber.FirstPlayer)
            {
                return hex.Y == Size - 1;
            }

            return hex.X == Size - 1;
        }

        public bool AreConnected(Hex start, Hex finish)
        {
            var visited = new HashSet<Hex>();

            return AreConnected(start, finish, visited);
        }

        private bool AreConnected(Hex start, Hex finish, HashSet<Hex> visited)
        {
            if (IsBadCallToAreConnected(start, finish)) return false;

            if (start.Equals(finish))
            {
                return true;
            }
            
            var fringes = GetFringes(start, visited);

            visited.Add(start);

            while (fringes.Any())
            {
                var hex = fringes.FirstOrDefault();
                visited.Add(hex);
                if (AreConnected(hex, finish, visited))
                {
                    return true;
                }

                fringes.Remove(hex);
            
            }

            return false;
            
        }

        private static bool IsBadCallToAreConnected(Hex start, Hex finish)
        {
            if (start == null || finish == null)
            {
                return true;
            }

            if (start.Owner != finish.Owner)
            {
                return true;
            }

            return false;
        }

        private HashSet<Hex> GetFringes(Hex start, HashSet<Hex> visited)
        {
            HashSet<Hex> fringes = new HashSet<Hex>();
            GetFriendlyNeighboursOf(start)
                .Where(x => !visited.Contains(x))
                .ToList()
                .ForEach(x => fringes.Add(x));
            return fringes;
        }
    }
}
