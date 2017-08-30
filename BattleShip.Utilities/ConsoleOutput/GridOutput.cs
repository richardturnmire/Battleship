using System;
using System.Diagnostics;
using BattleShip.Utilities;

namespace Battleship.Utilities
{ 
     class GridOutput
    {
        
        [DebuggerStepThrough]
        public static void DisplayGrid(string [,] grid)
        {

            string[] _letters = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            string[] _numbers = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            string[,] gr = grid;

            
            Output.SendToConsole(ConsoleOutputType.ClearScreen);
            Output.SendToConsole("  --1---2---3---4---5---6---7---8---9---10-");

            for (int i = 0; i < 10; i++)
            {
                Output.SendToConsole($"{_letters[i]} |", ConsoleOutputType.StringOnly);
                for (int j = 0; j < 10; j++)
                {
                    if (gr[i, j] == "M")
                        Output.SendToConsole($" {gr[i, j]} ", ConsoleOutputType.StringOnly, ConsoleColor.Yellow);
                    else
                        if (gr[i, j] == "H")
                        Output.SendToConsole($" {gr[i, j]} ", ConsoleOutputType.StringOnly, ConsoleColor.Red);
                    else
                        if (gr[i, j] == "S")
                        Output.SendToConsole($" {gr[i, j]} ", ConsoleOutputType.StringOnly, ConsoleColor.Green);
                    else
                        Output.SendToConsole("   ", ConsoleOutputType.StringOnly);

                    Output.SendToConsole("|", ConsoleOutputType.StringOnly);
                }

                Output.SendToConsole(ConsoleOutputType.NewLine);
                Output.SendToConsole("  |---|---|---|---|---|---|---|---|---|---|");
            }
            
        }
    }
}
