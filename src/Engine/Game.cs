using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
        public BasePlayer Player1 { get; private set; }
        public BasePlayer Player2 { get; private set; }
        public PlayerNumber Winner { get; private set; }
        public List<Move> Moves { get; private set; }

        private readonly Coordinates _concessionMove = new Coordinates(-1, -1);

        private BasePlayer _currentPlayer;
        private Coordinates _lastMove;

        public Game(int size, BasePlayer player1, BasePlayer player2)
        {
            Id = Guid.NewGuid();
            StartedOn = DateTime.Now;
            Board = new Board(size);
            Player1 = player1;
            Player2 = player2;
            Winner = PlayerNumber.Unowned;
            _currentPlayer = player1;
            _lastMove = _concessionMove;

            Moves = new List<Move>();
        }

        public void StartGame()
        {

            do
            {
                _lastMove = _currentPlayer.MakeMove(_lastMove);
                if (IsValidMove(_lastMove))
                {
                    HandleValidMove(_lastMove, _currentPlayer);
                }
                else
                {
                    Winner = SwitchCurrentPlayer().Number;
                }

                SwitchCurrentPlayer();


            } while (!IsGameOver());

           

            EndGame();
        }

        public bool IsGameOver()
        {
            return Winner != PlayerNumber.Unowned;
        }

        private void HandleValidMove(Coordinates coordinates, BasePlayer player)
        {
            Board.HexAt(coordinates)?.SetOwner(player.Number);

            Moves.Add(new Move(coordinates, player.Number));
            if (Board.WinningPathExistsForPlayer(player.Number))
            {
                Winner = player.Number;
            }
        }

        private bool IsValidMove(Coordinates coords)
        {
            if (coords.Equals(_concessionMove)) return false;
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

        private BasePlayer SwitchCurrentPlayer()
        {
            _currentPlayer = _currentPlayer.Number == PlayerNumber.FirstPlayer ? Player2 : Player1;
            return _currentPlayer;
        }
    }
}
