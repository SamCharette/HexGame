using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Players;
using Engine.Tools;
using Engine.ValueTypes;
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
            var player1 = new RandomPlayer(new PlayerConstructorArguments(size, PlayerNumber.FirstPlayer, "{\"Name\":\"Random\"}"));
            var player2 = new RandomPlayer(new PlayerConstructorArguments(size, PlayerNumber.SecondPlayer, "{\"Name\":\"Randomer\"}"));
            var game = new Game(size, player1, player2);

            // Act
            game.StartGame();
            var json = game.ToJson();

            // Assert
            var validator = new JsonValidator<Game>();
            Assert.IsTrue(validator.IsValid(json));

        }


        [Test]
        public void GameConstructor_ShouldProperlyAssignPlayers()
        {
            // Assemble
            var size = 5;
            var player1 = new TestPlayer(new PlayerConstructorArguments(size, PlayerNumber.FirstPlayer));
            var player2 = new TestPlayer(new PlayerConstructorArguments(size, PlayerNumber.SecondPlayer));

            // Act
            var game = new Game(size, player1, player2);

            // Assert
            Assert.AreEqual(player1, game.Player1);
            Assert.AreNotEqual(player2, game.Player1);
        }


        [Test]
        public void IsGameOver_ShouldBeFalse_WhenGameStarts()
        {
            // Assemble
            var size = 5;
            var player1 = new TestPlayer(new PlayerConstructorArguments(size, PlayerNumber.FirstPlayer));
            var player2 = new TestPlayer(new PlayerConstructorArguments(size, PlayerNumber.SecondPlayer));

            // Act
            var game = new Game(size, player1, player2);

            // Assert
            Assert.IsFalse(game.IsGameOver());

        }

        [Test]
        public void IsGameOver_ShouldBeTrue_WhenThereIsAWinner()
        {
            // Assemble
            var size = 5;
            var player1 = new TestPlayer(new PlayerConstructorArguments(size, PlayerNumber.FirstPlayer));
            var player2 = new TestPlayer(new PlayerConstructorArguments(size, PlayerNumber.SecondPlayer));

            // Act
            var game = new Game(size, player1, player2);
            game.StartGame();

            // Assert
            Assert.IsTrue(game.IsGameOver());

        }

        [Test]
        public void StartGame_ShouldFillTheBoard_WhenRandomPlayersPlay()
        {
            // Assemble
            var size = 5;
            var player1 = new TestPlayer(new PlayerConstructorArguments(size, PlayerNumber.FirstPlayer));
            var player2 = new TestPlayer(new PlayerConstructorArguments(size, PlayerNumber.SecondPlayer));

            player1.Moves.Add(new Coordinates(0, 0));
            player1.Moves.Add(new Coordinates(0, 1));
            player1.Moves.Add(new Coordinates(0, 2));
            player1.Moves.Add(new Coordinates(0, 3));
            player1.Moves.Add(new Coordinates(0, 4));

            Assume.That(player1.Moves.Count, Is.EqualTo(5));

            player2.Moves.Add(new Coordinates(2, 0));
            player2.Moves.Add(new Coordinates(3, 0));
            player2.Moves.Add(new Coordinates(4, 0));
            player2.Moves.Add(new Coordinates(2, 3));
            player2.Moves.Add(new Coordinates(2, 2));

            Assume.That(player2.Moves.Count, Is.EqualTo(5));

            var game = new Game(size, player1, player2);
            Assume.That(game.Moves.Count, Is.EqualTo(0));
            Assume.That(
                game.Board.Hexes.Count(x => x.Owner == PlayerNumber.Unowned),
                Is.EqualTo(size * size));

            // Act
            game.StartGame();
            var unownedSpaces = game
                .Board
                .Hexes
                .Count(x => x.Owner == PlayerNumber.Unowned);

            // Assert
            Assert.IsTrue(game.EndedOn > game.StartedOn);
            Assert.AreEqual(PlayerNumber.FirstPlayer, game.Winner);
            Assert.AreEqual((size * size) - 9, unownedSpaces);
            Assert.AreEqual(4,
                game
                    .Moves
                    .Count(x => x.PlayerNumber == PlayerNumber.SecondPlayer));
            Assert.AreEqual(9, game.Moves.Count);
        }

        [Test]
        public void StartGame_ShouldFailWithException_WhenBoardIsTooSmall()
        {
            var size = 11;
            var player1 = new RandomPlayer(new PlayerConstructorArguments (11, PlayerNumber.FirstPlayer ));
            var player2 = new RandomPlayer(new PlayerConstructorArguments (11, PlayerNumber.SecondPlayer ));

            Assert.Throws<ArgumentException>( () => new Game(1, player1, player2));
        }

        [Test]
        public void StartGame_ShouldFailWithException_WhenPlayer1IsNull()
        {
            var size = 11;
            var player2 = new RandomPlayer(new PlayerConstructorArguments (11, PlayerNumber.SecondPlayer ));

            Assert.Throws<ArgumentException>(() => new Game(1, null, player2));
        }

        [Test]
        public void StartGame_ShouldFailWithException_WhenPlayer2IsNull()
        {
            var size = 11;
            var player1 = new RandomPlayer(new PlayerConstructorArguments (11, PlayerNumber.FirstPlayer ));

            Assert.Throws<ArgumentException>(() => new Game(1, player1, null));
        }
    }
}