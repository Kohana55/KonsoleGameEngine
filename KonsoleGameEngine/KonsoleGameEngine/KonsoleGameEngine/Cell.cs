

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
        /// Standard Ctor
        /// Allows others to derive from this class
        /// </summary>
        public Cell(){}

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
