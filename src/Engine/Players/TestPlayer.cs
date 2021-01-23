using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine.ValueTypes;

namespace Engine.Players
{
    public class TestPlayer : PlayerBase<PlayerConstructorArguments>
    {
        public List<Coordinates> Moves { get; set; } = new List<Coordinates>();

        public TestPlayer(PlayerConstructorArguments parameters) : base(parameters)
        {
        }

        public override Coordinates MakeMove(Coordinates opponentMove)
        {
            if (!Moves.Any()) return new Coordinates(-1,-1);

            var move = new Coordinates(Moves.FirstOrDefault());
            Moves.RemoveAt(0);
            return move;
        }

    }
}
