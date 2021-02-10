using System;
using System.Collections.Generic;
using System.Text;
using Engine.ValueTypes;

namespace Engine.Players
{

    public abstract class PlayerBase 
    {
        public PlayerNumber Number { get; protected set; }
        public string Type { get; protected set; }
        protected int Size { get; set; }
        public abstract Move MakeMove(Coordinates opponentMove);

        public void SetPlayer(PlayerNumber number)
        {
            Number = number;
        }

        public void SetBoardSize(int size)
        {
            Size = size;
        }

        public abstract void Configure(Configuration config);
    }

}
