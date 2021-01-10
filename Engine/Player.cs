using Engine.ValueTypes;

namespace Engine
{
    public class Player
    {
        public PlayerNumber Number { get; private set; }

        public Player(PlayerNumber number)
        {
            Number = number;
        }
    }
}
