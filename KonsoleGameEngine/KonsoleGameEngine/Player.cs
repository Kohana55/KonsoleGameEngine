using KonsoleGameEngine;
using System;
using System.Collections.Generic;
using System.Threading;

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
        /// The Model can be singular or many Cells
        /// </summary>
        public Cell Model = new Cell(0, 0, "x");

        /// <summary>
        /// Reference to our Dog GameEntity
        /// </summary>
        public Dog _dog;

        /// <summary>
        /// Our first override from the GameEntity abstract parent class
        /// We must return all Cells we control for this entity so the GameWorld
        /// can put them into the world. The GameWorld is also made up of Cells so
        /// as you can imagine, this is just giving the GameWorld a list of cells to 
        /// override on its grid of Cells.
        /// 
        /// Our Player only has 1 Cell which we know because we made him - so rather
        /// than loop the Cells in Model adding to the List, just add the first element
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
            Thread animationThread = new Thread(Animate);
            animationThread.Start();
        }

        /// <summary>
        /// Register a Dog to the Player
        /// Swap references to each other 
        /// 
        /// Notice that in the Controller code we can
        /// then use our field _dog.CallDog() to call the dog
        /// </summary>
        /// <param name="dog"></param>
        public void RegisterDog(Dog dog)
        {
            _dog = dog;
            _dog.RegisterOwner(this);
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
                        if (!_gameWorld.GetCell(Model.X - 1, Model.Y).IsWalkable)
                            continue;
                        Model.X -= 1;
                    }
                }

                if (keypress.KeyChar == 'd')
                {
                    if (Model.X != _gameWorld.X - 1)
                    {
                        if (!_gameWorld.GetCell(Model.X + 1, Model.Y).IsWalkable)
                            continue;
                        Model.X += 1;
                    }
                }

                if (keypress.KeyChar == 'w')
                {
                    if (Model.Y != 0)
                    {
                        if (!_gameWorld.GetCell(Model.X, Model.Y-1).IsWalkable)
                            continue;
                        Model.Y -= 1;
                    }
                }

                if (keypress.KeyChar == 's')
                {
                    if (Model.Y != _gameWorld.Y - 1)
                    {
                        if (!_gameWorld.GetCell(Model.X, Model.Y+1).IsWalkable)
                            continue;
                        Model.Y += 1;
                    }
                }
            }
        }

        /// <summary>
        /// Just for fun, run this in its own thread
        /// kicked off when the Start() method is called just like
        /// Update! 
        /// </summary>
        private void Animate()
        {
            while(true)
            {
                if (Model.Contents == "x")
                    Model.Contents = "+";
                else
                    Model.Contents = "x";

                Thread.Sleep(350);
            }
        }
    }
}
