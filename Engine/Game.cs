using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Engine.ValueTypes;

namespace Engine
{
    public class Game
    {
        public Guid Id { get; private set; } // Simply here for when recording games
        public DateTime StartedOn { get; private set; }
        public DateTime EndedOn { get; private set; }

        public Board Board { get; private set; }

        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }

        public List<Move> Moves { get; private set; }

        public Game(int size, Player player1, Player player2)
        {
            Id = Guid.NewGuid();
            StartedOn = DateTime.Now;
            Board = new Board(size);
            Player1 = player1;
            Player2 = player2;

            Moves = new List<Move>();
        }

        public void EndGame()
        {
            EndedOn = DateTime.Now;
        }
    }
}
