using System.Collections.Generic;
using System.Threading;
using _Project.Code.Config;
using _Project.Code.Gameplay.Enemy;
using _Project.Code.Gameplay.Point;
using _Project.Code.Gameplay.Unit;
using _Project.Code.Services.Wallet;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace _Project.Code.Gameplay.Spawner
{
    public class EnemySpawner
    {
        private readonly WayPoint[] _wayPoints;
        private readonly SpawnPoint _spawnPoint;
        private readonly WalletService _walletService;

        private readonly ReactiveProperty<int> _waveIndex;
        private IReadOnlyList<WaveConfig> _waves;

        private PlayerHealth _playerHealth;
        private CancellationTokenSource _cts;
        private int _enemyCount;
        private bool _isPaused;

        public EnemySpawner(WayPoint[] wayPoints
            , SpawnPoint spawnPoint
            , WalletService walletService)
        {
            _wayPoints = wayPoints;
            _spawnPoint = spawnPoint;
            _walletService = walletService;

            _waveIndex = new ReactiveProperty<int>(0);
            WaveIndex = _waveIndex.ToReadOnlyReactiveProperty();
        }

        public IReadOnlyReactiveProperty<int> WaveIndex { get; }

        public void Initialize(IReadOnlyList<WaveConfig> waves, PlayerHealth playerHealth)
        {
            _waves = waves;
            _playerHealth = playerHealth;
        }

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
            if(_waveIndex.Value >= _waves.Count)
                return;
            
            var wave = _waves[_waveIndex.Value];
            _enemyCount = wave.Count;

            _waveIndex.Value++;

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
            enemy.Initialize(wave.Enemy, _wayPoints, _playerHealth);

            enemy.Health.Points.Where(hp => hp <= 0)
                .Subscribe(_ => HandleEnemyDeath(wave.Enemy.KillReward)).AddTo(enemy);
        }

        private void HandleEnemyDeath(int reward)
        {
            _enemyCount--;
            if (_enemyCount > 0)
                return;

            _walletService.AddGameplayCoins(reward);
            SpawnAsync().Forget();
        }
    }
}