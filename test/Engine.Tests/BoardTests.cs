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

        [Test]
        public void AreConnected_ShouldReturnTrue_WhenSameOwnerAndNeighbours()
        {
            // Assemble
            var board = new Board(11);
            var hex1 = board.HexAt(3, 3);
            var hex2 = board.HexAt(3, 4);
            hex1.SetOwner(PlayerNumber.FirstPlayer);
            hex2.SetOwner(PlayerNumber.FirstPlayer);

            // Act
            var areConnected = board.AreConnected(hex1,hex2);

            // Assert
            Assert.IsTrue(areConnected);
        }

        [Test]
        public void AreConnected_ShouldReturnTrue_WhenConnectedViaMultipleHexes()
        {
            // Assemble
            var board = new Board(11);
            var hex1 = board.HexAt(3, 3);
            var hex2 = board.HexAt(3, 4);
            var hex3 = board.HexAt(3, 5);
            hex1.SetOwner(PlayerNumber.FirstPlayer);
            hex2.SetOwner(PlayerNumber.FirstPlayer);
            hex3.SetOwner(PlayerNumber.FirstPlayer);

            // Act
            var areConnected = board.AreConnected(hex1, hex3);

            // Assert
            Assert.IsTrue(areConnected);
        }

        [Test]
        public void AreConnected_ShouldReturnTrue_WhenConnectedViaMultipleHexesAgain()
        {
            // Assemble
            var board = new Board(11);
            var hex1 = board.HexAt(3, 3);
            var hex2 = board.HexAt(3, 4);
            var hex3 = board.HexAt(3, 5);
            var hex4 = board.HexAt(3, 6);
            var hex5 = board.HexAt(3, 7);
            hex1.SetOwner(PlayerNumber.FirstPlayer);
            hex2.SetOwner(PlayerNumber.FirstPlayer);
            hex3.SetOwner(PlayerNumber.FirstPlayer);
            hex4.SetOwner(PlayerNumber.FirstPlayer);
            hex5.SetOwner(PlayerNumber.FirstPlayer);

            // Act
            var areConnected = board.AreConnected(hex1, hex5);

            // Assert
            Assert.IsTrue(areConnected);
        }

        [Test]
        public void AreConnected_ShouldReturnFalse_WhenThereIsOneHexInTheWay()
        {
            // Assemble
            var board = new Board(11);
            var hex1 = board.HexAt(3, 3);
            var hex2 = board.HexAt(3, 4);
            var hex3 = board.HexAt(3, 5);
            var hex4 = board.HexAt(3, 6);
            var hex5 = board.HexAt(3, 7);
            hex1.SetOwner(PlayerNumber.FirstPlayer);
            hex2.SetOwner(PlayerNumber.FirstPlayer);
            hex3.SetOwner(PlayerNumber.SecondPlayer);
            hex4.SetOwner(PlayerNumber.FirstPlayer);
            hex5.SetOwner(PlayerNumber.FirstPlayer);

            // Act
            var areConnected = board.AreConnected(hex1, hex5);

            // Assert
            Assert.False(areConnected);
        }

        [Test]
        public void AreConnected_ShouldReturnTrue_WhenSameHex()
        {
            // Assemble
            var board = new Board(11);
            var hex = board.HexAt(3, 3);
            hex.SetOwner(PlayerNumber.FirstPlayer);

            // Act
            var areConnected = board.AreConnected(hex, hex);

            // Assert
            Assert.IsTrue(areConnected);
        }

        [Test]
        public void AreConnected_ShouldReturnFalse_WhenEitherAreNull()
        {
            // Assemble
            var board = new Board(11);
            var hex1 = board.HexAt(3, 3);
            var hex2 = board.HexAt(-1, -1);

            // Act
            var test1 = board.AreConnected(hex1, hex2);
            var test2 = board.AreConnected(hex2, hex1);
            var test3 = board.AreConnected(hex1, null);
            var test4 = board.AreConnected(null, hex1);

            // Assert
            Assert.IsFalse(test1);
            Assert.IsFalse(test2);
            Assert.IsFalse(test3);
            Assert.IsFalse(test4);

        }


        [Test]
        public void AreConnected_ShouldReturnFalse_WhenTheySimplyAreNotConnected()
        {
            // Assemble
            var board = new Board(11);
            var hex1 = board.HexAt(3, 3);
            var hex2 = board.HexAt(3, 4);
            hex1.SetOwner(PlayerNumber.FirstPlayer);
            hex2.SetOwner(PlayerNumber.SecondPlayer);

            // Act
            var areConnected = board.AreConnected(hex1, hex2);

            // Assert
            Assert.IsFalse(areConnected);
        }

        [Test]
        public void IsStartingHexForPlayer_ShouldReturnTrue_IfPlayer1AndYIsZero()
        {
            // Assemble
            var board = new Board(11);
            var hex = board.HexAt(5, 0);

            // Act
            var isStartHex = board.IsStartHexForPlayer(hex, PlayerNumber.FirstPlayer);

            // Assert
            Assert.IsTrue(isStartHex);
        }

        [Test]
        public void IsStartingHexForPlayer_ShouldReturnTrue_IfPlayer2AndXIsZero()
        {
            // Assemble
            var board = new Board(11);
            var hex = board.HexAt( 0, 5);

            // Act
            var isStartHex = board.IsStartHexForPlayer(hex, PlayerNumber.SecondPlayer);

            // Assert
            Assert.IsTrue(isStartHex);
        }

        [Test]
        public void IsStartingHexForPlayer_ShouldReturnFalse_HexIsNotOnTheBoard()
        {
            // Assemble
            var board = new Board(11);
            var hex = new Hex(-1, 55);

            // Act
            var isStartHex = board.IsStartHexForPlayer(hex, PlayerNumber.SecondPlayer);

            // Assert
            Assert.False(isStartHex);
        }

        [Test]
        public void IsEndingHexForPlayer_ShouldReturnTrue_IfPlayer1AndYIsAppropriate()
        {
            // Assemble
            var board = new Board(11);
            var hex = board.HexAt(5, 10);

            // Act
            var isStartHex = board.IsEndHexForPlayer(hex, PlayerNumber.FirstPlayer);

            // Assert
            Assert.IsTrue(isStartHex);
        }

        [Test]
        public void IsEndingHexForPlayer_ShouldReturnTrue_IfPlayer2AndXIsAppropriate()
        {
            // Assemble
            var board = new Board(11);
            var hex = board.HexAt(10, 5);

            // Act
            var isStartHex = board.IsEndHexForPlayer(hex, PlayerNumber.SecondPlayer);

            // Assert
            Assert.IsTrue(isStartHex);
        }

        [Test]
        public void IsEndingHexForPlayer_ShouldReturnFalse_HexIsNotOnTheBoard()
        {
            // Assemble
            var board = new Board(11);
            var hex = new Hex(-1, 55);

            // Act
            var isStartHex = board.IsEndHexForPlayer(hex, PlayerNumber.SecondPlayer);

            // Assert
            Assert.False(isStartHex);
        }

        [Test]
        public void DoesWinningPathExistForPlayer_ShouldReturnFalse_IfLookingAtUnownedPlayer()
        {   
            // Assemble
            var board = new Board(11);

            // Act
            var pathExists = board.DoesWinningPathExistForPlayer(PlayerNumber.Unowned);

            // Assert
            Assert.IsFalse(pathExists);
        }

        [Test]
        public void DoesWinningPathExistForPlayer_ShouldReturnFalse_IfPlayerHasNoPath()
        {
            // Assemble
            var board = new Board(11);

            // Act
            var pathExists = board.DoesWinningPathExistForPlayer(PlayerNumber.SecondPlayer);

            // Assert
            Assert.IsFalse(pathExists);
        }

        [Test]
        public void DoesWinningPathExistForPlayer_ShouldReturnFalse_IfPlayerOnlyStartAndEndHexes()
        {
            // Assemble
            var board = new Board(11);
            board.HexAt(0,0).SetOwner(PlayerNumber.FirstPlayer);
            board.HexAt(0,10).SetOwner(PlayerNumber.FirstPlayer);

            // Act
            var pathExists = board.DoesWinningPathExistForPlayer(PlayerNumber.FirstPlayer);

            // Assert
            Assert.IsFalse(pathExists);
        }

        [Test]
        public void DoesWinningPathExistForPlayer_ShouldReturnTrue_IfPlayerHasPath()
        {
            // Assemble
            var board = new Board(11);
            board.HexAt(0, 0).SetOwner(PlayerNumber.FirstPlayer);
            board.HexAt(0, 1).SetOwner(PlayerNumber.FirstPlayer);
            board.HexAt(0, 2).SetOwner(PlayerNumber.FirstPlayer);
            board.HexAt(0, 3).SetOwner(PlayerNumber.FirstPlayer);
            board.HexAt(0, 4).SetOwner(PlayerNumber.FirstPlayer);
            board.HexAt(0, 5).SetOwner(PlayerNumber.FirstPlayer);
            board.HexAt(0, 6).SetOwner(PlayerNumber.FirstPlayer);
            board.HexAt(0, 7).SetOwner(PlayerNumber.FirstPlayer);
            board.HexAt(0, 8).SetOwner(PlayerNumber.FirstPlayer);
            board.HexAt(0, 9).SetOwner(PlayerNumber.FirstPlayer);
            board.HexAt(0, 10).SetOwner(PlayerNumber.FirstPlayer);

            // Act
            var pathExists = board.DoesWinningPathExistForPlayer(PlayerNumber.FirstPlayer);

            // Assert
            Assert.IsTrue(pathExists);
        }

        [Test]
        public void DoesWinningPathExistForPlayer_ShouldReturnTrue_EvenIfPathIsWindy()
        {
            // Assemble
            var board = new Board(11);
            board.HexAt(0, 0).SetOwner(PlayerNumber.SecondPlayer);
            board.HexAt(0, 1).SetOwner(PlayerNumber.SecondPlayer);
            board.HexAt(1, 1).SetOwner(PlayerNumber.SecondPlayer);
            board.HexAt(2, 1).SetOwner(PlayerNumber.SecondPlayer);
            board.HexAt(2, 2).SetOwner(PlayerNumber.SecondPlayer);
            board.HexAt(2, 3).SetOwner(PlayerNumber.SecondPlayer);
            board.HexAt(3, 3).SetOwner(PlayerNumber.SecondPlayer);
            board.HexAt(4, 3).SetOwner(PlayerNumber.SecondPlayer);
            board.HexAt(5, 3).SetOwner(PlayerNumber.SecondPlayer);
            board.HexAt(5, 4).SetOwner(PlayerNumber.SecondPlayer);
            board.HexAt(5, 5).SetOwner(PlayerNumber.SecondPlayer);
            board.HexAt(5, 6).SetOwner(PlayerNumber.SecondPlayer);
            board.HexAt(5, 7).SetOwner(PlayerNumber.SecondPlayer);
            board.HexAt(5, 8).SetOwner(PlayerNumber.SecondPlayer);
            board.HexAt(5, 9).SetOwner(PlayerNumber.SecondPlayer);
            board.HexAt(5, 10).SetOwner(PlayerNumber.SecondPlayer);
            board.HexAt(6, 10).SetOwner(PlayerNumber.SecondPlayer);
            board.HexAt(7, 10).SetOwner(PlayerNumber.SecondPlayer);
            board.HexAt(8, 10).SetOwner(PlayerNumber.SecondPlayer);
            board.HexAt(9, 10).SetOwner(PlayerNumber.SecondPlayer);
            board.HexAt(10, 9).SetOwner(PlayerNumber.SecondPlayer);

            // Act
            var pathExists = board.DoesWinningPathExistForPlayer(PlayerNumber.SecondPlayer);

            // Assert
            Assert.IsTrue(pathExists);
        }
    }
}
