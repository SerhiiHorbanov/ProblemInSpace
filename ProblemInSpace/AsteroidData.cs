using MyEngine.Render;
namespace ProblemInSpace
{
    class AsteroidData
    {
        public readonly float radius;

        public readonly MyEngineSprite sprite;

        private const string smallAsteroidsPath = "Textures/asteroids/small asteroids";

        private const string bigAsteroidsPath = "Textures/asteroids/big asteroids";

        private AsteroidData(float radius, MyEngineSprite sprite)
        {
            this.radius = radius;
            this.sprite = sprite;
        }

        public static readonly AsteroidData[] AsteroidDatas = new AsteroidData[8]
        {
            new AsteroidData(12, MyEngineSprite.newSprite(new(), smallAsteroidsPath + "/small asteroid1.png")),
            new AsteroidData(12, MyEngineSprite.newSprite(new(), smallAsteroidsPath + "/small asteroid2.png")),
            new AsteroidData(12, MyEngineSprite.newSprite(new(), smallAsteroidsPath + "/small asteroid3.png")),
            new AsteroidData(12, MyEngineSprite.newSprite(new(), smallAsteroidsPath + "/small asteroid4.png")),
            new AsteroidData(36, MyEngineSprite.newSprite(new(), bigAsteroidsPath + "/big asteroid1.png")),
            new AsteroidData(36, MyEngineSprite.newSprite(new(), bigAsteroidsPath + "/big asteroid2.png")),
            new AsteroidData(36, MyEngineSprite.newSprite(new(), bigAsteroidsPath + "/big asteroid3.png")),
            new AsteroidData(36, MyEngineSprite.newSprite(new(), bigAsteroidsPath + "/big asteroid4.png")),
        };
    }
}
