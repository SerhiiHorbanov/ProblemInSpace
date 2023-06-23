using MyEngine;
using ProblemInSpace.States;
using SFML.Window;
using SFML.Graphics;

namespace ProblemInSpace
{
    class ProblemInSpaceGame : Game
    {
        public const float degreesToRadiansMultiplayer = 0.0174532925f;//я не знаю куди це засунути :sob:, я не хочу створювати новий клас чисто заради
        public static Random random = new Random();
        protected override void SetStartingState()
        {
            SetState(new InGame());
        }
    }
}
