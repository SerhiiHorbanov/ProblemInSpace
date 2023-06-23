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

        public float radius;

        public Vector2f velocity = new Vector2f();
        public float rotationVelocity = 0;
        public float maxSpeed = 10;
        public float maxRotationVelocity = 30;

        public const string spaceObjectSpritePath = "Textures/space object.png";

        public float WidthOnRender
            => sprite.sprite.TextureRect.Width * sprite.sprite.Scale.X;

        public float HeightOnRender
            => sprite.sprite.TextureRect.Height * sprite.sprite.Scale.Y;

        protected SpaceObject(MyEngineSprite sprite, Camera camera)
        {
            this.sprite = sprite;
            this.camera = camera;
            radius = sprite.sprite.Scale.X * sprite.Width;
        }

        public static SpaceObject Instantiate(Scene scene)
        {
            MyEngineSprite sprite = MyEngineSprite.newSprite(new Vector2f(0, 0), spaceObjectSpritePath);

            sprite.sprite.Origin = new Vector2f(sprite.sprite.Texture.Size.X * 0.5f, sprite.sprite.Texture.Size.Y * 0.5f);

            return new SpaceObject(sprite, scene.camera);
        }

        protected void UpdatePosition()
        {
            float moveSpeedSquared = new Vector2f().DistanceSquared(velocity);
            if (moveSpeedSquared > maxSpeed * maxSpeed)
            {
                float moveSpeed =  new Vector2f().Distance(velocity);
                velocity /= moveSpeed;
                velocity *= maxSpeed;
            }
            Position += velocity;

            CheckOutOfBounds();
        }

        protected void CheckOutOfBounds()
        {
            if (camera.rectangle.Left > Position.X + (WidthOnRender * 0.5f) + 0.1f)
                Position = new Vector2f((camera.rectangle.Left + camera.rectangle.Width) + WidthOnRender, Position.Y);

            else if ((camera.rectangle.Left + camera.rectangle.Width) < Position.X - WidthOnRender - 0.1f)
                Position = new Vector2f(camera.rectangle.Left - (WidthOnRender * 0.5f), Position.Y);

            if (camera.rectangle.Top > Position.Y + (HeightOnRender * 0.5f))
                Position = new Vector2f(Position.X, (camera.rectangle.Top + camera.rectangle.Height) + HeightOnRender);

            else if ((camera.rectangle.Top + camera.rectangle.Height) < Position.Y - HeightOnRender)
                Position = new Vector2f(Position.X, camera.rectangle.Top - (HeightOnRender * 0.5f));
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
