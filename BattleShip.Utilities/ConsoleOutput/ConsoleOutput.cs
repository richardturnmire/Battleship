using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Utilities
{

    public static class Output
    {
        [DebuggerStepThrough]
        public static void SendToConsole(string str, ConsoleOutputType ot, ConsoleColor fg, ConsoleColor bg)
        {
            Console.ForegroundColor = fg;
            Console.BackgroundColor = bg;

            if (ot == ConsoleOutputType.NewLine)
                Console.WriteLine(str);
            else
                if (ot == ConsoleOutputType.ClearScreen)
                Console.Clear();
            else
                Console.Write(str);

            Console.ResetColor();
        }

        [DebuggerStepThrough]
        public static void SendToConsole(string str, ConsoleOutputType ot, ConsoleColor fg)
        {
            SendToConsole(str, ot, fg, Console.BackgroundColor);
        }

        [DebuggerStepThrough]
        public static void SendToConsole(string str, ConsoleOutputType ot)
        {
            SendToConsole(str, ot, Console.ForegroundColor, Console.BackgroundColor);
        }

        [DebuggerStepThrough]
        public static void SendToConsole(ConsoleOutputType ot)
        {
            SendToConsole(String.Empty, ot, Console.ForegroundColor, Console.BackgroundColor);
        }

        [DebuggerStepThrough]
        public static void SendToConsole(string str)
        {
            SendToConsole(str, ConsoleOutputType.NewLine, Console.ForegroundColor, Console.BackgroundColor);
        }

    }
}

