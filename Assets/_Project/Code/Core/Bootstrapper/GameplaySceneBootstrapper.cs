using _Project.Code.Config;
using _Project.Code.Core.AssetManagement;
using _Project.Code.Core.Factory;
using _Project.Code.Core.Fsm;
using _Project.Code.Gameplay.States;
using _Project.Code.Presenter;
using _Project.Code.Services.TowerPlacement;
using _Project.Code.Services.Wallet;
using _Project.Code.Utils;
using VContainer;
using VContainer.Unity;

namespace _Project.Code.Core.Bootstrapper
{
    public sealed class GameplaySceneBootstrapper : IInitializable
    {
        [Inject] private readonly ConfigProvider _configProvider;
        [Inject] private readonly StateFactory _stateFactory;
        [Inject] private readonly WalletService _walletService;

        [Inject] private readonly GameplayStateMachine _fsm; 
        [Inject] private readonly TowerPlacementService _placementService;
        [Inject] private readonly TowerShopPresenter _towerShopPresenter;
        
        public void Initialize()
        {
            _configProvider.LoadSingle<LevelConfig>(AssetPath.Config.LevelConfig);
            _configProvider.LoadSingle<TowerShopColors>(AssetPath.Config.TowerShopColors);
            
            var levelConfig = _configProvider.GetSingle<LevelConfig>();
            
            _walletService.AddGameplayCoins(levelConfig.InitialMoneyCount);
            
            _placementService.Initialize(levelConfig.PlacementLayer);
            _towerShopPresenter.Initialize();
            
            CreateStates();
            _fsm.Enter<IntroState>();
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
    }
}