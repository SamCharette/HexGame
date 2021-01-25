using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine.ValueTypes;
using Engine;

namespace Engine.Players
{
    public class TestPlayer : BasePlayer
    {
        public List<Coordinates> Moves { get; set; } = new List<Coordinates>();
        public override string Type { get; protected set; } = "Test";

        public TestPlayer(PlayerConstructorArguments parameters) : base(parameters)
        {
        }

        public override Move MakeMove(Coordinates opponentMove)
        {
            if (!Moves.Any())
            {
                return new Move(new Coordinates(-1,-1), Number);
            }

            var move = new Move(new Coordinates(Moves.FirstOrDefault()), Number);
            Moves.RemoveAt(0);
            return move;
        }

    }
}
