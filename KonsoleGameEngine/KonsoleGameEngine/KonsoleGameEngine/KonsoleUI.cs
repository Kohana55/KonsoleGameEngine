using System;
using System.Collections.Generic;
using System.Text;

namespace KonsoleGameEngine
{
    /// <summary>
    /// 
    /// </summary>
    abstract class KonsoleUI
    {
        /// <summary>
        /// Reference to the GameWorld the UI belongs to
        /// </summary>
        protected GameWorld _gameWorld;

        /// <summary>
        /// UI buffer to be drawn
        /// </summary>
        protected string _uiBuffer;

        /// <summary>
        /// UI builder to construct the UI buffer
        /// </summary>
        protected StringBuilder _uiBufferBuilder = new StringBuilder();

        /// <summary>
        /// Returns the complete UI to the GameWorld
        /// </summary>
        /// <returns></returns>
        public string GetUI()
        {
            return _uiBuffer;
        }

        /// <summary>
        /// Build custom UI with this override
        /// </summary>
        public abstract void Update();

        internal void RegisterGameWorld(GameWorld gameWorld)
        {
            _gameWorld = gameWorld;
        }
    }
}
