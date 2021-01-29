using System;
using Engine;
using Engine.Players;
using Engine.ValueTypes;

namespace ConsoleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var size = 5;
            var player1Args = new PlayerConstructorArguments(size, PlayerNumber.FirstPlayer);
            TestPlayer player1 = (TestPlayer)new PlayerFactory().CreatePlayerOfType("TestPlayer", player1Args);
            player1
                .AddMove(0, 0)
                .AddMove(0, 1)
                .AddMove(0, 2)
                .AddMove(0, 3)
                .AddMove(0, 4);

            var player2Args = new PlayerConstructorArguments(size, PlayerNumber.SecondPlayer);
            TestPlayer player2 = (TestPlayer)new PlayerFactory().CreatePlayerOfType("TestPlayer", player2Args);
            player2
                .AddMove(2, 0)
                .AddMove(3, 0)
                .AddMove(4, 0)
                .AddMove(2, 3)
                .AddMove(2, 2);

            var game = new Game(size, player1, player2);
            Console.WriteLine("Game is starting now.");

            game.StartGame();

            Console.WriteLine("Game over.  Player "  + game.Winner + " won.");
        }
    }
}
