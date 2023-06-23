using SFML.System;
using MyEngine;
using MyEngine.Render;

namespace ProblemInSpace.GameObjects
{
    class Asteroid : SpaceObject
    {
        public const float maxStartVelocity = 10;

        AsteroidData data; 
        private Asteroid(AsteroidData data, MyEngineSprite sprite, Camera camera) : base(sprite, camera)
        {
            this.data = data;
            this.sprite = sprite;
            this.camera = camera;
        }

        public static Asteroid Instantiate(Camera camera)
        {
            AsteroidData data = AsteroidData.AsteroidDatas[ProblemInSpaceGame.random.Next(0, AsteroidData.AsteroidDatas.Length)];

            MyEngineSprite sprite = new MyEngineSprite(data.sprite);

            sprite.sprite.Origin = new Vector2f(sprite.sprite.Texture.Size.X * 0.5f, sprite.sprite.Texture.Size.Y * 0.5f);
            sprite.sprite.Scale = new Vector2f(4, 4);

            Asteroid asteroid = new Asteroid(data, sprite, camera);
            
            float positionX = camera.center.X;
            float positionY = camera.center.Y;

            while (camera.rectangle.Contains(positionX, positionY))
            {
                positionX = ProblemInSpaceGame.random.Next((int)(camera.center.X - camera.size.X), (int)(camera.center.X + camera.size.X));
                positionY = ProblemInSpaceGame.random.Next((int)(camera.center.Y - camera.size.Y), (int)(camera.center.Y + camera.size.Y));
            }

            asteroid.Position = new Vector2f(positionX, positionY);

            float velocityX = ProblemInSpaceGame.random.Next((int)-maxStartVelocity, (int)maxStartVelocity);
            float velocityY = ProblemInSpaceGame.random.Next((int)-maxStartVelocity, (int)maxStartVelocity);

            asteroid.velocity = new Vector2f(velocityX, velocityY);

            return asteroid;
        }

        
    }
}
