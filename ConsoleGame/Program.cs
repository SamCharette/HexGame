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
            var size = 11;
            var player1Args = new PlayerConstructorArguments(size, PlayerNumber.FirstPlayer);
            var player1 = new PlayerFactory().CreatePlayerOfType("TestPlayer", player1Args);

            var player2Args = new PlayerConstructorArguments(size, PlayerNumber.SecondPlayer);
            var player2 = new PlayerFactory().CreatePlayerOfType("TestPlayer", player2Args);

            var game = new Game(size, player1, player2);
            Console.WriteLine("Game is starting now.");

            game.StartGame();

            Console.WriteLine("Game over.  Player "  + game.Winner + " won.");
        }
    }
}
