using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Engine.Players;
using Engine.Tools;
using Engine.ValueTypes;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using NUnit.Framework;


namespace Engine.Tests
{
    public class GameTests
    {
        
        [Test]
        public void ToJson_ShouldProduceValidJson()
        {
            // Assemble
            var size = 11;

            var player1 = PlayerFactory
                .Init()
                .NewOfType("RandomPlayer")
                .AsPlayerOne()
                .ForBoardSize(size)
                .Build();

            var player2 = PlayerFactory
                .Init()
                .NewOfType("RandomPlayer")
                .AsPlayerTwo()
                .ForBoardSize(size)
                .Build();


            var game = new Game(size, player1, player2);

            // Act
            game.StartGame();
            var json = game.ToJson();

            // Assert
            var validator = new JsonValidator<Game>();
            validator.IsValid(json).Should().BeTrue("because the serialization should work");

        }


        [Test]
        public void GameConstructor_ShouldProperlyAssignPlayers()
        {
            // Assemble
            var size = 5;
           
            var player1 = PlayerFactory
                .Init()
                .NewOfType("TestPlayer")
                .AsPlayerOne()
                .ForBoardSize(size)
                .Build();

            var player2 = PlayerFactory
                .Init()
                .NewOfType("TestPlayer")
                .AsPlayerTwo()
                .ForBoardSize(size)
                .Build();


            // Act
            var game = new Game(size, player1, player2);

            // Assert
            game.Player1.Should().BeEquivalentTo(player1, "because they are the same");
            game.Player2.Should().NotBeEquivalentTo(player1, "because player 2 is not player 1");
        }


        [Test]
        public void IsGameOver_ShouldBeFalse_WhenGameStarts()
        {
            // Assemble
            var size = 5;
            var player1 = PlayerFactory
                .Init()
                .NewOfType("TestPlayer")
                .AsPlayerOne()
                .ForBoardSize(size)
                .Build();

            var player2 = PlayerFactory
                .Init()
                .NewOfType("TestPlayer")
                .AsPlayerTwo()
                .ForBoardSize(size)
                .Build();

            // Act
            var game = new Game(size, player1, player2);

            // Assert
            game.IsGameOver().Should().BeFalse("because the game hasn't started");

        }

        [Test]
        public void IsGameOver_ShouldBeTrue_WhenThereIsAWinner()
        {
            // Assemble
            var size = 5;
           
            var player1 = PlayerFactory
                .Init()
                .NewOfType("TestPlayer")
                .AsPlayerOne()
                .ForBoardSize(size)
                .Build();

            var player2 = PlayerFactory
                .Init()
                .NewOfType("TestPlayer")
                .AsPlayerTwo()
                .ForBoardSize(size)
                .Build();

            // Act
            var game = new Game(size, player1, player2);
            game.StartGame();

            // Assert
            game.IsGameOver().Should().BeTrue("because with no moves, a test player forfeits the game");

        }

        [Test]
        public void StartGame_ShouldFillTheBoard_WhenRandomPlayersPlay()
        {
            // Assemble
            var size = 5;
            var player1 = PlayerFactory
                .Init()
                .NewOfType("TestPlayer")
                .AsPlayerOne()
                .ForBoardSize(size)
                .Build() as TestPlayer;
            
            var player2 = PlayerFactory
                .Init()
                .NewOfType("TestPlayer")
                .AsPlayerTwo()
                .ForBoardSize(size)
                .Build() as TestPlayer;
            
            player1
                .AddMove(0, 0)
                .AddMove(0, 1)
                .AddMove(0, 2)
                .AddMove(0, 3)
                .AddMove(0, 4);

            Assume.That(player1.Moves.Count, Is.EqualTo(5));

            player2
                .AddMove(2, 0)
                .AddMove(3, 0)
                .AddMove(4, 0)
                .AddMove(2, 3)
                .AddMove(2, 2);
            
            Assume.That(player2.Moves.Count, Is.EqualTo(5));

            var game = new Game(size, player1, player2);
            game.Moves.Should().HaveCount(0, "because the game hasn't started yet");
            game.Board.UnownedHexes().Should().HaveCount(size * size, "because the game hasn't begun");

            // Act
            game.StartGame();
            var unownedSpaces = game
                .Board
                .Hexes
                .Count(x => x.Owner == PlayerNumber.Unowned);

            // Assert
            (game.EndedOn > game.StartedOn).Should().BeTrue("because the game is over");
            game.Winner.Should().BeEquivalentTo(PlayerNumber.FirstPlayer, "because player 1 won the game");
            game.Board.UnownedHexes().Should()
                .HaveCount((size * size) - 9, "because 9 moves were made during the game");
            game.Moves.Where(x => x.PlayerNumber == PlayerNumber.SecondPlayer).Should()
                .HaveCount(4, "because the second player only made 4 moves");
            game.Moves.Should().HaveCount(9, "because 9 moves were made in total");
        }

        [Test]
        public void StartGame_ShouldFailWithException_WhenBoardIsTooSmall()
        {
            var size = 11;
            var player1 = PlayerFactory
                .Init()
                .NewOfType("RandomPlayer")
                .AsPlayerOne()
                .ForBoardSize(size)
                .Build();

            var player2 = PlayerFactory
                .Init()
                .NewOfType("RandomPlayer")
                .AsPlayerTwo()
                .ForBoardSize(size)
                .Build();
            Assert.Throws<ArgumentException>( () => new Game(1, player1, player2));
        }

        [Test]
        public void StartGame_ShouldFailWithException_WhenPlayer1IsNull()
        {
            var size = 11;
  
            var player2 = PlayerFactory
                .Init()
                .NewOfType("RandomPlayer")
                .AsPlayerTwo()
                .ForBoardSize(size)
                .Build();
  
            Action act = () => new Game(1, null, player2);
            act.Should().Throw<ArgumentException>("because player 1 is null");
        }

        [Test]
        public void StartGame_ShouldFailWithException_WhenPlayer2IsNull()
        {
            var size = 11;
            var player1 = PlayerFactory
                .Init()
                .NewOfType("RandomPlayer")
                .AsPlayerOne()
                .ForBoardSize(size)
                .Build();

            Action act = () => new Game(1, null, player1);
            act.Should().Throw<ArgumentException>("because player 2 is null");
        }
    }
}