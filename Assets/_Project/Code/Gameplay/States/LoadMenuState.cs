using _Project.Code.Core.Fsm;
using _Project.Code.Core.SceneManagement;
using _Project.Code.Utils;
using VContainer;

namespace _Project.Code.Gameplay.States
{
    public class LoadMenuState : IState
    {
        [Inject] private readonly ISceneLoader _sceneLoader;
        
        public void Enter()
        {
            _sceneLoader.Load(Constants.Scene.Lobby);
        }

        public void Exit()
        {
        }
    }
}