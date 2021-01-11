using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Engine.ValueTypes;

namespace Engine
{
    public class Game
    {
        public Guid Id { get; private set; } // Simply here for when recording games
        public DateTime StartedOn { get; private set; }
        public DateTime? EndedOn { get; private set; }
        public Board Board { get; private set; }
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }
        public PlayerNumber Winner { get; private set; }
        public List<Move> Moves { get; private set; }

        public Game(int size, Player player1, Player player2)
        {
            Id = Guid.NewGuid();
            StartedOn = DateTime.Now;
            Board = new Board(size);
            Player1 = player1;
            Player2 = player2;
            Winner = PlayerNumber.Unowned;

            Moves = new List<Move>();
        }

        public void StartGame()
        {
            try
            {
                var concedeMove = new Coordinates(-1, -1);

                var currentPlayer = Player1;

                var lastMove = concedeMove;
                do
                {
                    lastMove = currentPlayer.MakeMove(lastMove);
                    if (!lastMove.Equals(concedeMove) && Board.HexAt(lastMove).Owner == PlayerNumber.Unowned)
                    {
                        Board.Hexes.FirstOrDefault(x => x.Coordinates.Equals(lastMove))?.SetOwner(Player1.Number);
                        Moves.Add(new Move(lastMove, currentPlayer.Number));
                    }
                    else
                    {
                        Winner = SwitchCurrentPlayer(currentPlayer).Number;
                    }
                    currentPlayer = SwitchCurrentPlayer(currentPlayer);

                } while (!lastMove.Equals(concedeMove) && Winner == PlayerNumber.Unowned);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            EndGame();
        }

        public TimeSpan GameLength()
        {
            var currentOrEndGameTime = EndedOn ?? DateTime.Now;
            return currentOrEndGameTime.Subtract(StartedOn);
        }

        public void EndGame()
        {
            EndedOn = DateTime.Now;
        }

        private Player SwitchCurrentPlayer(Player lastPlayer)
        {
            return lastPlayer.Number == PlayerNumber.FirstPlayer ? Player2 : Player1;
        }
    }
}
