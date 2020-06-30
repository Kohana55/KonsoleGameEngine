

namespace KonsoleGameEngine
{
    public class Cell
    {
        #region Properties 
        /// <summary>
        /// Coords of the cell
        /// </summary>
        public int X { get; set; }
        public int Y { get; set; }

        #region Pathfinding
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
        public Cell CameFrom { get; set; }
        #endregion

        /// <summary>
        /// To be rendered
        /// </summary>
        public string Contents { get; set; }

        /// <summary>
        /// For collision detection (& pathfinding)
        /// </summary>
        public bool IsWalkable;
        #endregion

        #region Ctors
        /// <summary>
        /// Ctor for Cell
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="contents">A " " by default</param>
        public Cell(int x, int y, string contents = " ")
        {
            X = x;
            Y = y;
            Contents = contents;
            IsWalkable = true;
        }
        #endregion
    }
}
