using _Project.Code.Core.Fsm;
using _Project.Code.UI.View.State;
using _Project.Code.UI.View.State.Gameplay;
using _Project.Code.Utils;
using VContainer;

namespace _Project.Code.Gameplay.States
{
    public class FailureState : IState
    {
        [Inject] private readonly GameplayStateMachine _fsm;
        [Inject] private readonly FailureStateView _view;

        public async void Enter()
        {
            var result = await _view.Open();

            switch (result)
            {
                case TargetStates.Restart:
                    _fsm.Enter<RestartState>();
                    break;
                case TargetStates.LoadMenu:
                    _fsm.Enter<LoadMenuState>();
                    break;
                default:
                    throw new System.ArgumentOutOfRangeException();
            }
        }

        public void Exit()
        {
            _view.Hide();
        }
    }
}