using System;
using System.Collections.Generic;

namespace KonsoleGameEngine
{
    class GameWorld
    {
        #region Fields
        public int X = 50;
        public int Y = 20;

        private Cell[,] _board;
        private List<GameEntity> _entities = new List<GameEntity>();
        private KonsoleUI _ui;
        private KonsoleCursor _cursor;

        /// <summary>
        /// Lock for _board access
        /// </summary>
        private readonly object _boardLock = new object();
        private readonly object _entitiesLock = new object();
        #endregion

        #region Ctors
        /// <summary>
        /// Standard Ctor
        /// </summary>
        public GameWorld()
        {
            _board = new Cell[X, Y];
            InitiateGameBoard();
        }

        /// <summary>
        /// Custom Size Ctor
        /// </summary>
        public GameWorld(int x, int y)
        {
            X = x; y = Y;
            _board = new Cell[X, Y];
            InitiateGameBoard();
        }
        #endregion

        #region Public
        /// <summary>
        /// Start the GameWorld
        /// </summary>
        public void Start()
        {
            lock (_entitiesLock)
            {
                foreach (GameEntity entity in _entities)
                {
                    entity.Start();
                }
            }

            if (_cursor != null)
                _cursor.Start();
        }

        /// <summary>
        /// Update the GameWorld each frame
        /// </summary>
        public void Update()
        {
            lock (_boardLock)
            {
                lock (_entitiesLock)
                {
                    InitiateGameBoard();
                    foreach (GameEntity entity in _entities)
                    {
                        foreach (Cell cell in entity.GetCells())
                        {
                            _board[cell.X, cell.Y] = cell;
                        }
                    }
                }

                if (_cursor != null)
                    _board[_cursor.X, _cursor.Y] = _cursor.Model;

                if (_ui != null)
                    _ui.Update();
            }
        }

        /// <summary>
        /// Register a GameEntity with the GameWorld
        /// </summary>
        /// <param name="entity"></param>
        public void RegisterEntity(GameEntity entity)
        {
            lock (_entitiesLock)
            {
                entity._gameWorld = this;
                _entities.Add(entity);
            }
        }

        public void RegisterAndStartEntity(GameEntity entity)
        {
            lock (_entitiesLock)
            {
                entity._gameWorld = this;
                _entities.Add(entity);
                entity.Start();
            }
        }

        /// <summary>
        /// Get GameEntity assigned to Cell
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public GameEntity GetEntity(Cell cell)
        {
            foreach(GameEntity entity in _entities)
            {
                foreach(Cell _cell in entity.GetCells())
                {
                    if (_cell.X == cell.X && _cell.Y == cell.Y)
                        return entity;
                }
            }

            return null;
        }

        /// <summary>
        /// Register a Konsole Cursor with the GameWorld
        /// </summary>
        /// <param name="cursor"></param>
        public void RegisterCursor(KonsoleCursor cursor)
        {           
            _cursor = cursor;
            _cursor._gameWorld = this;
        }

        /// <summary>
        /// Register a KonsoleUI with the GameWorld
        /// </summary>
        /// <param name="ui"></param>
        public void RegisterUI(KonsoleUI ui)
        {
            _ui = ui;
            _ui.RegisterGameWorld(this);
        }

        /// <summary>
        /// Returns the UI buffer
        /// </summary>
        /// <returns></returns>
        public string GetUIBuffer()
        {
            if (_ui != null)
                return _ui.GetUI();
            return null;
        }

        /// <summary>
        /// Returns the KonsoleUI element to the GameWorld
        /// </summary>
        /// <returns></returns>
        public KonsoleUI GetUI()
        {
            if (_ui != null)
                return _ui;
            return null;
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
            string contents;
            lock (_boardLock)
            {
                contents = _board[x, y].Contents;
            }
            return contents;
        }

        /// <summary>
        /// Return a Cell via it's X & Y coords
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Cell GetCell(int x, int y)
        {
            Cell cell;
            lock (_boardLock)
            {
                cell = _board[x, y];
            }
            return cell;
        }

        /// <summary>
        /// Return a Cell via a Cell reference
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public Cell GetCell(Cell cell)
        {
            Cell _cell;
            lock (_boardLock)
            {
                _cell = _board[cell.X, cell.Y];
            }
            return _cell;
        }
        #endregion  // Cell Controls
        #endregion  // Public

        #region Private 
        /// <summary>
        /// Initiate a game board of constructed Cells
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
