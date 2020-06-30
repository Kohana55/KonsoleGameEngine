using KonsoleGameEngine;
using System;
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
            for (int i = 0; i < 250; i++)
            {
                Model.Add(new Cell(rng.Next(50), rng.Next(20), "█") { IsWalkable = false });
            }
        }
    }
}
