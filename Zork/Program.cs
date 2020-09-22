using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace Zork
{
    
    class Program
    {
        private static string currentRoom
        {
            get
            {
                return Rooms[Location.Row, Location.Column];
            }
        }
        
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            Commands command = Commands.UNKNOWN;
            while (command != Commands.QUIT)
            {
                Console.WriteLine(currentRoom);
                Console.Write("> ");
                command = ToCommand(Console.ReadLine().Trim());

                string outputString;
                switch (command)
                {
                    case Commands.QUIT:
                        outputString = "Thank you for playing!";
                        break;

                    case Commands.LOOK:
                        outputString = "This is an open field west of a white house with a boarded front door. \nA rubber mat saying 'Welcome to Zork!' lies by the door.";
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        if (Move(command) == false)
                        {

                            Console.WriteLine("The way is shut!");
                        }
                        outputString = $"You moved {command}";
                        break;

                    default:
                       outputString = "Unknown command.";
                        break;
                }

                Console.WriteLine(outputString);  
            }
        }

        private static bool Move(Commands command)
        {
            bool isValidMove = true;
            switch (command)
            {
                case Commands.NORTH:
                case Commands.SOUTH:
                    isValidMove = false;
                    break;

                case Commands.EAST when Location.Column < Rooms.Length - 1:
                    Location.Column++;
                    break;

                case Commands.WEST when Location.Column > 0:
                    Location.Column--;
                    break;

                default:
                    isValidMove = false;
                    break;
                    
                
            }
            return isValidMove;
        }

        private static Commands ToCommand(string commandString) => Enum.TryParse(commandString, true, out Commands result) ? result : Commands.UNKNOWN;

        private static readonly string[,] Rooms =
        {
            {"Forest", "West of House", "Behind House", "Clearing", "Canyon View" }
        };

        private static (int Row, int Column) Location = (0, 1);
    }
}
