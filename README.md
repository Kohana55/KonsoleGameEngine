# KonsoleGameEngine
KonsoleGameEngine seeks to make it easy to create Console games in C# .NET

# Included Example Program
<b>Program.cs</b> is an example programme showing how to use the Konsole Game Engine!

The basic premise is to create the <b>GameWorld</b> and <b>GraphicsManager</b> objects then set them running in a loop calling their relevant Start(), Update() & Draw() functions.

Each custom element you wish to add to the <b>GameWorld</b> must derive from <b>GameEntity</b> and be registered with the <b>GameWorld</b> using the .RegisterEntity() function.

<b>GameEntity</b> is an abstract class that forces you to override the Update and Start functions and provides a reference to the <b>GameWorld</b>, which is set when you register the entity.

In the Example <b>Program.cs</b> we add a custom entity, <b>Player.cs</b>.

# ToolSet
Cells and a GameWorld (of Cells)

Curser Entity to select Cells

AStar Pathfinding

GraphicsManager to draw the game in console neatly (without flickering)

GameEntity abstract class to create custom entities
