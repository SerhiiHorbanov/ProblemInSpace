using MyEngine.Render;
using SFML.System;
using MyEngine.GameObjects;
using MyEngine;
using MyEngine.Extensions;

namespace ProblemInSpace.GameObjects
{
    class Bullet : SpaceObject
    {
        List<GameObject> gameObjects;
        Player player;

        private static readonly MyEngineSprite bulletSprite = MyEngineSprite.newSprite(new Vector2f(), bulletTexturePath);

        public float slowering = 0.2f;

        private const string bulletTexturePath = "Textures/bullet.png";
        public const float bulletStartSpeed = 20;

        private Bullet(MyEngineSprite sprite, Player player, Camera camera, List<GameObject> gameObjects, Vector2f position, Vector2f velocity ) : base(sprite, camera)
        {
            this.sprite = sprite;
            this.player = player;
            this.camera = camera;
            this.gameObjects = gameObjects;
            Position = position;
            this.velocity = velocity;
        }

        public static Bullet Instantiate(Scene scene, Player player)
        {
            float XVelocity = (float)Math.Cos(player.Rotation * ProblemInSpaceGame.degreesToRadiansMultiplayer) * bulletStartSpeed;
            float YVelocity = (float)Math.Sin(player.Rotation * ProblemInSpaceGame.degreesToRadiansMultiplayer) * bulletStartSpeed;

            Vector2f velocity = new Vector2f(XVelocity, YVelocity);

            Bullet bullet = new Bullet(new MyEngineSprite(bulletSprite), player, scene.camera, scene.gameObjects, player.Position, velocity);

            bullet.Rotation = player.Rotation;

            bullet.maxSpeed = bulletStartSpeed;

            bullet.radius = 4;

            bullet.sprite.sprite.Origin = new Vector2f(bullet.sprite.sprite.Texture.Size.X * 0.5f, bullet.sprite.sprite.Texture.Size.Y * 0.5f);
            bullet.sprite.sprite.Scale = new Vector2f(4, 4);

            return bullet;
        }

        private void CheckCollisions()
        {
            foreach (GameObject gameObject in gameObjects)
            {
                CheckCollisionsWithGameObject(gameObject);
            }
        }

        private void CheckCollisionsWithGameObject(GameObject gameObject)
        {
            if (gameObject is SpaceObject && gameObject != this)
            {
                if (gameObject.toDestroy == false && gameObject != player)
                {
                    SpaceObject spaceObject = (SpaceObject)gameObject;
                    float neededDistanceForCollisionSquared = spaceObject.radius * spaceObject.radius;
                    if (Position.DistanceSquared(spaceObject.Position) < neededDistanceForCollisionSquared)
                    {
                        gameObject.toDestroy = true;
                        toDestroy = true;
                    }
                    return;
                }
            }
        }

        public override void Update()
        {
            maxSpeed -= slowering;
            toDestroy = maxSpeed < bulletStartSpeed - 5;

            CheckCollisions();

            base.Update();
        }
    }
}
