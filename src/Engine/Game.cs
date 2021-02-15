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
        [JsonIgnore] public Board Board { get; private set; }
        public int Size => Board.Size;
        public PlayerBase Player1 { get; private set; }
        public PlayerBase Player2 { get; private set; }
        public PlayerNumber Winner { get; private set; }
        public List<Move> Moves { get; private set; }

        private PlayerBase _currentPlayer;
        private Move _lastMove;

        public event EventHandler<Move> OnMoveMade = delegate {};
        public event Action OnGameStart = delegate { };
        public event Action OnGameEnd = delegate { };

        internal Game()
        {
            Id = Guid.NewGuid();
            Winner = PlayerNumber.Unowned;
            _lastMove = new Move(new Coordinates(-1, -1), PlayerNumber.Unowned);
            Moves = new List<Move>();
        }

        internal void SetBoardSize(int size)
        {
            Board = new Board(size);
        }

        internal void SetPlayer1(PlayerBase player)
        {
            Player1 = player;
        }

        internal void SetPlayer2(PlayerBase player)
        {
            Player2 = player;
        }

        public void StartGame()
        {
            OnGameStart();
            _currentPlayer = Player1;
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

            OnMoveMade(this, move);
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
            OnGameEnd();
            EndedOn = DateTime.Now;
        }

        private PlayerBase SwitchCurrentPlayer()
        {
            _currentPlayer = _currentPlayer.Number == PlayerNumber.FirstPlayer ? Player2 : Player1;
            return _currentPlayer;
        }
    }
}
