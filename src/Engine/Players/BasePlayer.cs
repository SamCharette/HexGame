﻿using System;
using System.Collections.Generic;
using System.Text;
using Engine.ValueTypes;

namespace Engine.Players
{

    public interface IPlayer
    {
        public PlayerNumber Number { get; }
        public string Type { get; }

        public Move MakeMove(Coordinates opponentMove);

    }

    public abstract class MustInitialize<T>
    {
        protected MustInitialize(T parameters)
        {

        }
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
