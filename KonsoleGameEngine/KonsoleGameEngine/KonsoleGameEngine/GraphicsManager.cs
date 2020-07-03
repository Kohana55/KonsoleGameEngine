using System;
using System.Text;

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

        /// <summary>
        /// String builder to construct the scene buffer
        /// </summary>
        private StringBuilder gameSceneBuilder;
        #endregion

        #region Ctors
        /// <summary>
        /// Standard Ctor must accept a reference to GameWorld
        /// </summary>
        /// <param name="argGameWorld">GameWorld to be rendered</param>
        public GraphicsManager(GameWorld argGameWorld, string title = "KonsoleGameEngine")
        {
            Console.Title = title;
            gameWorld = argGameWorld;
            Console.CursorVisible = false;
            Console.SetWindowSize(gameWorld.X + 2, gameWorld.Y + 5);
            Console.BufferWidth = gameWorld.X + 2;
            Console.BufferHeight = gameWorld.Y + 5;
            gameSceneBuilder = new StringBuilder(gameWorld.X * gameWorld.Y + 1);
        }
        #endregion

        #region Public
        /// <summary>
        /// Updates the current scene
        /// </summary>
        public void Update()
        {
            gameSceneBuilder.Clear();
            for (int y = 0; y < gameWorld.Y; y++)
            {
                // Draw World
                for (int x = 0; x < gameWorld.X; x++)
                {
                    gameSceneBuilder.Append(gameWorld.GetCellContents(x, y));
                }

                gameSceneBuilder.Append("\n");
            }

            // Draw UI
            gameSceneBuilder.Append(gameWorld.GetUIBuffer());

            gameScene = gameSceneBuilder.ToString();
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
