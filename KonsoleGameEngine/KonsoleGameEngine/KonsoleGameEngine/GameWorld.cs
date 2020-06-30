using System.Collections.Generic;

namespace KonsoleGameEngine
{
    class GameWorld
    {
        #region Fields
        public int X = 50;
        public int Y = 20;

        private Cell[,] _board;
        private AStarPathfinder _pathFinder;
        private List<GameEntity> _enetities = new List<GameEntity>();
        #endregion

        #region Ctors
        /// <summary>
        /// Standard Ctor
        /// </summary>
        public GameWorld()
        {
            _board = new Cell[X, Y];
            _pathFinder = new AStarPathfinder(this);
            InitiateGameBoard();
        }

        /// <summary>
        /// Custom Size Ctor
        /// </summary>
        public GameWorld(int x, int y)
        {
            X = x; y = Y;
            _board = new Cell[X, Y];
            _pathFinder = new AStarPathfinder(this);
            InitiateGameBoard();
        }
        #endregion

        #region Public
        /// <summary>
        /// Start the GameWorld
        /// </summary>
        public void Start()
        {
            foreach(GameEntity entity in _enetities)
            {
                entity.Start();
            }
        }

        /// <summary>
        /// Update the GameWorld each frame
        /// </summary>
        public void Update()
        {
            InitiateGameBoard();
            foreach (GameEntity entity in _enetities)
            {
                foreach(Cell cell in entity.GetCells())
                {
                    _board[cell.X, cell.Y] = cell;
                }
            }
        }

        /// <summary>
        /// Register a GameEntity with the GameWorld
        /// </summary>
        /// <param name="entity"></param>
        public void RegisterEntity(GameEntity entity)
        {
            entity._gameWorld = this;
            _enetities.Add(entity);
        }

        #region CellControls
        /// <summary>
        /// Return contents of a cell
        /// Mainly used by GraphicsManager
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public string GetCellContents(int x, int y)
        {
            return _board[x, y].Contents;
        }

        /// <summary>
        /// Return a Cell via it's X & Y coords
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Cell GetCell(int x, int y)
        {
            return _board[x, y];
        }

        /// <summary>
        /// Return a Cell via a Cell reference
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public Cell GetCell(Cell cell)
        {
            return _board[cell.X, cell.Y];
        }
        #endregion  // Cell Controls
        #endregion  // Public

        #region Private 
        /// <summary>
        /// Initiate a blank board of constructed Cells
        /// </summary>
        private void InitiateGameBoard()
        {
            for (int x = 0; x < X; x++)
            {
                for (int y = 0; y < Y; y++)
                {
                    _board[x, y] = new Cell(x, y);
                }
            }
        }
        #endregion
    }
}
