using Engine.ValueTypes;

namespace Engine
{
    public class Hex
    {
        public Coordinates Coordinates;
        public PlayerNumber Owner { get; set; } = 0;

        public Hex(int x, int y, PlayerNumber owner = PlayerNumber.Unowned)
        {
            Coordinates = new Coordinates(x, y);
            Owner = owner;
        }

        public void SetOwner(PlayerNumber newOwner)
        {
            Owner = newOwner;
        }
    }
}
