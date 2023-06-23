using MyEngine.GameObjects;
using MyEngine.GameObjects.Interfaces;
using MyEngine.Render;
using MyEngine;
using SFML.System;

namespace ProblemInSpace.GameObjects
{
    class Player : SpaceObject
    {
        float rotateSpeed = 1;
        float moveSpeed = 0.3f;

        public const string playerTexturePath = "Textures/player.png";
        public const float degreesToRadiansMultiplayer = 0.0174532925f;

        private Player(MyEngineSprite sprite, Camera camera) : base(sprite, camera)
        {
            this.sprite = sprite;
            this.camera = camera;
        }

        public static new Player Instantiate(Scene scene)
        {
            MyEngineSprite sprite = MyEngineSprite.newSprite(new Vector2f(0, 0), playerTexturePath);

            sprite.sprite.Origin = new Vector2f(sprite.sprite.Texture.Size.X * 0.5f, sprite.sprite.Texture.Size.Y * 0.5f);

            return new Player(sprite, scene.camera);
        }

        public void RotateRight()
            => rotationVelocity += rotateSpeed;

        public void RotateLeft()
            => rotationVelocity -= rotateSpeed;

        public void GoForward()
        {
            float XMove = (float)Math.Cos(Rotation * degreesToRadiansMultiplayer) * moveSpeed;
            float YMove = (float)Math.Sin(Rotation * degreesToRadiansMultiplayer) * moveSpeed;
            moveVelocity += new Vector2f(XMove, YMove);
        }

        public override void Update()
        {
            RotateRight();
            GoForward();
            UpdatePosition();
            UpdateRotation();
        }
    }
}
