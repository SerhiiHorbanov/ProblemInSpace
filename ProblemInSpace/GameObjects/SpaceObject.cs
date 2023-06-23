using MyEngine.GameObjects;
using MyEngine.GameObjects.Interfaces;
using MyEngine.Render;
using MyEngine;
using MyEngine.Extensions;
using SFML.System;

namespace ProblemInSpace.GameObjects
{
    class SpaceObject : GameObject, IUpdateable, IRenderable
    {
        public MyEngineSprite sprite;
        public Camera camera;
        public Vector2f Position
        {
            get
            {
                return sprite.position;
            }
            set
            {
                sprite.position = value;
            }
        }
        public float Rotation
        {
            get
            {
                return sprite.sprite.Rotation;
            }
            set
            {
                sprite.sprite.Rotation = value;
            }
        }

        public Vector2f moveVelocity = new Vector2f();
        public float rotationVelocity = 0;
        public float maxMoveSpeed = 10;
        public float maxRotationVelocity = 30;

        public const string spaceObjectSpritePath = "Textures/space object.png";

        public float Width
            => sprite.sprite.TextureRect.Width;

        public float Height
            => sprite.sprite.TextureRect.Height;

        protected SpaceObject(MyEngineSprite sprite, Camera camera)
        {
            this.sprite = sprite;
            this.camera = camera;
        }

        public static SpaceObject Instantiate(Scene scene)
        {
            MyEngineSprite sprite = MyEngineSprite.newSprite(new Vector2f(0, 0), spaceObjectSpritePath);

            sprite.sprite.Origin = new Vector2f(sprite.sprite.Texture.Size.X * 0.5f, sprite.sprite.Texture.Size.Y * 0.5f);

            return new SpaceObject(sprite, scene.camera);
        }

        protected void UpdatePosition()
        {
            float moveSpeedSquared = new Vector2f().DistanceSquared(moveVelocity);
            if (moveSpeedSquared > maxMoveSpeed * maxMoveSpeed)
            {
                float moveSpeed =  new Vector2f().Distance(moveVelocity);
                moveVelocity /= moveSpeed;
                moveVelocity *= maxMoveSpeed;
            }
            Position += moveVelocity;

            CheckOutOfBounds();
        }

        protected void CheckOutOfBounds()
        {
            if (camera.rectangle.Left > Position.X + (Width * 0.5f) + 0.1f)
                Position = new Vector2f((camera.rectangle.Left + camera.rectangle.Width) + Width, Position.Y);

            else if ((camera.rectangle.Left + camera.rectangle.Width) < Position.X - Width - 0.1f)
                Position = new Vector2f(camera.rectangle.Left - (Width * 0.5f), Position.Y);

            if (camera.rectangle.Top > Position.Y + (Height * 0.5f))
                Position = new Vector2f(Position.X, (camera.rectangle.Top + camera.rectangle.Height) + Height);

            else if ((camera.rectangle.Top + camera.rectangle.Height) < Position.Y - Height)
                Position = new Vector2f(Position.X, camera.rectangle.Top - (Height * 0.5f));
        }

        protected void UpdateRotation()
        {
            rotationVelocity = Math.Clamp(rotationVelocity, -30, 30);
            Rotation += rotationVelocity;
        }

        public virtual void Update()
        {
            UpdatePosition();
            UpdateRotation();
        }

        public void Render()
        {
            sprite.Render(camera);
        }
    }
}
