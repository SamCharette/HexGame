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
            var game = new Game(
                11, 
                new Player(PlayerNumber.FirstPlayer, 11), 
                new Player(PlayerNumber.SecondPlayer, 11));
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
            Assert.Throws<ArgumentException>( () => new Game(1, new Player(PlayerNumber.FirstPlayer, 1), new Player(PlayerNumber.SecondPlayer, 1)));
            
        }
    }
}