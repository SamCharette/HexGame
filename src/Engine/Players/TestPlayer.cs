using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Engine.ValueTypes;
using Engine;
using Newtonsoft.Json.Linq;

namespace Engine.Players
{
    public class TestPlayer : PlayerBase
    {
        public Queue<Coordinates> Moves { get; set; } = new Queue<Coordinates>();

        public override void Configure(string data)
        {
            var regex = new Regex("([0-9]+[\\w\\s][0-9]+)$", RegexOptions.None, TimeSpan.FromSeconds(1));
            JObject movesToMake = JObject.Parse(data);
            foreach (var move in movesToMake)
            {
                if (move.Key.ToUpper() == "MOVE")
                {
                    if (regex.IsMatch((string) move.Value))
                    {
                        var bits = move.Value.ToString().Split(' ');
                        var x = int.Parse(bits[0]);
                        var y = int.Parse(bits[1]);
                        AddMove(new Coordinates(x, y));
                    }
                }
            }
        }

        public TestPlayer AddMove(Coordinates coordinates)
        {
            Moves.Enqueue(coordinates);
            return this;
        }

        public TestPlayer AddMove(int x, int y)
        {
            return AddMove(new Coordinates(x, y));
        }

        public Coordinates ViewNextMove()
        {
            return Moves.Any() ? Moves.Peek() : new Coordinates(-1,-1);
        }

        public override Move MakeMove(Coordinates opponentMove)
        {
            if (!Moves.Any())
            {
                return new Move(new Coordinates(-1,-1), Number);
            }

            var move = new Move(Moves.Dequeue(), Number);
            
            return move;
        }


    }
}
