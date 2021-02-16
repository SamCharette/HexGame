using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Engine;
using Engine.Players;
using Engine.ValueTypes;
using HexGame.Blazor.GUI.Shared;
using Microsoft.JSInterop;

namespace HexGame.Blazor.GUI.Pages
{
    public partial class Index
    {
        public List<Move> Moves { get; set; } = new List<Move>();
        private bool _gameHasStarted = false;
        private bool _gameHasCompleted = false;

        private PlayerNumber _winner = PlayerNumber.Unowned;

        
        public Game game { get; set; }


        protected override Task OnInitializedAsync()
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

            game.OnGameStart += GameHasStarted;
            game.OnMoveMade += (sender, move) => MoveWasMade(move);
            game.OnGameEnd += GameHasEnded;

            return base.OnInitializedAsync();
        }

        public async void MoveWasMade(Move move)
        {
            Moves.Add(move);
            JsRuntime.InvokeAsync<string>("console.log", "Move was made: " + move);
            await InvokeAsync(() =>
            {
                StateHasChanged();
            });
        }

        public void GameHasEnded()
        {
            _winner = game.Winner;
            _gameHasCompleted = true;
            JsRuntime.InvokeAsync<string>("console.log", "Game ended.  " + _winner + " won.");
        }

        public void GameHasStarted()
        {
            _gameHasCompleted = false;
            _gameHasStarted = true;
            Moves = new List<Move>();
            _winner = PlayerNumber.Unowned;
            JsRuntime.InvokeAsync<string>("console.log", "Game has started...");
        }

        public async void StartGame()
        {
            var gameThread = new Thread(game.StartGame);
            gameThread.Start();
        }
    }
}
