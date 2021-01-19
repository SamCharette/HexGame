using System;
using System.Linq;
using NUnit.Framework;
using Engine;
using Engine.ValueTypes;

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
            var player1 = new Player(PlayerNumber.FirstPlayer, 11);
            var player2 = new Player(PlayerNumber.SecondPlayer, 11);
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
            var player1 = new Player(PlayerNumber.FirstPlayer, size);
            var player2 = new Player(PlayerNumber.SecondPlayer, size);

            Assert.Throws<ArgumentException>( () => new Game(1, player1, player2));
        }

        [Test]
        public void StartGame_ShouldFailWithException_WhenPlayer1IsNull()
        {
            var size = 11;
            var player2 = new Player(PlayerNumber.SecondPlayer, size);

            Assert.Throws<ArgumentException>(() => new Game(1, null, player2));
        }

        [Test]
        public void StartGame_ShouldFailWithException_WhenPlayer2IsNull()
        {
            var size = 11;
            var player1 = new Player(PlayerNumber.FirstPlayer, size);

            Assert.Throws<ArgumentException>(() => new Game(1, player1, null));
        }
    }
}