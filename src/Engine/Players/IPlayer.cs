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
        public PlayerNumber PlayerNumber { get; set; }
        public int BoardSize { get; set; }
    }
}
