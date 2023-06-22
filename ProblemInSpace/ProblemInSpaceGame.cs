using MyEngine;
using ProblemInSpace.States;

namespace ProblemInSpace
{
    class ProblemInSpaceGame : Game
    {
        protected override void SetStartingState()
        {
            state = new InGame();
        }
    }
}
