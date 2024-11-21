using _Project.Code.Core.Fsm;
using _Project.Code.Core.SceneManagement;
using VContainer;

namespace _Project.Code.Gameplay.States
{
    public class RestartState : IState
    {
        [Inject] private readonly ISceneLoader _sceneLoader;
        
        public void Enter()
        {
            _sceneLoader.Load("Game");
        }

        public void Exit()
        {
        }
    }
}