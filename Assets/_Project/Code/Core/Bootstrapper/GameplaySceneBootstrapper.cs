using System;
using _Project.Code.Config;
using _Project.Code.Core.AssetManagement;
using _Project.Code.Core.Factory;
using _Project.Code.Core.Fsm;
using _Project.Code.Gameplay.Spawner;
using _Project.Code.Gameplay.States;
using _Project.Code.Gameplay.Unit;
using _Project.Code.Presenter;
using _Project.Code.Services.TowerPlacement;
using _Project.Code.Services.Wallet;
using _Project.Code.UI.Label;
using _Project.Code.Utils;
using VContainer;
using VContainer.Unity;
using UniRx;

namespace _Project.Code.Core.Bootstrapper
{
    public sealed class GameplaySceneBootstrapper : IInitializable, IDisposable
    {
        [Inject] private readonly ConfigProvider _configProvider;
        [Inject] private readonly StateFactory _stateFactory;
        [Inject] private readonly WalletService _walletService;
        [Inject] private readonly EnemySpawner _enemySpawner;

        [Inject] private readonly GameplayStateMachine _fsm;
        [Inject] private readonly TowerPlacementService _placementService;
        [Inject] private readonly TowerShopPresenter _towerShopPresenter;

        [Inject] private readonly WaveLabel _waveLabel;
        [Inject] private readonly HealthLabel _healthLabel;

        private readonly CompositeDisposable _cd;

        public GameplaySceneBootstrapper()
        {
            _cd = new CompositeDisposable();
        }
        
        public void Initialize()
        {
            WarmUpAssets();

            var levelConfig = _configProvider.GetSingle<LevelConfig>();
            var playerHealth = new PlayerHealth(levelConfig.MaxPlayerHealth);

            _walletService.ResetGameplayCoins();
            _walletService.AddGameplayCoins(levelConfig.InitialMoneyCount);

            _waveLabel.Initialize(_enemySpawner, levelConfig.Waves.Length);
            _enemySpawner.Initialize(levelConfig.Waves, playerHealth);
            _placementService.Initialize(levelConfig.PlacementLayer);
            _towerShopPresenter.Initialize();

            playerHealth.Points
                .Subscribe(_healthLabel.SetAmount).AddTo(_cd);
            
            playerHealth.Points
                .Where(points => points <= 0)
                .Subscribe(_ => _fsm.Enter<FailureState>()).AddTo(_cd);

            CreateStates();

            _fsm.Enter<PlayingState>();
        }

        private void WarmUpAssets()
        {
            _configProvider.LoadSingle<LevelConfig>(AssetPath.Config.LevelConfig);
            _configProvider.LoadSingle<TowerShopColors>(AssetPath.Config.TowerShopColors);
        }

        private void CreateStates()
        {
            _fsm.RegisterState(_stateFactory.Create<IntroState>());
            _fsm.RegisterState(_stateFactory.Create<PlayingState>());
            _fsm.RegisterState(_stateFactory.Create<FailureState>());
            _fsm.RegisterState(_stateFactory.Create<VictoryState>());

            _fsm.RegisterState(_stateFactory.Create<PauseState>());
            _fsm.RegisterState(_stateFactory.Create<RestartState>());
            _fsm.RegisterState(_stateFactory.Create<LoadMenuState>());
        }

        public void Dispose() => _cd.Dispose();
    }
}