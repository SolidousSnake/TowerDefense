﻿using System;
using System.Collections.Generic;
using System.Threading;
using _Project.Code.Core.Fsm;
using _Project.Code.Data.Config;
using _Project.Code.Gameplay.Point;
using _Project.Code.Gameplay.States;
using _Project.Code.Services.Wallet;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Code.Gameplay.Spawner
{
    public class EnemySpawner : IDisposable
    {
        private readonly IObjectResolver _objectResolver;
        private readonly WayPoint[] _wayPoints;
        private readonly SpawnPoint _spawnPoint;
        private readonly WalletService _walletService;

        private readonly GameplayStateMachine _fsm;
        private readonly ReactiveProperty<int> _waveIndex;
        private IReadOnlyList<WaveConfig> _waves;

        private CancellationTokenSource _cts;
        private int _enemyCount;
        private bool _isPaused;

        public EnemySpawner(
            IObjectResolver objectResolver
            , WayPoint[] wayPoints
            , SpawnPoint spawnPoint
            , WalletService walletService
            , GameplayStateMachine fsm)
        {
            _objectResolver = objectResolver;
            _wayPoints = wayPoints;
            _spawnPoint = spawnPoint;
            _walletService = walletService;
            _fsm = fsm;

            _waveIndex = new ReactiveProperty<int>(0);
            WaveIndex = _waveIndex.ToReadOnlyReactiveProperty();
        }

        public IReadOnlyReactiveProperty<int> WaveIndex { get; }

        public void Initialize(IReadOnlyList<WaveConfig> waves) => _waves = waves;

        private void StartSpawning()
        {
            _cts = new CancellationTokenSource();
            SpawnAsync().Forget();
        }

        private void StopSpawning() => _cts?.Cancel();
        public void Pause() => _isPaused = true;
        private void Resume() => _isPaused = false;

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

            await UniTask.Delay((int)(wave.TimeBetweenWaves * 1000), cancellationToken: _cts.Token);

            _waveIndex.Value++;

            for (int i = 0; i < wave.Count; i++)
            {
                SpawnEnemy(wave);
                await UniTask.Delay((int)(wave.SpawnDelay * 1000), cancellationToken: _cts.Token);
            }
        }

        private void SpawnEnemy(WaveConfig wave)
        {
            var enemy = _objectResolver.Instantiate(wave.Enemy.Prefab, _spawnPoint.Position, Quaternion.identity);

            enemy.Initialize(wave.Enemy, _wayPoints);
            enemy.Health.Points.Where(hp => hp <= 0)
                .Subscribe(_ => HandleEnemyDeath(wave.Enemy.KillReward)).AddTo(enemy);
        }

        private void HandleEnemyDeath(int reward)
        {
            _walletService.AddGameplayCoins(reward);
            _enemyCount--;
            
            if (_enemyCount > 0)
                return;

            if (_waveIndex.Value >= _waves.Count)
            {
                _fsm.Enter<VictoryState>();
                return;
            }
            
            SpawnAsync().Forget();
        }

        public void Dispose() => StopSpawning();
    }
}