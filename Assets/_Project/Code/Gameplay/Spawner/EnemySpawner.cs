using System.Threading;
using _Project.Code.Config;
using _Project.Code.Core.AssetManagement;
using _Project.Code.Gameplay.Enemy;
using _Project.Code.Gameplay.Point;
using _Project.Code.Services.Wallet;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace _Project.Code.Gameplay.Spawner
{
    public class EnemySpawner
    {
        private readonly WaveConfig[] _waves;
        private readonly WayPoint[] _wayPoints;
        private readonly SpawnPoint _spawnPoint;
        private readonly WalletService _walletService;

        private readonly ReactiveProperty<int> _waveIndex;

        private CancellationTokenSource _cts;
        private int _enemyCount;
        private bool _isPaused;

        public EnemySpawner(WayPoint[] wayPoints
            , SpawnPoint spawnPoint
            , ConfigProvider configProvider
            , WalletService walletService)
        {
            _waves = configProvider.GetSingle<LevelConfig>().Waves;
            _wayPoints = wayPoints;
            _spawnPoint = spawnPoint;
            _walletService = walletService;

            _waveIndex = new ReactiveProperty<int>(0);
            WaveIndex = _waveIndex.ToReadOnlyReactiveProperty();
        }

        public IReadOnlyReactiveProperty<int> WaveIndex { get; }

        public void StartSpawning()
        {
            _cts = new CancellationTokenSource();
            SpawnAsync().Forget();
        }

        public void StopSpawning() => _cts.Cancel();
        public void Pause() => _isPaused = true;
        public void Resume() => _isPaused = false;

        public void ResumeOrStart()
        {
            if (_isPaused)
                Resume();
            else
                StartSpawning();
        }


        private async UniTask SpawnAsync()
        {
            var wave = _waves[_waveIndex.Value];
            _enemyCount = wave.Count;
            
            for (int i = 0; i < wave.Count; i++)
            {
                SpawnEnemy(wave);
                await UniTask.Delay((int)(wave.SpawnDelay * 1000), cancellationToken: _cts.Token);
            }
        }

        private void SpawnEnemy(WaveConfig wave)
        {
            var instance = Object.Instantiate(wave.Enemy.Prefab, _spawnPoint.Position, Quaternion.identity);

            var enemy = instance.GetComponent<EnemyFacade>();
            enemy.Initialize(wave.Enemy, _wayPoints);

            enemy.Health.Points.Where(hp => hp <= 0)
                .Subscribe(_ => HandleEnemyDeath(wave.Enemy.KillReward)).AddTo(enemy);
        }

        private void HandleEnemyDeath(int reward)
        {
            _enemyCount--;
            if (_enemyCount > 0)
                return;

            _waveIndex.Value++;
            _walletService.AddGameplayCoins(reward);
            SpawnAsync().Forget();
        }
    }
}