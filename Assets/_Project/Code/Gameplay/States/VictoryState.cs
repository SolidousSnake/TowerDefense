using _Project.Code.Core.AssetManagement;
using _Project.Code.Core.Fsm;
using _Project.Code.Data.Config;
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
            var config = _configProvider.GetSingle<LevelConfig>();
            int health = (int)_playerHealth.Points.Value;
            
            int bonusReward = health * config.RewardPerHealth;
            int reward = bonusReward + config.VictoryReward;
            
            int stars = health <= config.HealthForOneStar ? 1 : (health <= config.HealthForTwoStars ? 2 : 3);

            _walletService.AddMenuCoins(reward);
            _view.SetBonusReward(bonusReward);
            _view.SetTotalReward(reward);
            _view.ToggleStars(stars);
            
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