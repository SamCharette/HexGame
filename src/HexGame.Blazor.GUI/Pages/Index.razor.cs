using System;
using System.Collections.Generic;
using Engine;
using Engine.Players;
using Engine.ValueTypes;
using HexGame.Blazor.GUI.Shared;
using Microsoft.JSInterop;

namespace HexGame.Blazor.GUI.Pages
{
    public partial class Index
    {
        public MoveList MoveList { get; set; }
        private bool _gameHasStarted = false;
        private bool _gameHasCompleted = false;

        private PlayerNumber _winner = PlayerNumber.Unowned;

        
        public Game game { get; set; }


        protected override void OnInitialized()
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
        }

        public void MoveWasMade(Move move)
        {
            this.MoveList.AddMove(move);
            JsRuntime.InvokeAsync<string>("console.log", "Move was made: " + move);
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
            MoveList = new MoveList();
            JsRuntime.InvokeAsync<string>("console.log", "Game has started...");
        }

        public void StartGame()
        {
            game.StartGame();
        }
    }
}
