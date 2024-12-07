using _Project.Code.Config;
using _Project.Code.Core.AssetManagement;
using _Project.Code.Core.Fsm;
using _Project.Code.Gameplay.Unit;
using _Project.Code.Services.Wallet;
using _Project.Code.UI.View.State.Gameplay;
using _Project.Code.Utils;
using VContainer;

namespace _Project.Code.Gameplay.States
{
    public class VictoryState : IState
    {
        [Inject] private readonly GameplayStateMachine _fsm;
        [Inject] private readonly VictoryStateView _view;
        [Inject] private readonly ConfigProvider _configProvider;
        [Inject] private readonly WalletService _walletService;
        [Inject] private readonly PlayerHealth _playerHealth;
        
        public async void Enter()
        {
            int reward = (int)_playerHealth.Points.Value * _configProvider.GetSingle<LevelConfig>().VictoryRewardPerHealth;
            _walletService.AddMenuCoins(reward);
            _view.SetReward(reward);
            
            var result = await _view.Open();

            switch (result)
            {
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