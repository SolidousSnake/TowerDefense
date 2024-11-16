using _Project.Code.Core.Bootstrapper;
using _Project.Code.Core.Factory;
using _Project.Code.Core.Fsm;
using _Project.Code.Gameplay.Point;
using _Project.Code.Gameplay.Spawner;
using _Project.Code.Presenter;
using _Project.Code.Services.TowerPlacement;
using _Project.Code.UI.View;
using _Project.Code.UI.View.State;
using _Project.Code.Utils;
using Alchemy.Inspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Code.Core.DiContainer
{
    public sealed class SceneDiContainer : DiContainerBase
    {
        [SerializeField] private SpawnPoint _spawnPoint;
        [SerializeField] private WayPoint[] _wayPoints;

        [BoxGroup("UI")] [SerializeField] private PauseStateView _pauseStateView;
        [BoxGroup("UI")] [SerializeField] private TowerShopView _towerShopView;
        [BoxGroup("UI")] [SerializeField] private TowerPlacementView _towerPlacementView;
        [BoxGroup("UI")] [SerializeField] private TowerOperationView _towerOperationView;

        protected override void AddDependencies(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameplaySceneBootstrapper>();

            builder.AddSingleton<GameplayStateMachine>();
            builder.AddSingleton<StateFactory>();

            builder.RegisterInstance(_pauseStateView);
            builder.RegisterInstance(_towerShopView);
            builder.RegisterInstance(_towerOperationView);
            builder.RegisterInstance(_towerPlacementView);

            builder.RegisterInstance(_spawnPoint);
            builder.RegisterInstance(_wayPoints);

            builder.AddSingleton<EnemySpawner>();
            builder.AddSingleton<TowerShopPresenter>();

            builder.RegisterEntryPoint<TowerPlacementService>().AsSelf();
        }
    }
}