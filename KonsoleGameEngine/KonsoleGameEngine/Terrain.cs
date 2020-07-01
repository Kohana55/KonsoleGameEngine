using KonsoleGameEngine;
using System;
using System.Collections.Generic;

namespace MyGame
{
    /// <summary>
    /// A basic terrain object - nothing fancy here
    /// </summary>
    class Terrain : GameEntity
    {
        // Array of Cells it controls
        public List<Cell> Model;

        /// <summary>
        /// First override, return Cells as a list, standard
        /// </summary>
        /// <returns></returns>
        public override List<Cell> GetCells()
        {
            return Model;
        }

        /// <summary>
        /// Since we want to use the GameWorlds size in our
        /// wall spawning we can simply fill in the Model here in the
        /// Start() function as this is called once Registered with the GameWorld,
        /// ensuring we have _gameWorld set. 
        /// </summary>
        public override void Start()
        {
            Random rng = new Random();
            Model = new List<Cell>();
            double fillPercent = .2;

            for(int x = 0; x < 50; x++)
            {
                for(int y = 0; y < 20; y++)
                {
                    if(x < 3 && y < 3) //keep walls out of the starting area
                    {
                        continue;
                    }

                    if(rng.NextDouble() <= fillPercent)
                    {
                        Model.Add(new Cell(x, y, "█") { IsWalkable = false });
                    }
                }
            }
        }
    }
}
