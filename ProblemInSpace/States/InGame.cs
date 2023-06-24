using MyEngine;
using MyEngine.Render;
using MyEngine.GameObjects;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using ProblemInSpace.GameObjects;
using MyEngine.Input;
using MyEngine.MyEngineSound;

namespace ProblemInSpace.States
{
    class InGame : State
    {
        public Scene scene;
        public PlayerInput input = new PlayerInput();
        public override void Initialize()
        {
            SoundManager.LoadSound("metal pipe.wav", "metal pipe");

            input.AddBinding("forward", Keyboard.Key.W);
            input.AddBinding("right", Keyboard.Key.D);
            input.AddBinding("left", Keyboard.Key.A);
            input.AddBinding("shoot", Keyboard.Key.E);

            Vector2f cameraCenter = new Vector2f(Game.window.Size.X * 0.5f, Game.window.Size.X * 0.5f);
            Vector2f cameraSize = new Vector2f(Game.window.Size.X, Game.window.Size.Y);
            Camera camera = new Camera(cameraCenter, cameraSize, Game.window, Color.Black);

            scene = new Scene(new List<GameObject>(), camera);

            scene.Add(Player.Instantiate(scene, input));

            scene.Add(Asteroid.Instantiate(scene.camera));
        }

        public override void Input()
        {
            Game.window.DispatchEvents();
            input.UpdateKeyInput();
        }

        public override void Render()
        {
            scene.Render();
        }

        public override void Update()
        {
            scene.Update();
        }
    }
}
