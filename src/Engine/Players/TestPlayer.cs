using System;
using System.Collections.Generic;
using System.Text;
using Engine.ValueTypes;

namespace Engine.Players
{
    public class TestPlayer : PlayerBase<PlayerConstructorArguments>, IPlayer
    {
        public TestPlayer(PlayerConstructorArguments parameters) : base(parameters)
        {
        }

        public Coordinates MakeMove(Coordinates opponentMove)
        {
            throw new NotImplementedException();
        }
    }
}
