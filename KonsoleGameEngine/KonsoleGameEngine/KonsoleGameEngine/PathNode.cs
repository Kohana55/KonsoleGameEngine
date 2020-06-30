using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonsoleGameEngine
{
    class PathNode
    {
        /// <summary>
        /// Coords of the cell
        /// </summary>
        public int X { get; set; }
        public int Y { get; set; }

        /// <summary>
        /// Movement cost from starting Cell
        /// </summary>
        public int GScore { get; set; }
        /// <summary>
        /// Movement cost from this Cell to destination Cell
        /// </summary>
        public int HScore { get; set; }
        /// <summary>
        /// gScore + hScore
        /// </summary>
        public int FScore { get { return GScore + HScore; } }
        /// <summary>
        /// Cell travelled from to this Cell
        /// </summary>
        public PathNode CameFrom { get; set; }

        /// <summary>
        /// For collision detection (& pathfinding)
        /// </summary>
        public bool IsWalkable;

        public PathNode(Cell cell)
        {
            X = cell.X;
            Y = cell.Y;
            IsWalkable = cell.IsWalkable;
        }

    }
}
