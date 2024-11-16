using _Project.Code.Config;
using _Project.Code.Core.AssetManagement;
using _Project.Code.Core.Fsm;
using Cysharp.Threading.Tasks;
using VContainer;

namespace _Project.Code.Gameplay.States
{
    public class IntroState : IState
    {
        [Inject] private ConfigProvider _configProvider;
        [Inject] private GameplayStateMachine _fsm;
        
        public async void Enter()
        {
            await UniTask.WaitForSeconds(_configProvider.GetSingle<LevelConfig>().FirstSpawnDelay);
            _fsm.Enter<PlayingState>();
        }

        public void Exit()
        {
        }
    }
}