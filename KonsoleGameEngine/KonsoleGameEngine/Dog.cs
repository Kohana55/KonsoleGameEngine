using KonsoleGameEngine;
using System.Collections.Generic;
using System.Threading;

namespace MyGame
{
    class Dog : GameEntity
    {
        /// <summary>
        /// A little bit more complicated than the Player
        /// The Dog contains a few elements, the first and obvious is a Cell.
        /// Then a reference to the player who owns us, but isn't "needed" for the 
        /// dog to exist.
        /// And an A* pathfinder along with a _path to hold our path
        /// </summary>
        public Cell Model = new Cell(0, 0, "O") { IsWalkable = false };
        private Player _player;
        private AStarPathfinder _pathFinder;
        private List<PathNode> _path = new List<PathNode>();

        /// <summary>
        /// As player summary...
        /// </summary>
        /// <returns></returns>
        public override List<Cell> GetCells()
        {
            return new List<Cell> { Model };
        }

        /// <summary>
        /// In this override notice the setting up of the pathfinder.
        /// This isn't done during construction of our object because our
        /// local _gameWorld from our baseclass is null; it wont be set until this 
        /// entity is registered with the game world. 
        /// 
        /// Once registered, this function will be called when GameWorld calls Start()
        /// Allowing us to finalise any object setup we need that relies on _gameWorld
        /// </summary>
        public override void Start()
        {
            _pathFinder = new AStarPathfinder(_gameWorld);

            Thread doggyUpdateThread = new Thread(Update);
            doggyUpdateThread.Start();
            Thread animationThread = new Thread(Animate);
            animationThread.Start();
        }

        /// <summary>
        /// Register the "owner" of this Dog...
        /// 
        /// ...and steal his position and sit right next to him!
        /// *Good Boy*
        /// </summary>
        /// <param name="player"></param>
        public void RegisterOwner(Player player)
        {
            _player = player;
            Model.X = _player.Model.X + 1;
            Model.Y = _player.Model.Y;
        }

        /// <summary>
        /// Sets up a path when called
        /// </summary>
        /// <param name="dest"></param>
        private void FindPlayer(Cell dest)
        {
            if (Model == dest)
                return;
            _path = _pathFinder.CalculatePath(Model, dest, true);
            if (_path.Count > 2)
            {
                _path.RemoveAt(0);
                _path.RemoveAt(_path.Count - 1);
            }
        }

        /// <summary>
        /// The Player has a Controller thread, which seems like a fitting name.
        /// This is essentially our Dogs 'Controller', Update seemed like a better name.
        /// 
        /// Notice this is kicked off during the Start(), right after we
        /// setup our pathfinder
        /// </summary>
        private void Update()
        {
            while(true)
            {
                if (_path.Count>0)
                    FollowPlayer();

                
                FindPlayer(_player.Model);

                Thread.Sleep(400);
            }
        }

        /// <summary>
        /// Moves the Dog's Model to the next Cell on its path
        /// 
        /// The path is ordered, so we're using the first element of 
        /// the path, then deleting it. 
        /// 
        /// It would be better practice to check if the _path is empty here
        /// rather than in the Update function. But this is YOUR code, you fix it! :)
        /// </summary>
        private void FollowPlayer()
        {
            Model.X = _path[0].X;
            Model.Y = _path[0].Y;
            _path.RemoveAt(0);
        }

        /// <summary>
        /// Just for fun, run this in its own thread
        /// kicked off when the Start() method is called just like
        /// Update! 
        /// </summary>
        private void Animate()
        {
            while (true)
            {
                if (Model.Contents == "O")
                    Model.Contents = "o";
                else
                    Model.Contents = "O";

                Thread.Sleep(350);
            }
        }
    }
}
