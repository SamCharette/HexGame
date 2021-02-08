using System;
using System.Collections.Generic;
using Engine.Players;
using Engine.ValueTypes;
using Newtonsoft.Json;

namespace Engine
{
    public class Game
    {
        public Guid Id { get; private set; } // Simply here for when recording games
        public DateTime StartedOn { get; private set; }
        public DateTime? EndedOn { get; private set; }
        [JsonIgnore]
        public Board Board { get; private set; }
        public int Size => Board.Size;
        public PlayerBase Player1 { get; private set; }
        public PlayerBase Player2 { get; private set; }
        public PlayerNumber Winner { get; private set; }
        public List<Move> Moves { get; private set; }

        private PlayerBase _currentPlayer;
        private Move _lastMove;

        public Game(int size, PlayerBase player1, PlayerBase player2)
        {
            Id = Guid.NewGuid();
            StartedOn = DateTime.Now;
            Board = new Board(size);
            Player1 = player1;
            Player2 = player2;
            Winner = PlayerNumber.Unowned;
            _currentPlayer = player1;
            _lastMove = new Move(new Coordinates(-1, -1), PlayerNumber.Unowned);

            Moves = new List<Move>();
        }

        public void StartGame()
        {

            do
            {
                _lastMove = _currentPlayer.MakeMove(_lastMove.Coordinates);
                if (IsValidMove(_lastMove))
                {
                    HandleValidMove(_lastMove);
                }
                else
                {
                    Winner = SwitchCurrentPlayer().Number;
                }

                SwitchCurrentPlayer();


            } while (!IsGameOver());

           

            EndGame();
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public bool IsGameOver()
        {
            return Winner != PlayerNumber.Unowned;
        }

        private void HandleValidMove(Move move)
        {
            Board.HexAt(move.Coordinates)?.SetOwner(move.PlayerNumber);

            Moves.Add(new Move(move.Coordinates, move.PlayerNumber));
            if (Board.WinningPathExistsForPlayer(move.PlayerNumber))
            {
                Winner = move.PlayerNumber;
            }
        }

        private bool IsValidMove(Move move)
        {
            var hexOnBoard = Board.HexAt(move.Coordinates);

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

        private PlayerBase SwitchCurrentPlayer()
        {
            _currentPlayer = _currentPlayer.Number == PlayerNumber.FirstPlayer ? Player2 : Player1;
            return _currentPlayer;
        }
    }
}
