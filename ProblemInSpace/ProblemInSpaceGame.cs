using MyEngine;
using ProblemInSpace.States;
using SFML.Window;
using SFML.Graphics;

namespace ProblemInSpace
{
    class ProblemInSpaceGame : Game
    {
        protected override void SetStartingState()
        {
            SetState(new InGame());
        }
    }
}
