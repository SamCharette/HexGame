using System.Collections.Generic;
using Engine;
using Engine.Players;
using Engine.ValueTypes;

namespace HexGame.Blazor.GUI.Pages
{
    public partial class Index
    {
        public List<Move> Moves { get; set; } = new List<Move>();
        public PlayerNumber Winner { get; private set; } = PlayerNumber.Unowned;

        public Game game { get; set; }

        public void StartGame()
        {
            var boardSize = 11;

            game = GameBuilder
                .New()
                .WithBoardSize(boardSize)
                .WithPlayerOne(
                    PlayerBuilder
                        .New()
                        .OfType("RandomPlayer")
                        .AsPlayerOne()
                        .ForBoardSize(boardSize)
                        .WithConfiguration(null)
                        .Build()
                )
                .WithPlayerTwo(
                    PlayerBuilder
                        .New()
                        .OfType("RandomPlayer")
                        .AsPlayerTwo()
                        .ForBoardSize(boardSize)
                        .WithConfiguration(null)
                        .Build()
                )
                .Build();


            game.StartGame();

            Moves = game.Moves;
            Winner = game.Winner;
        }
    }
}
