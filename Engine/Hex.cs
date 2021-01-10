using Engine.ValueTypes;

namespace Engine
{
    public class Hex
    {
        public Coordinates Coordinates { get; private set; }
        public PlayerNumber Owner { get; private set; }

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
    }
}
