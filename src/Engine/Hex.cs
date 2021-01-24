using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Engine.ValueTypes;

namespace Engine
{
    public class Hex
    {
        public readonly Coordinates Coordinates;

        public PlayerNumber Owner { get; private set; }
        public bool IsOwned => Owner != PlayerNumber.Unowned;

        public int X => Coordinates.X;
        public int Y => Coordinates.Y;
        public HashSet<Hex> ConnectedHexList { get; private set; } = new HashSet<Hex>();

        public Hex(int x, int y, PlayerNumber owner = PlayerNumber.Unowned)
        {
            Coordinates = new Coordinates(x, y);
            Owner = owner;
        }

        public Hex(Coordinates coordinates, PlayerNumber owner = PlayerNumber.Unowned)
        {
            Coordinates = coordinates;
            Owner = owner;
        }

        public void SetOwner(PlayerNumber newOwner)
        {
            Owner = newOwner;
     
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            var otherHex = obj as Hex;
            return Coordinates.Equals(otherHex?.Coordinates);
        }

        protected bool Equals(Hex other)
        {
            return Coordinates.Equals(other.Coordinates);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Coordinates);
        }
    }
}
