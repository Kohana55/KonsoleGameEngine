using KonsoleGameEngine;
using System.Threading;

namespace MyGame
{
    /// <summary>
    /// Example Programme showing how to use the Konsole Game Engine!
    /// 
    /// The basic premise is to give yourself a GameWorld and a GraphicsManager 
    /// then set them running in a loop calling their Update() functions (and Draw() for the graphics).
    /// 
    /// Each custom element you wish to add to the GameWorld must derive from GameEntity
    /// and added to the GameWorld using the .RegisterEntity() method.
    /// 
    /// GameEntity is an abstract class that forces you to write the Update and Start functions and provides
    /// a reference property for the GameWorld, which is set when you register the entity.
    /// 
    /// In the Example below we add a custom entity, player.
    /// 
    /// Once you have read the below example code to setup a basic game, head over to the
    /// Player class for an example on how to write your own entities
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // You will always need a GameWorld and a GraphicsManager
            var game = new GameWorld();
            var graphicsManager = new GraphicsManager(game);
            
            // Custom Entity written by "you"
            var player = new Player();
            var dog = new Dog(player.Model[0].X+2, player.Model[0].Y+1);
            player.RegisterDog(dog);

            // Register your custom Entity
            game.RegisterEntity(player);
            game.RegisterEntity(dog);

            // Standard Game Loop
            game.Start();
            while (true)
            {
                // Update the game
                game.Update();

                // Update & draw the scene
                graphicsManager.Update();
                graphicsManager.Draw();

                // Just so we don't hammer the CPU
                Thread.Sleep(1);
            }
        }
    }
}
