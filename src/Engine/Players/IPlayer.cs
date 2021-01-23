using System;
using System.Collections.Generic;
using System.Text;
using Engine.ValueTypes;

namespace Engine.Players
{
    public interface IPlayer
    {
        public Coordinates MakeMove(Coordinates opponentMove);
    }

    public abstract class PlayerBase<T> : IPlayer
    {
        protected PlayerBase(T parameters)
        {

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
