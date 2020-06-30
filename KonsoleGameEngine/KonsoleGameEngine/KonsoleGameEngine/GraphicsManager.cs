using System;

namespace KonsoleGameEngine
{
    class GraphicsManager
    {
        #region Fields
        /// <summary>
        /// Reference to the game world to be rendered
        /// Set by the Ctor
        /// </summary>
        private GameWorld gameWorld;

        /// <summary>
        /// Scene buffer to be drawn
        /// </summary>
        private string gameScene;
        #endregion

        #region Ctors
        /// <summary>
        /// Standard Ctor must accept a reference to GameWorld
        /// </summary>
        /// <param name="argGameWorld">GameWorld to be rendered</param>
        public GraphicsManager(GameWorld argGameWorld)
        {
            Console.Title = "Space Invaders";
            gameWorld = argGameWorld;
            Console.CursorVisible = false;
            Console.SetWindowSize(gameWorld.X + 2, gameWorld.Y + 1);
            Console.BufferWidth = gameWorld.X + 2;
            Console.BufferHeight = gameWorld.Y + 1;
        }
        #endregion

        #region Public
        /// <summary>
        /// Updates the current scene
        /// </summary>
        public void Update()
        {
            gameScene = "";
            for (int y = 0; y < gameWorld.Y; y++)
            {
                for (int x = 0; x < gameWorld.X; x++)
                {
                    gameScene += gameWorld.GetCellContents(x, y);
                }

                gameScene += "\n";
            }
        }

        /// <summary>
        /// Draws the current scene
        /// </summary>
        public void Draw()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(gameScene);
        }
        #endregion
    }
}
