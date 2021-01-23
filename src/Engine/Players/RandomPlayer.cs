using System;
using System.Linq;
using Engine.ValueTypes;

namespace Engine.Players
{
    public class RandomPlayer : PlayerBase<PlayerConstructorArguments>
    {
        private Board Board;

        public RandomPlayer(PlayerConstructorArguments args) : base(args)
        {
            Number = args.PlayerNumber;
            Board = new Board(args.BoardSize);
        }

        public override Coordinates MakeMove(Coordinates opponentMove)
        {
            try
            {
                SetOpponentPosition(opponentMove);
                var randomGenerator = new Random();
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
                    return moveToMake.Coordinates;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return ConcedeGame();
        }

        private Coordinates ConcedeGame()
        {
            return new Coordinates(-1, -1);
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
