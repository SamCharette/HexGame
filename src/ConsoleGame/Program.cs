using System;
using System.Collections.Generic;
using System.Linq;
using Engine;
using Engine.Players;
using Engine.ValueTypes;

namespace ConsoleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var playerTypes = new PlayerFactory().GetPlayerTypes();
            
            var size = 5;
            var player1Args = new PlayerConstructorArguments(size, PlayerNumber.FirstPlayer);
            var player1 = new PlayerFactory().CreatePlayerOfType(GetPlayerFromUserInput(playerTypes), player1Args);
            
            var player2Args = new PlayerConstructorArguments(size, PlayerNumber.SecondPlayer);
            var player2 = new PlayerFactory().CreatePlayerOfType(GetPlayerFromUserInput(playerTypes), player1Args);

            var game = new Game(size, player1, player2);
            Console.WriteLine("Game is starting now.");

            game.StartGame();

            Console.WriteLine("Game over.  Player "  + game.Winner + " won.");
            DisplayMoves(game);
        }

        private static void DisplayMoves(Game game)
        {
            foreach (var move in game.Moves)
            {
                Console.WriteLine(move);
            }
        }

        private static string GetPlayerFromUserInput(List<PlayerFactory.PlayerType> types)
        {
            string selectedType = "";

            while (!types.Select(x => x.Name).Any(x => x.Equals(selectedType)))
            {
                Console.WriteLine("Please choose a player type for player:");
                foreach (var playerType in types)
                {
                    Console.WriteLine(playerType.Name);
                }

                selectedType = Console.ReadLine();

            }

            return selectedType;
        }
    }
}
