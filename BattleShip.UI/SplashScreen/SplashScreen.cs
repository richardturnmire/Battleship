using System;
using System.Threading;
using BattleShip.Utilities;

namespace BattleShip.UI
{
    class SplashScreen
    {
        protected static int _consoleWidth = Console.WindowWidth - 2;
        protected static int _padLength = 0;

        private static string[] _lines = new string[14]
            {
            "                                          ",
            "   Welcome to Richard's Battleship game   ",
            "                                          ",
            "                                          ",
            "                                          ",
            "         ===   /                          ",
            "       =======/                           ",
            "_______=======____________________________",
            " ___     U.S.S. Richard            ____   ",
            "   __________________________________     ",
            "                                          ",
            "                                          ",
            "                                          ",
            "                                          "
            };

        private static string[] _reverse = new string[5]
            {
            @"                            \   ===       ",
            @"                             \=======     ",
            @"______________________________=======_____",
            @" ____            U.S.S. Jake        ___   ",
            @"   __________________________________     ",
            }
            ;

        private static string[] _display;

        
        public static void DisplaySplashScreen()
        {
            int x = _consoleWidth - _lines[1].Length;
            int y = (int)x / 2 + 1;
            _display = new string[16];
            _display[0] = " ".PadRight(_consoleWidth);
            _display[1] = _display[0];
            _display[2] = (_lines[1].PadLeft(y)).PadRight(_consoleWidth);
            _display[3] = _display[0];
            _display[4] = _display[0];
            _display[5] = _display[0];

            _display[11] = _display[0];
            _display[12] = _display[0];
            _display[13] = _display[0];
            _display[14] = _display[0];
            _display[15] = _display[0];

                                              
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);

            for (int row = 0; row < 6; row++)
            {
                Console.SetCursorPosition(0, row);
                Console.Write(_display[row]);
            }

            Console.SetCursorPosition(0, 11);
            Console.BackgroundColor = ConsoleColor.Blue;

            for (int row = 11; row < 16; row++)
            {
                Console.SetCursorPosition(0, row);
                Console.Write(_display[row]);
            }

            Console.ResetColor();

            for (int i = 0; i < y; i += 3)
            {
                Console.SetCursorPosition(0,6);
                _display[6] = (" ".PadLeft(i) + _lines[5].PadLeft(i)).PadRight(_consoleWidth);
                _display[7] = (" ".PadLeft(i) + _lines[6].PadLeft(i)).PadRight(_consoleWidth);
                _display[8] = (" ".PadLeft(i) + _lines[7].PadLeft(i)).PadRight(_consoleWidth);
                _display[9] = (" ".PadLeft(i) + _lines[8].PadLeft(i)).PadRight(_consoleWidth);
                _display[10] = (" ".PadLeft(i) + _lines[9].PadLeft(i)).PadRight(_consoleWidth);
                              
                for (int j = 6; j < 11; j++)
                {
                    Output.SendToConsole(_display[j]);
                }

                Thread.Sleep(0100);
            }
            
            Console.CursorVisible = true;
            Console.SetCursorPosition(0, 20);
            Input.GetStringFromUser("\nPress Enter to begin...");
            Output.SendToConsole(ConsoleOutputType.ClearScreen);
        }
    }
}
