using System;
using System.Linq;
using Engine.Players;
using Engine.ValueTypes;
using NUnit.Framework;

namespace Engine.Tests
{
    public class GameTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void StartGame_ShouldFillTheBoard_WhenRandomPlayersPlay()
        {
            // Assemble
            var player1 = new RandomPlayer(new PlayerConstructorArguments { BoardSize = 11, PlayerNumber = PlayerNumber.FirstPlayer });
            var player2 = new RandomPlayer(new PlayerConstructorArguments { BoardSize = 11, PlayerNumber = PlayerNumber.SecondPlayer });
            var game = new Game(11, player1, player2);
            Assume.That(game.Moves.Count, Is.EqualTo(0));
            Assume.That(
                game.Board.Hexes.Count(x => x.Owner == PlayerNumber.Unowned), 
                Is.EqualTo(121));

            // Act
            game.StartGame();
            var unownedSpaces = game
                .Board
                .Hexes
                .Count(x => x.Owner == PlayerNumber.Unowned);

            // Assert
            Assert.AreEqual(0, unownedSpaces);
            Assert.AreEqual(121, game.Moves.Count);
        }

        [Test]
        public void StartGame_ShouldFailWithException_WhenBoardIsTooSmall()
        {
            var size = 11;
            var player1 = new RandomPlayer(new PlayerConstructorArguments { BoardSize = 11, PlayerNumber = PlayerNumber.FirstPlayer });
            var player2 = new RandomPlayer(new PlayerConstructorArguments { BoardSize = 11, PlayerNumber = PlayerNumber.SecondPlayer });

            Assert.Throws<ArgumentException>( () => new Game(1, player1, player2));
        }

        [Test]
        public void StartGame_ShouldFailWithException_WhenPlayer1IsNull()
        {
            var size = 11;
            var player2 = new RandomPlayer(new PlayerConstructorArguments { BoardSize = 11, PlayerNumber = PlayerNumber.SecondPlayer });

            Assert.Throws<ArgumentException>(() => new Game(1, null, player2));
        }

        [Test]
        public void StartGame_ShouldFailWithException_WhenPlayer2IsNull()
        {
            var size = 11;
            var player1 = new RandomPlayer(new PlayerConstructorArguments { BoardSize = 11, PlayerNumber = PlayerNumber.FirstPlayer });

            Assert.Throws<ArgumentException>(() => new Game(1, player1, null));
        }
    }
}