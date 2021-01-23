using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using Engine.Players;
using Engine.ValueTypes;

namespace Engine
{
    public class Game
    {
        public Guid Id { get; private set; } // Simply here for when recording games
        public DateTime StartedOn { get; private set; }
        public DateTime? EndedOn { get; private set; }
        public Board Board { get; private set; }
        public PlayerBase Player1 { get; private set; }
        public PlayerBase Player2 { get; private set; }
        public PlayerNumber Winner { get; private set; }
        public List<Move> Moves { get; private set; }

        private readonly Coordinates _concessionMove = new Coordinates(-1, -1);

        public Game(int size, PlayerBase player1, PlayerBase player2)
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
                var currentPlayer = Player1;

                var lastMove = _concessionMove;
                do
                {
                    lastMove = currentPlayer.MakeMove(lastMove);
                    if (IsValidMove(lastMove))
                    {
                        Board.Hexes.FirstOrDefault(x => x.Coordinates.Equals(lastMove))?.SetOwner(Player1.Number);
                        Moves.Add(new Move(lastMove, currentPlayer.Number));
                    }
                    else
                    {
                        Winner = SwitchCurrentPlayer(currentPlayer).Number;
                    }
                    currentPlayer = SwitchCurrentPlayer(currentPlayer);

                } while (!lastMove.Equals(_concessionMove) && Winner == PlayerNumber.Unowned);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            EndGame();
        }

        private bool IsValidMove(Coordinates coords)
        {
            var hexOnBoard = Board.HexAt(coords);

            return hexOnBoard != null && hexOnBoard.Owner == PlayerNumber.Unowned;
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

        private PlayerBase SwitchCurrentPlayer(PlayerBase lastPlayer)
        {
            return lastPlayer.Number == PlayerNumber.FirstPlayer ? Player2 : Player1;
        }
    }
}
