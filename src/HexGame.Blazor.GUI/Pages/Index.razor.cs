using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Engine;
using Engine.Players;
using Engine.ValueTypes;
using HexGame.Blazor.GUI.Shared;
using Microsoft.AspNetCore.Components;

namespace HexGame.Blazor.GUI.Pages
{
    public partial class Index
    {
        public List<Move> Moves { get; set; } = new List<Move>();
        public PlayerNumber Winner { get; private set; } = PlayerNumber.Unowned;

        public void StartGame()
        {
            var boardSize = 11;

            var player1 = PlayerFactory
                .Init()
                .NewOfType("RandomPlayer")
                .AsPlayerOne()
                .ForBoardSize(boardSize)
                .Build();

            var player2 = PlayerFactory
                .Init()
                .NewOfType("RandomPlayer")
                .AsPlayerTwo()
                .ForBoardSize(boardSize)
                .Build();

            var game = new Game(boardSize, player1, player2);

            game.StartGame();

            Moves = game.Moves;
            Winner = game.Winner;
        }
    }
}
