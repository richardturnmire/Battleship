using System;
using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Players;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;
using BattleShip.Utilities;

namespace BattleShip.UI
{
    public static class GameFlow
    {
        private static Player[] Players = new Player[2];
        private static int WhosTurn = 0;
        private static string _letters = "ABCDEFGHIJ";
        private static bool? GameOver = false;
        private static Random rndm = new Random();

        public static void Start()
        {
            while (true)
            {
                GetPlayers();
                PopulateBoards();

                while (true)
                {
                    Output.SendToConsole(ConsoleOutputType.ClearScreen);
                    GameOver = PlayGame(Players[WhosTurn]);
                    if (GameOver == true)
                        break;
                    else
                        if (GameOver == false)
                    {
                        WhosTurn = ++WhosTurn;
                        WhosTurn = WhosTurn % 2;
                    }
                    else
                    {
                        Output.SendToConsole("You entered a location that was either a duplicate or invalid.");
                        Input.GetStringFromUser("Try again! Press enter to continue...");
                    }
                }

                string _response = Input.GetStringFromUser("Play another game? ");
                if (!(_response.ToUpper() == "Y"))
                    break;
            }
        }

        private static void GetPlayers()
        {
            String name1 = String.Empty;
            String name2 = String.Empty;


            name1 = Input.GetStringFromUser("Enter first Player's name:  ");
            name2 = Input.GetStringFromUser("Enter second Player's name: ");

            WhosTurn = rndm.Next(0, 2);

            if (WhosTurn == 0)
            {
                Players[0] = new Player(name1);
                Players[1] = new Player(name2);
            }
            else
            {
                Players[0] = new Player(name2);
                Players[1] = new Player(name1);
            }

            Input.GetStringFromUser($"\n{Players[0].Name} goes first. Press enter to begin setting up the boards...\n");

        }

        private static void PopulateBoards()
        {
            for (int i = 0; i < 2; i++)
            {
                Console.Title = $"{Players[i].Name}'s Ship Placement Board";
                Output.SendToConsole(ConsoleOutputType.ClearScreen);
                Output.SendToConsole($"{Players[i].Name}, Please select your 5 ships.");
                Board shipb = Players[i].PlayersBoard;
                for (int j = 0; j < shipb.Ships.Length; j++)
                {
                    ShipType st = AskUserForShipType(j);

                    while (true)
                    {
                        DisplayShotHistory(Players[i]);

                        Coordinate co = AskUserForCoordinate($"\nEnter coordinates for your {Enum.GetName(typeof(ShipType), (int)st)}: ");
                        ShipDirection di = AskUserForDirection();

                        PlaceShipRequest pr = new PlaceShipRequest
                        {
                            Coordinate = co,
                            Direction = di,
                            ShipType = st

                        };

                        ShipPlacement sp = shipb.PlaceShip(pr);

                        if (sp == ShipPlacement.Ok)
                            break;
                        else
                            if (sp == ShipPlacement.NotEnoughSpace)
                        {
                            Input.GetStringFromUser("\nNot Enough Space! Press enter to try again...\n");
                        }
                        else
                            if (sp == ShipPlacement.Overlap)
                        {
                            Output.SendToConsole("Two ships cannot occupy the same location\n");
                            Input.GetStringFromUser("Press enter to try again...");
                        }
                    }

                    foreach (Coordinate co in shipb.Ships[j].BoardPositions)
                    {
                        Players[i].PlayersGrid.PutShipInGrid(co);
                    }

                }

                Players[i].PlayersGrid.Type = GridType.Shot;
                Players[i].PlayersGrid.ClearGrid();

                Output.SendToConsole(ConsoleOutputType.ClearScreen);

            }
        }

        private static ShipType AskUserForShipType(int j)
        {
            int ship;
            int si = 0;
            Output.SendToConsole(ConsoleOutputType.ClearScreen);
            Output.SendToConsole($"Select ship {j + 1} from the following list: ");

            foreach (string s in Enum.GetNames(typeof(ShipType)))
            {
                si++;
                Output.SendToConsole($"{si}) {s} ");
            }

            Output.SendToConsole(ConsoleOutputType.NewLine);

            while (true)
            {
                ship = Input.GetIntFromUser($"Enter 1 - {si}: ");
                if (ship >= 1 && ship <= si)
                    break;
                else
                    Input.GetStringFromUser("Invalid number. Press enter to try again...");
            }

            Output.SendToConsole(ConsoleOutputType.ClearScreen);

            return (ShipType)ship - 1;

        }

