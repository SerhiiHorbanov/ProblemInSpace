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

        public static readonly AsteroidData[] AsteroidDatas = new AsteroidData[1]
        {
            new AsteroidData(4, MyEngineSprite.newSprite(new(), smallAsteroidsPath + "/small asteroid1.png"))
        };
    }
}
