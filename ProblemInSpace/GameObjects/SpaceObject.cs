using MyEngine.GameObjects;
using MyEngine.GameObjects.Interfaces;
using MyEngine.Render;
using MyEngine;
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
        public float maxMoveVelocity = 10;
        public float maxRorationVelocity = 30;

        public const string spaceObjectSpritePath = "Textures/space object.png";

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
            Position += moveVelocity;

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
