﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine.Players;
using Engine.ValueTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Engine.Tests
{
    public class TestPlayerTests
    {
        [Test]
        public void MakeMove_ShouldSendFirstMoveAndReduceTotalMoves_WhenMovesAreAvailable()
        {
            // Assemble
            var size = 5;
            var player1 = new TestPlayer(new PlayerConstructorArguments(size, PlayerNumber.FirstPlayer));

            player1.Moves.Add(new Coordinates(0, 0));
            player1.Moves.Add(new Coordinates(0, 1));
            player1.Moves.Add(new Coordinates(0, 2));
            player1.Moves.Add(new Coordinates(0, 3));
            player1.Moves.Add(new Coordinates(0, 4));

            Assume.That(player1.Moves.Count, Is.EqualTo(5));
            Assume.That(player1.Moves.FirstOrDefault(), Is.EqualTo(new Coordinates(0,0)));

            // Act
            var move = player1.MakeMove(new Coordinates(-1, -1));

            // Assert
            move.Should().BeEquivalentTo(new Move(new Coordinates(0,0), player1.Number));
            player1.Moves.Should().HaveCount(4, "because we had 5 moves and used one");

            var movesLeft = player1.Moves.ToList();
            player1.Moves.Any(x => x.Equals(new Coordinates(0, 0))).Should().BeFalse();

        }

        [Test]
        public void MakeMove_ShouldReturnConcedeMove_WhenPlayerHasNoMovesLeft()
        {
            // Assemble
            var player1 = new TestPlayer(new PlayerConstructorArguments(5, PlayerNumber.FirstPlayer));
            Assume.That(player1.Moves, Is.Empty);

            // Act
            var move = player1.MakeMove(new Coordinates(0, 0));
            var badMove = new Move(new Coordinates(-1, -1), player1.Number);
            // Assert
            move.Should().BeEquivalentTo(badMove, "because the player has no moves to choose from");
        }
    }
}
