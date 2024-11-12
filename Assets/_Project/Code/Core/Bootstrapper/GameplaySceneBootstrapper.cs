using _Project.Code.Config;
using _Project.Code.Core.AssetManagement;
using _Project.Code.Presenter;
using _Project.Code.Services.TowerPlacement;
using _Project.Code.Services.Wallet;
using _Project.Code.Utils;
using VContainer;
using VContainer.Unity;

namespace _Project.Code.Core.Bootstrapper
{
    public class  GameplaySceneBootstrapper : IInitializable
    {
        [Inject] private readonly ConfigProvider _configProvider;
        [Inject] private readonly TowerPlacementService _placementService;
        [Inject] private readonly TowerShopPresenter _towerShopPresenter;
        [Inject] private readonly WalletService _walletService;

        public void Initialize()
        {
            _configProvider.LoadSingle<LevelConfig>(AssetPath.Config.LevelConfig);
            _configProvider.LoadSingle<TowerShopColors>(AssetPath.Config.TowerShopColors);

            var levelConfig = _configProvider.GetSingle<LevelConfig>();
            _placementService.Initialize(levelConfig.PlacementLayer);
            _towerShopPresenter.Initialize();
            
            _walletService.AddGameplayCoins(levelConfig.InitialMoneyCount);
        }
    }
}