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
            var player1Args = new PlayerConstructorArguments(11, PlayerNumber.FirstPlayer);
            var player1 = new PlayerFactory().CreatePlayerOfType("RandomPlayer", player1Args);

            var player2Args = new PlayerConstructorArguments(11, PlayerNumber.SecondPlayer);
            var player2 = new PlayerFactory().CreatePlayerOfType("RandomPlayer", player2Args);

            var game = new Game(11, player1, player2);

            game.StartGame();

            Moves = game.Moves;
            Winner = game.Winner;
        }
    }
}
