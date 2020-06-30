using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KonsoleGameEngine;

namespace MyGame
{
    class Dog : GameEntity
    {
        public override Cell[] Model { get; } = { new Cell(0,0,"O") };

        // Control fields for being called by the Player
        private Player _player;
        public bool Called { get; set; }
        private List<PathNode> _path = new List<PathNode>();
        private Cell destinationCell;

        public Dog(int x, int y)
        {
            Model[0].X += x;
            Model[0].Y += y;
        }

        public override List<Cell> GetCells()
        {
            return new List<Cell> { Model[0] };
        }

        public override void Start()
        {
            Thread doggyUpdateThread = new Thread(Update);
            doggyUpdateThread.Start();
        }

        public void CallDog(Cell dest)
        {
            destinationCell = dest;
            _path = _gameWorld.GetPath(Model[0], dest);
        }

        private void Update()
        {
            int counter = 0;
            while(true)
            {
                if (_path.Count>0)
                    FollowPath();
                else
                {
                    if (counter > 10) { IdleMovement(); counter = 0; }
                }

                Thread.Sleep(100);
                counter++;
            }
        }

        private void FollowPath()
        {
            Model[0].X = _path[0].X;
            Model[0].Y = _path[0].Y;
            _path.RemoveAt(0);
        }

        // Move to Random Walkable spot
        private void IdleMovement()
        {

        }

        internal void RegisterOwner(Player player)
        {
            _player = player;
        }
    }
}
