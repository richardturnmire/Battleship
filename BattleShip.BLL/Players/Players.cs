using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.GameLogic;


namespace BattleShip.BLL.Players
{
    
    public class Player
    {

        public string Name { get; set; }

        public Board PlayersBoard { get; set; }

        public Grid PlayersGrid { get; set; }

        public Player(string name)
        {
            Name = name;
            PlayersBoard = new Board();
            PlayersGrid = new Grid();
        }
    }
}