        private static ShipDirection AskUserForDirection()
        {
            int di;

            while (true)
            {
                int si = 0;

                Output.SendToConsole($"Select a direction (1 - 4) from the following list: ");

                foreach (string s in Enum.GetNames(typeof(ShipDirection)))
                {
                    si++;
                    Output.SendToConsole($"{si}) {s} ", ConsoleOutputType.StringOnly);
                }

                di = Input.GetIntFromUser(": ");
                if (di >= 1 && di <= si)
                    break;
                else
                    Input.GetStringFromUser("Invalid direction. Try again ...");
            }

            return (ShipDirection)di - 1;

        }

        private static Coordinate AskUserForCoordinate(string prompt)
        {
            var coord = string.Empty;
            while (true)
            {
                coord = Input.GetStringFromUser(prompt).ToUpper();

                if (ValidateCoordinate(coord))
                {
                    break;
                }
                else
                {
                    Input.GetStringFromUser("Invalid coordinate! Press enter to try again...");
                }
            }

            int x = _letters.IndexOf(coord.Substring(0, 1)) + 1;
            int y = int.Parse(coord.Substring(1));

            return new Coordinate(x, y);
        }

        private static bool ValidateCoordinate(string coord)
        {
            bool results = true;

            if (coord.Length == 2 || coord.Length == 3)
            {
                string letter = coord.Substring(0, 1).ToUpper();
                if (!_letters.Contains(letter))
                {
                    results = false;
                }
                else
                {
                    if (int.TryParse(coord.Substring(1), out int y))
                    {
                        if (y < 1 || y > 10)
                            results = false;
                    }
                    else
                    {
                        results = false;
                    }
                }
            }
            else
            {
                results = false;
            }

            return results;
        }

        private static bool? PlayGame(Player pl)
        {
            bool? gover = false;

            Console.Title = $"{pl.Name}'s Turn";

            Coordinate co = AskUserForCoordinate($"{pl.Name}, Please enter the coordinate that you want to target: ");

            Player op;

            if (WhosTurn == 0)
                op = Players[1];
            else
                op = Players[0];

            FireShotResponse resp = op.PlayersBoard.FireShot(co);

            pl.PlayersGrid.PlaceInGrid(co, resp);

            DisplayShotHistory(pl);

            if (resp.ShotStatus == ShotStatus.Hit)
            {
                Input.GetStringFromUser($"\nHit! Press enter to continue...");
            }
            else
            if (resp.ShotStatus == ShotStatus.HitAndSunk)
            {
                Input.GetStringFromUser($"\nHit! You sunk {op.Name}'s {resp.ShipImpacted}! Press enter to continue...");
            }
            else
            if (resp.ShotStatus == ShotStatus.Miss)
            {
                Input.GetStringFromUser($"\nMissed. Press enter to continue...");
            }
            else
            if (resp.ShotStatus == ShotStatus.Victory)
            {
                Output.SendToConsole($"\nHit! You sunk {op.Name}'s {resp.ShipImpacted}!");
                Input.GetStringFromUser($"\nCongratulations {pl.Name}! You have won. Press enter to continue.");
                gover = true;
            }
            else
            if (resp.ShotStatus == ShotStatus.Duplicate)
            {
                Input.GetStringFromUser("\nYou've already tried that location. Press enter to try again...");
                gover = null;
            }
            else
            if (resp.ShotStatus == ShotStatus.Invalid)
            {
                Input.GetStringFromUser($"\nError! Invalid Shot Status. Press enter to try again...");
                gover = null;
            }
            else
            {
                Input.GetStringFromUser($"\nError! Unknown Shot Status reported. Press enter to try again...");
                gover = null;
            }

            Output.SendToConsole(ConsoleOutputType.ClearScreen);
            return gover;
        }
        private static void DisplayShotHistory(Player pl)
        {
            DisplayShotHistory(pl, false);
        }

        private static void DisplayShotHistory(Player pl, bool pause)
        {
            if (pl.PlayersGrid.Type == GridType.Shot)
                Console.Title = $"{pl.Name}'s Shot History";
            else
                Console.Title = $"{pl.Name}'s Ship Placement Board";

            GridOutput.DisplayGrid(pl.PlayersGrid.GetGrid());
            if (pause)
                Input.GetStringFromUser("\nPress enter to continue...\n");

        }
    }
}
