using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Players;

namespace Engine
{

    public interface IAddPlayerOne
    {
        public IAddPlayerTwo WithPlayerOne(PlayerBase player);
    }

    public interface IAddPlayerTwo
    {
        public IBuild WithPlayerTwo(PlayerBase player);
    }

    public interface IConfigureGame
    {
        public IAddPlayerOne WithBoardSize(int size);
    }

    public interface IBuild
    {
        public Game Build();
    }

    public class GameBuilder : IConfigureGame, IBuild, IAddPlayerOne, IAddPlayerTwo
    {
        private static Game _game = new Game();

        public Game Build() => _game;

        
        public static GameBuilder New()
        {
            return new GameBuilder();
        }
        

        public IAddPlayerTwo WithPlayerOne(PlayerBase player)
        {
            if (player == null)
            {
                throw new ArgumentException("Player must not be null");
            }
            _game.SetPlayer1(player);
            return this;
        }
        public IBuild WithPlayerTwo(PlayerBase player)
        {
            if (player == null)
            {
                throw new ArgumentException("Player must not be null");
            }
            _game.SetPlayer2(player);
            return this;
        }

        public IAddPlayerOne WithBoardSize(int size)
        {
            _game.SetBoardSize(size);
            return this;
        }
    }
}
