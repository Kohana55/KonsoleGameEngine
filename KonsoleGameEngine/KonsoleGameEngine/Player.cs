using System;
using System.Collections.Generic;
using System.Threading;
using KonsoleGameEngine;

namespace MyGame
{
    /// <summary>
    /// Example of a custom GameEntity
    /// 
    /// Must derive from GameEntity
    /// Must override GetCells() and Start()
    /// Has acces to the GameWorld via _gameWorld
    /// </summary>
    class Player : GameEntity
    {

        /// <summary>
        /// The Model represents the Cell for this Entity
        /// 
        /// The Model can be singular or an array/list and as such,
        /// it is not included in the abstract class.
        /// </summary>
        private Cell Model = new Cell(0, 0, "x") { IsWalkable = false };

        /// <summary>
        /// Our first override from the GameEntity abstract parent class
        /// We must return all Cells we control for this entity so the GameWorld
        /// can put them into the world. The GameWorld is also made up of Cells so
        /// as you can imagine, this is just giving the GameWorld a list of cells to 
        /// override on its grid of Cells.
        /// </summary>
        /// <returns></returns>
        public override List<Cell> GetCells()
        {
            return new List<Cell> { Model };
        }

        /// <summary>
        /// The second override from GameEntity is Start() which is used to kick off
        /// any threads you may want to control this entity. 
        /// 
        /// For example - here we are a "player" and a player needs a controller right?
        /// So we use the Start() function to kick off the Controller method in a thread
        /// </summary>
        public override void Start()
        {
            Thread playerControllerThread = new Thread(Controller);
            playerControllerThread.Start();
        }

        /// <summary>
        /// I am running in my own thread - thank you Start() function
        /// 
        /// Simple movement controls using input from the Console then adding 1
        /// to the X or Y.
        /// 
        /// Notice we are referencing the _gameWorld.X and .Y to check bounds before we update Cell
        /// _gameWorld is a field in the GameEntity abstract class we inherit from and is set when 
        /// you register the GameEntity with the GameWorld.
        /// </summary>
        private void Controller()
        {
            ConsoleKeyInfo keypress;

            while (true)
            {

                keypress = Console.ReadKey(true);

                // Update players new position after keypress
                if (keypress.KeyChar == 'a')
                {
                    if (Model.X != 0)
                    {
                        Model.X -= 1;
                    }
                }

                if (keypress.KeyChar == 'd')
                {
                    if (Model.X != _gameWorld.X - 1)
                    {
                        Model.X += 1;
                    }
                }

                if (keypress.KeyChar == 'w')
                {
                    if (Model.Y != 0)
                    {
                        Model.Y -= 1;
                    }
                }

                if (keypress.KeyChar == 's')
                {
                    if (Model.Y != _gameWorld.Y - 1)
                    {
                        Model.Y += 1;
                    }
                }
            }
        }
    }
}
