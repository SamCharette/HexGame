using System;
using System.Collections.Generic;
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

    }
}
