using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Engine.ValueTypes;

namespace Engine
{
    public class Game
    {
        public Guid Id { get; set; } // Simply here for when recording games
        public DateTime StartedOn { get; set; }
        public DateTime EndedOn { get; set; }

        public Board Board { get; set; }

        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

        public List<Move> Moves { get; set; }

        public Game(int size, Player player1, Player player2)
        {
            Id = Guid.NewGuid();
            StartedOn = DateTime.Now;
            Board = new Board(size);
            Player1 = player1;
            Player2 = player2;

            Moves = new List<Move>();
        }
    }
}
