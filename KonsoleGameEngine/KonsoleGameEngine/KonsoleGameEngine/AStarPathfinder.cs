using System;
using System.Collections.Generic;

namespace KonsoleGameEngine
{
    class AStarPathfinder
    {
        #region Fields
        private GameWorld _game;
        private List<PathNode> openListP;
        private List<PathNode> closedListP;
        private PathNode[,] _board;
        #endregion

        #region Ctors
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="gameWorld"></param>
        public AStarPathfinder(GameWorld gameWorld)
        {
            _game = gameWorld;
            _board = new PathNode[gameWorld.X, gameWorld.Y];
        }
        #endregion

        #region Public
        /// <summary>
        /// Calculate A* path between two Cells
        /// </summary>
        /// <param name="startLocation"></param>
        /// <param name="destination"></param>
        /// <returns>A* Path</returns>
        public List<PathNode> CalculatePath(Cell startLocation, Cell destination, bool fourNeighbours = false)
        {
            // Reset Game World's pathfinding scores to default
            for (int i = 0; i < _game.X; i++)
            {
                for (int j = 0; j < _game.Y; j++)
                {
                    _board[i, j] = new PathNode(_game.GetCell(i, j));
                    _board[i, j].GScore = int.MaxValue;
                    _board[i, j].CameFrom = null;
                }
            }

            // Get starting Cell, add to open list and begin AStar search
            PathNode startNode = _board[startLocation.X, startLocation.Y];
            PathNode destNode = _board[destination.X, destination.Y];
            startNode.GScore = 0;
            startNode.HScore = CalulateMoveCost(startNode, destNode);

            openListP = new List<PathNode> { startNode };
            closedListP = new List<PathNode>();

            while (openListP.Count > 0)
            {
                PathNode currentNode = GetLowestFCost(openListP);
                if (currentNode == destNode)
                {
                    // path found!
                    return TracePath(destNode);
                }

                openListP.Remove(currentNode);
                closedListP.Add(currentNode);

                List<PathNode> neighbours = FindNeighbours(currentNode, fourNeighbours);
                foreach (PathNode neighbour in neighbours)
                {
                    if (closedListP.Contains(neighbour))
                        continue;

                    int tenGScore = currentNode.GScore + CalulateMoveCost(currentNode, neighbour);
                    if (tenGScore < neighbour.GScore)
                    {
                        neighbour.CameFrom = currentNode;
                        neighbour.GScore = tenGScore;
                        neighbour.HScore = CalulateMoveCost(neighbour, destNode);

                        if (!openListP.Contains(neighbour))
                        {
                            openListP.Add(neighbour);
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
        private List<PathNode> TracePath(PathNode destNode)
        {
            List<PathNode> path = new List<PathNode>();
            path.Add(destNode);
            PathNode currentCell = destNode;
            while (currentCell.CameFrom != null)
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
        private PathNode GetLowestFCost(List<PathNode> nodes)
        {
            PathNode lowestFCell = nodes[0];
            foreach (PathNode node in nodes)
            {
                if (node.FScore < lowestFCell.FScore)
                    lowestFCell = node;
            }
            return lowestFCell;
        }

        /// <summary>
        /// Calculate a movement heuristic cost between Cells
        /// </summary>
        /// <param name="start"></param>
        /// <param name="dest"></param>
        /// <returns>Move heuristic</returns>
        private int CalulateMoveCost(PathNode start, PathNode dest)
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
        private List<PathNode> FindNeighbours(PathNode node, bool fourNeighbours = false)
        {
            List<PathNode> neighbours = new List<PathNode>();

            int x = node.X;
            int y = node.Y;

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
                    if (_board[x + i, y + j].IsWalkable == false)
                        continue;
                    if (fourNeighbours)
                        if (!(_board[x + i, y + j].X == x || _board[x + i, y + j].Y == y))
                            continue;

                    // Add Cell to neighbour list
                    neighbours.Add(_board[x + i, y + j]);
                }
            }

            return neighbours;
        }
        #endregion
    }
}
