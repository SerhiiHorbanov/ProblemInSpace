using MyEngine.Render;
using SFML.System;
using MyEngine.GameObjects;
using MyEngine;

namespace ProblemInSpace.GameObjects
{
    class Bullet : SpaceObject
    {
        List<GameObject> gameObjects;
        Player player;

        private static readonly MyEngineSprite bulletSprite = MyEngineSprite.newSprite(new Vector2f(), bulletTexturePath);

        public float slowering = 0.2f;
        private const string bulletTexturePath = "Textures/bullet.png";

        private Bullet(MyEngineSprite sprite, Camera camera, List<GameObject> gameObjects, Vector2f position, Vector2f velocity ) : base(sprite, camera)
        {
            this.sprite = sprite;
            this.camera = camera;
            this.gameObjects = gameObjects;
            Position = position;
            this.velocity = velocity;
        }

        public static Bullet Instantiate(Scene scene, Player player)
        {
            float XVelocity = (float)Math.Cos(player.Rotation * ProblemInSpaceGame.degreesToRadiansMultiplayer) * 10;
            float YVelocity = (float)Math.Sin(player.Rotation * ProblemInSpaceGame.degreesToRadiansMultiplayer) * 10;

            float XPosition = player.Position.X + (float)Math.Cos(player.Rotation * ProblemInSpaceGame.degreesToRadiansMultiplayer) * player.WidthOnRender * 0.5f;
            float YPosition = player.Position.Y + (float)Math.Sin(player.Rotation * ProblemInSpaceGame.degreesToRadiansMultiplayer) * player.WidthOnRender * 0.5f;

            Vector2f position = new Vector2f(XPosition, YPosition);
            Vector2f velocity = new Vector2f(XVelocity, YVelocity);

            return new Bullet(new MyEngineSprite(bulletSprite), scene.camera, scene.gameObjects, position, velocity);
        }



        public void Update()
        {
            maxSpeed -= slowering;
            base.Update();
        }
    }
}
