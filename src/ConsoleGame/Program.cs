using System;
using System.Collections.Generic;
using System.Linq;
using Engine;
using Engine.Players;
using Engine.ValueTypes;

namespace ConsoleGame
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Not enough arguments");
                return;
            }

            if (!int.TryParse(args[0], out var boardSize))
            {
                Console.WriteLine("First argument must be a number");
            }
            var playerTypes = new PlayerFactory().PlayerTypes;

            if (!playerTypes.Select(x => x.Name).Contains(args[1]))
            {
                Console.WriteLine("Player 1 type '" + args[1] + "' doesn't exist");
                return;
            }
            var player1Args = new PlayerConstructorArguments(boardSize, PlayerNumber.FirstPlayer);
            var player1 = new PlayerFactory().CreatePlayerOfType(args[1], player1Args);

            if (!playerTypes.Select(x => x.Name).Contains(args[2]))
            {
                Console.WriteLine("Player 2 type '" + args[2] + "' doesn't exist");
                return;
            }
            var player2Args = new PlayerConstructorArguments(boardSize, PlayerNumber.SecondPlayer);
            var player2 = new PlayerFactory().CreatePlayerOfType(args[2], player2Args);

            StartGame(boardSize, player1, player2);
        }

        public static void StartGame(int size, IPlayer player1, IPlayer player2)
        {
            
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
