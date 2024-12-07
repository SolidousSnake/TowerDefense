using _Project.Code.Core.Fsm;
using _Project.Code.Core.SceneManagement;
using _Project.Code.Gameplay.Repository;
using VContainer;

namespace _Project.Code.Gameplay.States
{
    public class RestartState : IState
    {
        [Inject] private readonly ISceneLoader _sceneLoader;
        [Inject] private readonly BuildingRepository _buildingRepository;
        
        public void Enter()
        {
            _buildingRepository.Clear();
            _sceneLoader.Load("Game");
        }

        public void Exit()
        {
        }
    }
}