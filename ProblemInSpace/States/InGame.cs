using MyEngine;
using MyEngine.Render;
using MyEngine.GameObjects;
using SFML.System;
using SFML.Graphics;
using ProblemInSpace.GameObjects;

namespace ProblemInSpace.States
{
    class InGame : State
    {
        public Scene scene;
        public override void Initialize()
        {
            Vector2f cameraCenter = new Vector2f(Game.window.Size.X * 0.5f, Game.window.Size.X * 0.5f);
            Vector2f cameraSize = new Vector2f(Game.window.Size.X, Game.window.Size.Y);
            Camera camera = new Camera(cameraCenter, cameraSize, Game.window, Color.Black);

            scene = new Scene(new List<GameObject> { }, camera);

            scene.Add(Player.Instantiate(scene));
            if (scene.gameObjects[0] is Player)
            {
                Player player = (Player)scene.gameObjects[0];
                player.Position = new Vector2f(375, 375);
                player.sprite.sprite.Color = new Color(255, 0, 0);
            }
        }

        public override void Input()
        {
            Game.window.DispatchEvents();
        }

        public override void Render()
        {
            scene.Render();
            Game.window.Display();
        }

        public override void Update()
        {
            scene.Update();
        }
    }
}
