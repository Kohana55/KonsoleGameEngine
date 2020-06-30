using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KonsoleGameEngine
{
    class Cursor : GameEntity
    {

        /// <summary>
        /// Cursor Model
        /// </summary>
        public override Cell[] Model { get; } = { new Cell(0, 0, "x") { IsWalkable = false } };

        /// <summary>
        /// Returns Cursor Cell
        /// </summary>
        /// <returns></returns>
        public override List<Cell> GetCells()
        {
            return new List<Cell> { Model[0] };
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
        private void Controller()
        {
            ConsoleKeyInfo keypress;
            Cell currentCell;

            while (true)
            {

                keypress = Console.ReadKey(true);

                if (keypress.KeyChar == 'a' && Model[0].X != 0)
                    Model[0].X -= 1;

                if (keypress.KeyChar == 'd' && Model[0].X != _gameWorld.X - 1)
                    Model[0].X += 1;

                if (keypress.KeyChar == 'w' && Model[0].Y != 0)
                    Model[0].Y -= 1;

                if (keypress.KeyChar == 's' && Model[0].Y != _gameWorld.Y - 1)
                        Model[0].Y += 1;

                if (keypress.KeyChar == ' ')
                    currentCell = _gameWorld.GetCell(Model[0]);
            }
        }
    }
}
