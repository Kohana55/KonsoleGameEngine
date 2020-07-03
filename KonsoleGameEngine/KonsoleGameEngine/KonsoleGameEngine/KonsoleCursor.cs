using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KonsoleGameEngine
{
    class KonsoleCursor : GameEntity
    {

        /// <summary>
        /// Cursor Model
        /// </summary>
        public Cell Model = new Cell(0, 0, "x");

        public int X { get { return Model.X; } }
        public int Y { get { return Model.Y; } }

        /// <summary>
        /// Returns Cursor Cell
        /// </summary>
        /// <returns></returns>
        public override List<Cell> GetCells()
        {
            return new List<Cell> { Model };
        }

        /// <summary>
        /// Calls the Controller method in its own thread
        /// </summary>
        public override void Start()
        {
            Thread playerControllerThread = new Thread(Controller);
            playerControllerThread.Start();
        }

        /// <summary>
        /// Controls the Cursor
        /// WASD    - movement
        /// SPACE   - select
        /// </summary>
        protected virtual void Controller()
        {
            ConsoleKeyInfo keypress;
            Cell currentCell;

            while (true)
            {

                keypress = Console.ReadKey(true);

                if (keypress.KeyChar == 'a' && Model.X != 0)
                    Model.X -= 1;

                if (keypress.KeyChar == 'd' && Model.X != _gameWorld.X - 1)
                    Model.X += 1;

                if (keypress.KeyChar == 'w' && Model.Y != 0)
                    Model.Y -= 1;

                if (keypress.KeyChar == 's' && Model.Y != _gameWorld.Y - 1)
                        Model.Y += 1;

                if (keypress.KeyChar == ' ')
                    currentCell = _gameWorld.GetCell(Model);
            }
        }
    }
}
