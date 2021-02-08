using System;
using System.Collections.Generic;
using System.Text;
using Engine.ValueTypes;

namespace Engine.Players
{

    public interface IPlayer
    {
        public PlayerNumber Number { get; }
        public string Type { get; }
        public int Size { get; }

        public Move MakeMove(Coordinates opponentMove);
        public void SetPlayer(PlayerNumber number);
        public void SetBoardSize(int size);
        public void Configure(PlayerConstructorArguments args);
        public void Configure(Configuration config);
    }

    public abstract class PlayerBase : IPlayer
    {
        public PlayerNumber Number { get; protected set; }
        public string Type { get; protected set; }
        public int Size { get; protected set; }
        public abstract Move MakeMove(Coordinates opponentMove);

        public void SetPlayer(PlayerNumber number)
        {
            Number = number;
        }

        public void SetBoardSize(int size)
        {
            Size = size;
        }

        public abstract void Configure(PlayerConstructorArguments args);

        public abstract void Configure(Configuration config);
    }

    public class PlayerConstructorArguments
    {
        public PlayerNumber PlayerNumber { get; private set; }
        public int BoardSize { get; private set; }
        public Configuration Configuration { get; private set; }

        public PlayerConstructorArguments(int size, PlayerNumber number, string config = "")
        {
            PlayerNumber = number;
            BoardSize = size;
            Configuration = Configuration.GetConfiguration(config);
        }
    }
}
