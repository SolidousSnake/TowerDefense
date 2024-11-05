using _Project.Code.Core.Bootstrapper;
using _Project.Code.Presenter;
using _Project.Code.Services.TowerPlacement;
using _Project.Code.UI.View;
using _Project.Code.Utils;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Code.Core.DiContainer
{
    public sealed class SceneDiContainer : DiContainerBase
    {
        [SerializeField] private Grid _grid;
        [SerializeField] private GridView _gridView;
        [SerializeField] private TowerShopView _towerShopView;
        [SerializeField] private TowerSellView _towerSellView;
        
        protected override void AddDependencies(IContainerBuilder builder)
        {
            builder.RegisterInstance(_towerShopView);
            builder.RegisterInstance(_towerSellView);
            
            builder.AddSingleton<TowerShopPresenter>();
            builder.RegisterEntryPoint<GameplaySceneBootstrapper>();
            
            builder.RegisterEntryPoint<TowerPlacementService>()
                .WithParameter(Camera.main)
                .WithParameter(_gridView)
                .WithParameter(_grid)
                .AsSelf();
        }
    }
}