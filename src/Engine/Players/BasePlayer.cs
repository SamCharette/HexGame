using System;
using System.Collections.Generic;
using System.Text;
using Engine.ValueTypes;

namespace Engine.Players
{

    public abstract class BasePlayer
    {
        public PlayerNumber Number { get; protected set; }
        protected BasePlayer(PlayerConstructorArguments parameters)
        {
            Number = parameters.PlayerNumber;
        }

        public abstract Coordinates MakeMove(Coordinates opponentMove);
    }

    public class PlayerConstructorArguments
    {
        public PlayerNumber PlayerNumber { get; private set; }
        public int BoardSize { get; private set; }
        public Configuration Configuration { get; private set; }

        public PlayerConstructorArguments(int size, PlayerNumber number, string config = "")
        {
            PlayerNumber = number;
            BoardSize = size;
            Configuration = Configuration.GetConfiguration(config);
        }
    }
}
