
namespace BattleShip.BLL.Players
{
    public class Grid
    {
        protected string[,] _grid;

        public GridType Type { get; set; }

        public Grid()
        {
            _grid = new string[10, 10];
            Type = GridType.Ship;
   
        }
            
        public string[,] GetGrid()
        {
            return _grid;
        }
        
        public void PlaceInGrid(Requests.Coordinate co, Responses.FireShotResponse resp)
        {
            int x = co.XCoordinate - 1;
            int y = co.YCoordinate - 1;

            if (resp.ShotStatus == Responses.ShotStatus.Miss)
                _grid[x, y] = "M";
            else
                if (resp.ShotStatus == Responses.ShotStatus.Hit || 
                    resp.ShotStatus == Responses.ShotStatus.HitAndSunk ||
                    resp.ShotStatus == Responses.ShotStatus.Victory)
                _grid[x, y] = "H";
        }

        public void PutShipInGrid(Requests.Coordinate co)
        {
            int x = co.XCoordinate - 1;
            int y = co.YCoordinate - 1;

            _grid[x, y] = "S";
        }

        public void ClearGrid()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    _grid[i, j] = string.Empty;
                }
            }
        }

    }
}

