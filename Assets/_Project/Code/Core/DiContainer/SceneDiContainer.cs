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
        [SerializeField] private TowerShopView _towerShopView;
        [SerializeField] private TowerPlacementView _towerPlacementView;
        [SerializeField] private TowerOperationView _towerOperationView;

        protected override void AddDependencies(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameplaySceneBootstrapper>();
            
            builder.RegisterInstance(_towerShopView);
            builder.RegisterInstance(_towerOperationView);
            builder.RegisterInstance(_towerPlacementView);

            builder.AddSingleton<TowerShopPresenter>();

            builder.RegisterEntryPoint<TowerPlacementService>().AsSelf();
        }
    }
}