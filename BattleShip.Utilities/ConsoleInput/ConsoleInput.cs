using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Utilities
{
    public class Input
    {
        [DebuggerStepThrough]
        public static string GetStringFromUser(string prompt)
        {
            Output.SendToConsole(prompt, ConsoleOutputType.StringOnly);
            return Console.ReadLine();
        }

        [DebuggerStepThrough]
        public static int GetIntFromUser(string prompt)
        {          
            

            while (true)
            {
                Output.SendToConsole(prompt, ConsoleOutputType.StringOnly);
                string input = Console.ReadLine();

                if (int.TryParse(input, out int output))
                {
                    return output;
                }
                else
                {
                    Output.SendToConsole("That was not a valid number! Press any key to try again...");
                }
            }
        }

    }
}
