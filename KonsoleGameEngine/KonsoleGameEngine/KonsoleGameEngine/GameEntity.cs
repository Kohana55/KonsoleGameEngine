using System.Collections.Generic;

namespace KonsoleGameEngine
{
    abstract class GameEntity
    {
        /// <summary>
        /// Reference to the GameWorld the Entity is a part of
        /// </summary>
        public GameWorld _gameWorld;
        /// <summary>
        /// Return all the Cells this GameEntity is in control of
        /// </summary>
        /// <returns></returns>
        public abstract List<Cell> GetCells();
        /// <summary>
        /// For the purpose of kicking off a control thread and setup of an object
        /// </summary>
        public abstract void Start();
    }
}
