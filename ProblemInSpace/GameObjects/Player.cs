using MyEngine.GameObjects;
using MyEngine.GameObjects.Interfaces;
using MyEngine.Render;
using MyEngine.Input;
using MyEngine.Extensions;
using MyEngine;
using SFML.System;
using MyEngine.MyEngineSound;

namespace ProblemInSpace.GameObjects
{
    class Player : SpaceObject
    {
        PlayerInput input;

        Scene scene;

        float rotateSpeed = 1;
        float moveSpeed = 0.3f;

        public const string playerTexturePath = "Textures/player.png";
        public const float playerRadius = 12;

        private Player(PlayerInput input, MyEngineSprite sprite, Camera camera, Scene scene) : base(sprite, camera)
        {
            this.input = input;
            this.sprite = sprite;
            this.camera = camera;
            this.scene = scene;
        }

        public static Player Instantiate(Scene scene, PlayerInput input)
        {
            MyEngineSprite sprite = MyEngineSprite.newSprite(new Vector2f(0, 0), playerTexturePath);

            sprite.sprite.Origin = new Vector2f(sprite.sprite.Texture.Size.X * 0.5f, sprite.sprite.Texture.Size.Y * 0.5f);
            sprite.sprite.Scale = new Vector2f(4, 4);

            Player player = new Player(input, sprite, scene.camera, scene);

            player.OnDestroy += player.Destroy;

            float x = ProblemInSpaceGame.random.Next((int)scene.camera.rectangle.Left, (int)(scene.camera.rectangle.Left + scene.camera.rectangle.Width));
            float y = ProblemInSpaceGame.random.Next((int)scene.camera.rectangle.Top, (int)(scene.camera.rectangle.Top + scene.camera.rectangle.Height));
            player.Position = new Vector2f(x, y);

            player.radius = playerRadius;

            return player;
        }

        private void RotateRight()
            => rotationVelocity += rotateSpeed;

        private void RotateLeft()
            => rotationVelocity -= rotateSpeed;

        private void GoForward()
        {
            float XMove = (float)Math.Cos(Rotation * ProblemInSpaceGame.degreesToRadiansMultiplayer) * moveSpeed;
            float YMove = (float)Math.Sin(Rotation * ProblemInSpaceGame.degreesToRadiansMultiplayer) * moveSpeed;
            velocity += new Vector2f(XMove, YMove);
        }

        private void Shoot()
        {
            scene.Add(Bullet.Instantiate(scene, this));
        }

        private void CheckInput()
        {
            if (input.IsKeyPressed("forward"))
                GoForward();
            if (input.IsKeyPressed("right"))
                RotateRight();
            if (input.IsKeyPressed("left"))
                RotateLeft();
            if (input.IsKeyPressed("shoot") && !input.WasKeyPressed("shoot"))
                Shoot();
        }

        private void CheckCollisions()
        {
            foreach (GameObject gameObject in scene.gameObjects)
            {
                CheckCollisionWithGameObject(gameObject);
            }
        }

        private void CheckCollisionWithGameObject(GameObject gameObject)
        {
            if (gameObject is Asteroid)
            {
                Asteroid asteroid = (Asteroid)gameObject;
                float neededDistanceForCollisionSquared = (radius + asteroid.radius) * (radius + asteroid.radius);
                if (Position.DistanceSquared(asteroid.Position) < neededDistanceForCollisionSquared)
                {
                    toDestroy = true;
                }
                return;
            }
        }

        public override void Update()
        {
            CheckInput();
            CheckCollisions();

            base.Update();
        }

        public void Destroy()
        {
            SoundManager.PlaySound("metal pipe");
        }
    }
}
