using _Project.Code.Core.AssetManagement;
using _Project.Code.Core.Fsm;
using _Project.Code.Core.Timers;
using _Project.Code.Data.Config;

namespace _Project.Code.Gameplay.States
{
    public class IntroState : IState
    {
        private readonly GameplayStateMachine _fsm;
        private readonly CountdownTimer _timer;

        public IntroState(ConfigProvider configProvider
            , GameplayStateMachine fsm)
        {
            _fsm = fsm;
            _timer = new CountdownTimer(configProvider.GetSingle<LevelConfig>().FirstSpawnDelay);
        }

        public void Enter()
        {
            _timer.OnFinish += _fsm.Enter<PlayingState>;
            _timer.Start();
        }

        public void Exit() => _timer.OnStart -= _fsm.Enter<PlayingState>;
    }
}