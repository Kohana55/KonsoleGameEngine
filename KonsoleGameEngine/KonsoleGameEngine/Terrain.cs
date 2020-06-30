using KonsoleGameEngine;
using System.Collections.Generic;
using System.Linq;

namespace MyGame
{
    /// <summary>
    /// A basic terrain object - nothing fancy here
    /// </summary>
    class Terrain : GameEntity
    {
        // Array of Cells it controls
        public Cell[] Model;

        /// <summary>
        /// First override, return Cells as a list, standard
        /// </summary>
        /// <returns></returns>
        public override List<Cell> GetCells()
        {
            return Model.OfType<Cell>().ToList();
        }

        /// <summary>
        /// Since we want to use the GameWorlds size in our
        /// wall spawning we can simply fill in the Model here in the
        /// Start() function as this is called once Registered with the GameWorld,
        /// ensuring we have _gameWorld set. 
        /// </summary>
        public override void Start()
        {
            Model = new Cell[_gameWorld.X];
            int i = 0;
            foreach(Cell cell in Model)
            {
                if (i == 10 || i == 48)
                {
                    i++;
                    continue;
                }

                Model[i] = new Cell(i, 5, "^") { IsWalkable = false };
                i++;
            }
        }
    }
}
