using _Project.Code.Core.Bootstrapper;
using _Project.Code.Core.Factory;
using _Project.Code.Core.Fsm;
using _Project.Code.Gameplay.Point;
using _Project.Code.Gameplay.Repository;
using _Project.Code.Gameplay.Spawner;
using _Project.Code.Gameplay.Unit;
using _Project.Code.Presenter;
using _Project.Code.Services.Tower;
using _Project.Code.UI.Label;
using _Project.Code.UI.View;
using _Project.Code.UI.View.State.Gameplay;
using _Project.Code.Utils;
using NaughtyAttributes;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Code.Core.DiContainer
{
    public sealed class GameplaySceneDiContainer : DiContainerBase
    {
        [Space(25f)]

        [BoxGroup("Points")] [SerializeField] private SpawnPoint _spawnPoint;
        [BoxGroup("Points")] [SerializeField] private WayPoint[] _wayPoints;

        [BoxGroup("UI")] [SerializeField] private HealthLabel _healthLabel;
        [BoxGroup("UI")] [SerializeField] private WaveLabel _waveLabel;
        [Space]
        [BoxGroup("UI")] [SerializeField] private PauseStateView _pauseStateView;
        [BoxGroup("UI")] [SerializeField] private FailureStateView _failureStateView;
        [BoxGroup("UI")] [SerializeField] private VictoryStateView _victoryStateView;
        [Space]
        [BoxGroup("UI")] [SerializeField] private TowerShopView _towerShopView;
        [BoxGroup("UI")] [SerializeField] private TowerPlacementView _towerPlacementView;
        [BoxGroup("UI")] [SerializeField] private TowerOperationView _towerOperationView;

        public LayerMask _layerMask;
        
        protected override void AddDependencies(IContainerBuilder builder)
        {
            RegisterUI(builder);
            RegisterServices(builder);
            RegisterRepository(builder);

            builder.AddSingleton<GameplayStateMachine>();
            builder.AddSingleton<StateFactory>();
            builder.AddSingleton<PlayerHealth>();
            
            builder.RegisterInstance(_spawnPoint);
            builder.RegisterInstance(_wayPoints);

            builder.AddSingleton<EnemySpawner>();
            builder.AddSingleton<TowerShopPresenter>();

            builder.RegisterEntryPoint<GameplaySceneBootstrapper>();
        }

        private void RegisterRepository(IContainerBuilder builder)
        {
            builder.AddSingleton<EnemyRepository>();
            builder.AddSingleton<BuildingRepository>();
        }

        private void RegisterServices(IContainerBuilder builder)
        {
            builder.AddSingleton<TowerOperationService>();
            builder.RegisterEntryPoint<TowerPlacementService>().AsSelf().WithParameter(_layerMask);
        }

        private void RegisterUI(IContainerBuilder builder)
        {
            builder.RegisterInstance(_healthLabel);
            builder.RegisterInstance(_waveLabel);
            
            builder.RegisterInstance(_pauseStateView);
            builder.RegisterInstance(_failureStateView);
            builder.RegisterInstance(_victoryStateView);
            
            builder.RegisterInstance(_towerShopView);
            builder.RegisterInstance(_towerOperationView);
            builder.RegisterInstance(_towerPlacementView);
        }
    }
}