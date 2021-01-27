using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine.ValueTypes;
using FluentAssertions;
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
            neighbours.Should().HaveCount(6, "because the middle hexes are surrounded on 6 sides");

        }

        [Test]
        public void GetNeighbourAt_ShouldReturnThreeNeighbours_WhenAtTopLeft()
        {
            // Assemble
            var board = new Board(11);

            // Act
            var neighbours = board.GetNeighboursOf(0, 0);

            // Assert
            neighbours.Should().HaveCount(2, "because the origin hex at 0,0 only has two neighbours");
        }

        [Test]
        public void GetNeighboursAt_ShouldReturnEmptyList_WhenHexOffBoard()
        {
            // Assemble
            var board = new Board(11);

            // Act
            var neighbours = board.GetNeighboursOf(-1, -1);

            // Assert
            neighbours.Should().BeEmpty("because that location is not on the board.");
        }

        [Test]
        public void GetAt_ShouldReturnNull_WhenCoordinatesOffBoard()
        {
            // Assemble
            var board = new Board(11);

            // Act
            var hex = board.HexAt(-1, -1);

            // Assert
            hex.Should().BeNull("because that hex does not exist on the board");
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
            friendlyNeighbours
                .Should()
                .HaveCount(2, "because that is how many friendly neighbours we set up");
        }

        [Test]
        public void GetFriendlyNeighbours_ShouldGetEmptyList_WhenHexIsOutOfBounds()
        {
            // Assemble
            var board = new Board(11);

            // Act
            var friendlyNeighbours = board.GetFriendlyNeighboursOf(-1, -1);

            // Assert
            friendlyNeighbours.Should().BeEmpty("because that hex doesn't exist");
        }

        [Test]
        public void ClaimHex_ShouldAlterOnlyOneHex_WhenHexWasEmpty()
        {
            // Assemble
            var board = new Board(11);
            board.Hexes.Where(x => !x.IsOwned).Should().HaveCount(121, "because it's a new 11x11 board");

            // Act
            board.ClaimHex(3,3, PlayerNumber.FirstPlayer);

            // Assert
            board
                .Hexes
                .Where(x => !x.IsOwned)
                .Should()
                .HaveCount(120, "because now one is owned");
            board
                .Hexes
                .Where(x => x.IsOwned)
                .Should()
                .HaveCount(1, "because one is now owned");
            board
                .HexAt(3,3)
                .Owner
                .Should()
                .BeEquivalentTo(PlayerNumber.FirstPlayer, "because that's who now owns the hex"); 

        }

        [Test]
        public void ReleaseHex_ShouldAlterOnlyOneHex_WhenHexWasOccupied()
        {
            // Assemble
            var board = new Board(11);
            board.HexAt(3,3).SetOwner(PlayerNumber.FirstPlayer);
            board.Hexes.Where(x => !x.IsOwned).Should().HaveCount(120, "because only one is owned");

            // Act
            board.ReleaseHex(3, 3);

            // Assert
            board.Hexes.Where(x => !x.IsOwned).Should().HaveCount(121, "because now all are free");
        }

        [Test]
        public void UnownedHexes_ShouldReturnAllHexes_ThatAreUnowned()
        {
            // Assemble
            var board = new Board(11);
            board.HexAt(3,3).SetOwner(PlayerNumber.FirstPlayer);
            board.UnownedHexes().Should().HaveCount(120, "because we have one owned");

            // Act
            board.HexAt(3, 4).SetOwner(PlayerNumber.FirstPlayer);

            // Assert
            board.UnownedHexes().Should().HaveCount(119, "because now two are owned");

        }

        [Test]
        public void Player1Hexes_ShouldReturnAllHexes_ThatAreOwnedByPlayer1()
        {
            // Assemble
            var board = new Board(11);
            board.HexAt(3, 3).SetOwner(PlayerNumber.FirstPlayer);
            board.Player1Hexes().Should().HaveCount(1, "because player 1 has only one hex");

            // Act
            board.HexAt(3, 4).SetOwner(PlayerNumber.FirstPlayer);

            // Assert
            board.Player1Hexes().Should().HaveCount(2, "because now two are owned by player 1");

        }

        [Test]
        public void Player2Hexes_ShouldReturnAllHexes_ThatAreOwnedByPlayer2()
        {
            // Assemble
            var board = new Board(11);
            board.HexAt(3, 3).SetOwner(PlayerNumber.SecondPlayer);
            board.Player2Hexes().Should().HaveCount(1, "because player 2 has only one hex");

            // Act
            board.HexAt(3, 4).SetOwner(PlayerNumber.SecondPlayer);

            // Assert
            board.Player2Hexes().Should().HaveCount(2, "because now player 2 owns 2 hexes");
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
            areConnected.Should().BeTrue("because the hexes are connected");
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
            areConnected.Should().BeTrue("because the hexes are connected");
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
            areConnected.Should().BeTrue("because the hexes are connected");
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
            areConnected.Should().BeFalse("because they are not connected");
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
            areConnected.Should().BeTrue("because a hex is considered connected to itself");
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
            test1.Should().BeFalse("because the finish hex is not on the board");
            test2.Should().BeFalse("because the start hex is not on the board");
            test3.Should().BeFalse("because the finish hex is null");
            test4.Should().BeFalse("because the start hex is null");

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
            areConnected.Should().BeFalse("because the hexes simply have different owners");
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
            isStartHex.Should().BeTrue("because the y coordinate is on the starting line for player 1");
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
            isStartHex.Should().BeTrue("because the x coordinate is on the starting line for player 2");
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
            isStartHex.Should().BeFalse("because that hex doesn't exist on the board");
            Assert.False(isStartHex);
        }

        [Test]
        public void IsEndingHexForPlayer_ShouldReturnTrue_IfPlayer1AndYIsAppropriate()
        {
            // Assemble
            var board = new Board(11);
            var hex = board.HexAt(5, 10);

            // Act
            var isEndingHex = board.IsEndHexForPlayer(hex, PlayerNumber.FirstPlayer);

            // Assert
            isEndingHex.Should().BeTrue("because the Y coordinate is on the finish line on this board for player 1");
        }

        [Test]
        public void IsEndingHexForPlayer_ShouldReturnTrue_IfPlayer2AndXIsAppropriate()
        {
            // Assemble
            var board = new Board(11);
            var hex = board.HexAt(10, 5);

            // Act
            var isEndingHex = board.IsEndHexForPlayer(hex, PlayerNumber.SecondPlayer);

            // Assert
            isEndingHex.Should().BeTrue("because the x coordinate is on the finish line on this board for player 2");
        }

        [Test]
        public void IsEndingHexForPlayer_ShouldReturnFalse_HexIsNotOnTheBoard()
        {
            // Assemble
            var board = new Board(11);
            var hex = new Hex(-1, 55);

            // Act
            var isEndingHex = board.IsEndHexForPlayer(hex, PlayerNumber.SecondPlayer);

            // Assert
            isEndingHex.Should().BeFalse("because the hex is not on the board");
        }

        [Test]
        public void DoesWinningPathExistForPlayer_ShouldReturnFalse_IfLookingAtUnownedPlayer()
        {   
            // Assemble
            var board = new Board(11);

            // Act
            var pathExists = board.WinningPathExistsForPlayer(PlayerNumber.Unowned);

            // Assert
            pathExists.Should().BeFalse("because player is unowned, and can't have a path");
        }

        [Test]
        public void DoesWinningPathExistForPlayer_ShouldReturnFalse_IfPlayerHasNoPath()
        {
            // Assemble
            var board = new Board(11);

            // Act
            var pathExists = board.WinningPathExistsForPlayer(PlayerNumber.SecondPlayer);

            // Assert
            pathExists.Should().BeFalse("because there is not a path for this player between these hexes");
        }

        [Test]
        public void DoesWinningPathExistForPlayer_ShouldReturnFalse_IfPlayerOnlyStartAndEndHexes()
        {
            // Assemble
            var board = new Board(11);
            board.HexAt(0,0).SetOwner(PlayerNumber.FirstPlayer);
            board.HexAt(0,10).SetOwner(PlayerNumber.FirstPlayer);

            // Act
            var pathExists = board.WinningPathExistsForPlayer(PlayerNumber.FirstPlayer);

            // Assert
            pathExists.Should().BeFalse("because only the start and end hexes are owned, no path inbetween");
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
            var pathExists = board.WinningPathExistsForPlayer(PlayerNumber.FirstPlayer);

            // Assert
            pathExists.Should().BeTrue("because a path for this player exists");
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
            var pathExists = board.WinningPathExistsForPlayer(PlayerNumber.SecondPlayer);

            // Assert
            pathExists.Should().BeTrue("because even if it is a windy path, a path exists in this scenario");
        }
    }
}
