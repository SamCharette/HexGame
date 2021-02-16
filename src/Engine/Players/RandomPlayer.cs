using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Engine.ValueTypes;
using Engine;

namespace Engine.Players
{
    public class RandomPlayer : PlayerBase
    {
        private Board Board;
        private int _minimumWaitTime;
        private int _maximumWaitTime;
        private bool _hasWaitTime = true;

        public override void Configure(Configuration config)
        {
            Board = new Board(Size);
            if (config != null)
            {
                _minimumWaitTime = config.GetIntFromOption("MinimumWaitTime");
                _maximumWaitTime = config.GetIntFromOption("MaximumWaitTime");

                if (_minimumWaitTime < 0 || _maximumWaitTime < _minimumWaitTime)
                {
                    _maximumWaitTime = 0;
                    _minimumWaitTime = 0;
                    _hasWaitTime = false;
                }
            }
        }


        public override Move MakeMove(Coordinates opponentMove)
        {
            try
            {
                SetOpponentPosition(opponentMove);
                var randomGenerator = new Random();

                if (_hasWaitTime)
                {
                    Thread.Sleep(randomGenerator.Next(_minimumWaitTime, _maximumWaitTime));
                }

                var openHexes =
                    Board
                        .Hexes
                        .Where(x => x.Owner == PlayerNumber.Unowned)
                        .ToList();
                var indexToChoose = randomGenerator.Next(openHexes.Count());
                var moveToMake = openHexes[indexToChoose];
                if (moveToMake != null)
                {
                    moveToMake.SetOwner(Number);
                    return new Move(moveToMake.Coordinates, Number);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return ConcedeGame();
        }

        private Move ConcedeGame()
        {
            return new Move(new Coordinates(-1, -1), Number);
        }

        private void SetOpponentPosition(Coordinates opponentMove)
        {
            if (!opponentMove.Equals(new Coordinates(-1, -1)))
            {
                Board
                    .Hexes
                    .FirstOrDefault(x =>
                        x.Coordinates.Equals(opponentMove))?
                    .SetOwner(PlayerNumber.FirstPlayer);
            }
        }
    }
}
