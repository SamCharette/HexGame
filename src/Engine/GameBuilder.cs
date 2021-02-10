using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Players;

namespace Engine
{
    public interface INewGame
    {
        public IConfigureGame New();
    }

    public interface IConfigureGame
    {
        public IConfigureGame WithPlayerOne(PlayerBase player);
        public IConfigureGame WithPlayerTwo(PlayerBase player);
        public IConfigureGame WithBoardSize(int size);
    }

    public interface IBuild
    {
        public Game Build();
    }

    public class GameBuilder : INewGame, IConfigureGame, IBuild
    {
        private Game _game;

        public Game Build() => _game;

        public IConfigureGame New()
        {
            _game = new Game();
            return this;
        }

        public IConfigureGame WithPlayerOne(PlayerBase player)
        {
            _game.SetPlayer1(player);
            return this;
        }
        public IConfigureGame WithPlayerTwo(PlayerBase player)
        {
            _game.SetPlayer2(player);
            return this;
        }


    }
}
