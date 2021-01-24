using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine.ValueTypes;
using NUnit.Framework;

namespace Engine.Tests
{
    public class BoardTests
    {
        [Test]
        public void GetNeighboursAt_ShouldReturnSixNeighbours_WhenInTheMiddleOfTheBoard()
        {
            // Assemble
            var board = new Board(11);

            // Act
            var neighbours = board.GetNeighboursOf(5, 5);

            // Assert
            Assert.AreEqual(6, neighbours.Count);
        }

        [Test]
        public void GetNeighbourAt_ShouldReturnThreeNeighbours_WhenAtTopLeft()
        {
            // Assemble
            var board = new Board(11);

            // Act
            var neighbours = board.GetNeighboursOf(0, 0);

            // Assert
            Assert.AreEqual(2, neighbours.Count);
        }

        [Test]
        public void GetNeighboursAt_ShouldReturnEmptyList_WhenHexOffBoard()
        {
            // Assemble
            var board = new Board(11);

            // Act
            var neighbours = board.GetNeighboursOf(-1, -1);

            // Assert
            Assert.IsEmpty(neighbours);
        }

        [Test]
        public void GetAt_ShouldReturnNull_WhenCoordinatesOffBoard()
        {
            // Assemble
            var board = new Board(11);

            // Act
            var hex = board.HexAt(-1, -1);

            // Assert
            Assert.IsNull(hex);
        }

        [Test]
        public void GetFriendlyNeighbours_ShouldGetTwoNeighbours_WhenThereAreTwo()
        {
            // Assemble
            var board = new Board(11);
            board.HexAt(3,3).SetOwner(PlayerNumber.FirstPlayer);
            board.HexAt(3,4).SetOwner(PlayerNumber.FirstPlayer);
            board.HexAt(3, 2).SetOwner(PlayerNumber.FirstPlayer);

            // Act
            var friendlyNeighbours = board.GetFriendlyNeighboursOf(3, 3);

            // Assert
            Assert.AreEqual(2, friendlyNeighbours.Count);
        }

        [Test]
        public void GetFriendlyNeighbours_ShouldGetEmptyList_WhenHexIsOutOfBounds()
        {
            // Assemble
            var board = new Board(11);

            // Act
            var friendlyNeighbours = board.GetFriendlyNeighboursOf(-1, -1);

            // Assert
            Assert.IsEmpty(friendlyNeighbours);
        }

        [Test]
        public void ClaimHex_ShouldAlterOnlyOneHex_WhenHexWasEmpty()
        {
            // Assemble
            var board = new Board(11);

            // Act
            board.ClaimHex(3,3, PlayerNumber.FirstPlayer);

            // Assert
            Assert.AreEqual(board.Hexes.Count, board.Hexes.Count(x => x.Owner == PlayerNumber.Unowned) + 1);
            Assert.AreEqual(1, board.Hexes.Count(x => x.Owner == PlayerNumber.FirstPlayer));
            Assert.AreEqual(PlayerNumber.FirstPlayer, board.HexAt(3,3).Owner);
        }

        [Test]
        public void ReleaseHex_ShouldAlterOnlyOneHex_WhenHexWasOccupied()
        {
            // Assemble
            var board = new Board(11);
            board.HexAt(3,3).SetOwner(PlayerNumber.FirstPlayer);
            Assume.That(board.Hexes.Count(x => x.Owner != PlayerNumber.FirstPlayer), Is.EqualTo(120));

            // Act
            board.ReleaseHex(3, 3);

            // Assert
            Assert.AreEqual(121, board.Hexes.Count(x => !x.IsOwned));
        }

        [Test]
        public void UnownedHexes_ShouldReturnAllHexes_ThatAreUnowned()
        {
            // Assemble
            var board = new Board(11);
            board.HexAt(3,3).SetOwner(PlayerNumber.FirstPlayer);
            Assume.That(board.UnownedHexes().Count, Is.EqualTo(120));

            // Act
            board.HexAt(3, 4).SetOwner(PlayerNumber.FirstPlayer);

            // Assert
            Assert.AreEqual(119, board.UnownedHexes().Count);

        }

        [Test]
        public void Player1Hexes_ShouldReturnAllHexes_ThatAreOwnedByPlayer1()
        {
            // Assemble
            var board = new Board(11);
            board.HexAt(3, 3).SetOwner(PlayerNumber.FirstPlayer);
            Assume.That(board.Player1Hexes().Count, Is.EqualTo(1));

            // Act
            board.HexAt(3, 4).SetOwner(PlayerNumber.FirstPlayer);

            // Assert
            Assert.AreEqual(2, board.Player1Hexes().Count);

        }

        [Test]
        public void Player2Hexes_ShouldReturnAllHexes_ThatAreOwnedByPlayer2()
        {
            // Assemble
            var board = new Board(11);
            board.HexAt(3, 3).SetOwner(PlayerNumber.SecondPlayer);
            Assume.That(board.Player2Hexes().Count, Is.EqualTo(1));

            // Act
            board.HexAt(3, 4).SetOwner(PlayerNumber.SecondPlayer);

            // Assert
            Assert.AreEqual(2, board.Player2Hexes().Count);

        }

    }
}
