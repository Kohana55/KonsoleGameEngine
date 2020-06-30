using System;
using System.Collections.Generic;

namespace KonsoleGameEngine
{
    class AStarPathfinder
    {
        #region Fields
        private List<Cell> openList;
        private List<Cell> closedList;
        private GameWorld _game;
        #endregion

        #region Ctors
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="gameWorld"></param>
        public AStarPathfinder(GameWorld gameWorld)
        {
            _game = gameWorld;
        }
        #endregion

        #region Public
        /// <summary>
        /// Calculate A* path between two Cells
        /// </summary>
        /// <param name="startLocation"></param>
        /// <param name="destination"></param>
        /// <returns>A* Path</returns>
        public List<Cell> CalculatePath(Cell startLocation, Cell destination)
        {
            // Reset Game World's pathfinding scores to default
            for (int i = 0; i < _game.X; i++)
            {
                for (int j = 0; j < _game.Y; j++)
                {
                    Cell cell = _game.GetCell(i, j);
                    cell.GScore = int.MaxValue;
                    cell.CameFrom = null;
                }
            }

            // Get starting Cell, add to open list and begin AStar search
            Cell startCell = _game.GetCell(startLocation);
            Cell destCell = _game.GetCell(destination);
            startCell.GScore = 0;
            startCell.HScore = CalulateMoveCost(startCell, destCell);

            openList = new List<Cell> { startCell };
            closedList = new List<Cell>();

            while(openList.Count > 0)
            {
                Cell currentCell = GetLowestFCost(openList);
                if(currentCell == destCell)
                {
                    // path found!
                    return TracePath(destCell);
                }

                openList.Remove(currentCell);
                closedList.Add(currentCell);

                List<Cell> neighbours = FindNeighbours(currentCell);
                foreach (Cell neighbour in neighbours)
                {
                    if (closedList.Contains(neighbour))
                        continue;

                    int tenGScore = currentCell.GScore + CalulateMoveCost(currentCell, neighbour);
                    if (tenGScore < neighbour.GScore)
                    {
                        neighbour.CameFrom = currentCell;
                        neighbour.GScore = tenGScore;
                        neighbour.HScore = CalulateMoveCost(neighbour, destCell);

                        if (!openList.Contains(neighbour))
                        {
                            openList.Add(neighbour);
                        }
                    }
                }
            }
            return null;
        }
        #endregion

        #region Private
        /// <summary>
        /// 
        /// </summary>
        /// <param name="destCell"></param>
        /// <returns></returns>
        private List<Cell> TracePath(Cell destCell)
        {
            List<Cell> path = new List<Cell>();
            path.Add(destCell);
            Cell currentCell = destCell;
            while(currentCell.CameFrom != null)
            {
                path.Add(currentCell.CameFrom);
                currentCell = currentCell.CameFrom;
            }

            path.Reverse();
            return path;
        }

        /// <summary>
        /// Returns the Cell with the lowest F score from a List<Cell>
        /// </summary>
        /// <param name="cells"></param>
        /// <returns>Cell with lowest F score</returns>
        private Cell GetLowestFCost(List<Cell> cells)
        {
            Cell lowestFCell = cells[0];
            foreach(Cell cell in cells)
            {
                if (cell.FScore < lowestFCell.FScore)
                    lowestFCell = cell;
            }
            return lowestFCell;
        }

        /// <summary>
        /// Calculate a movement heuristic cost between Cells
        /// </summary>
        /// <param name="start"></param>
        /// <param name="dest"></param>
        /// <returns>Move heuristic</returns>
        private int CalulateMoveCost(Cell start, Cell dest)
        {
            int xDist = Math.Abs(start.X - dest.X);
            int yDist = Math.Abs(start.Y - dest.Y);
            int remaining = Math.Abs(xDist - yDist);
            return 14 * Math.Min(xDist, yDist) + 10 * remaining;
        }

        /// <summary>
        /// Returns list of neighbours
        /// </summary>
        /// <param name="cell"></param>
        /// <returns>List of neighbours</returns>
        private List<Cell> FindNeighbours (Cell cell)
        {
            List<Cell> neighbours = new List<Cell>();

            int x = cell.X;
            int y = cell.Y;

            // Find neighours
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (x + i < 0 || x + i >= _game.X)   // Out of bounds
                        continue;
                    if (y + j < 0 || y + j >= _game.Y)   // Out of bounds
                        continue;
                    if (x + i == x && y + j == y)       // Same Cell
                        continue;
                    if (_game.GetCell(x + i, y + j).IsWalkable == false)
                        continue;

                    // Add Cell to neighbour list
                    neighbours.Add(_game.GetCell(x + i, y + j));
                }
            }

            return neighbours;
        }
        #endregion
    }
}
