﻿using System;
using System.Collections.Generic;
using _Project.Code.Config;
using _Project.Code.Gameplay.Point;
using _Project.Code.Gameplay.Unit;
using _Project.Code.Gameplay.Unit.Movement;
using _Project.Code.Gameplay.Unit.Rotator;
using Cysharp.Threading.Tasks;
using _Project.Code.Utils;
using UnityEngine;
using UniRx;

namespace _Project.Code.Gameplay.Enemy
{
    public class EnemyFacade : MonoBehaviour
    {
        [SerializeField] private GameObject _originalPrefab;
        [SerializeField] private GameObject _destroyedPrefab;
        [SerializeField] private float _deathTime;
        [SerializeField] private float _rotationSpeed;

        private WayPointMovement _wayPointMovement;
        private EnemyRotator _enemyRotator;
        private PlayerHealth _playerHealth;
        
        public Health Health { get; private set; }

        public bool IsAlive => Health.Points.Value > 0;
        
        public void Initialize(EnemyConfig config, IReadOnlyList<WayPoint> wayPoints, PlayerHealth playerHealth)
        {
            _originalPrefab.Show();
            _destroyedPrefab.Hide();

            _playerHealth = playerHealth;
            
            Health = new Health(config.MaxHealth);
            _enemyRotator = new EnemyRotator(transform, _rotationSpeed);
            _wayPointMovement = new WayPointMovement(transform, wayPoints);

            _wayPointMovement.SetSpeed(config.MovementSpeed);
            
            Health.Points.Where(hp => hp <= 0).Subscribe(_ => ApplyDeath().Forget()).AddTo(this);
            _wayPointMovement.ReachedFinalWaypoint += OnReachedLastWaypoint;
        }

        private void OnReachedLastWaypoint()
        {
            _playerHealth.ApplyDamage(1f);
            Destroy(gameObject);
        }

        private async UniTaskVoid ApplyDeath()
        {
            _originalPrefab.Hide();
            _destroyedPrefab.Show();

            await UniTask.Delay((int)(_deathTime * 1000));
            Destroy(gameObject);
        }

        private void Update()
        {
            var wayPoint = _wayPointMovement.GetCurrentWayPoint();
            
            if(wayPoint is null || IsAlive is false)
                return;
            
            _wayPointMovement.MoveTowardsWaypoint();
            _enemyRotator.RotateTowards(wayPoint.transform.position);
        }

        private void OnDestroy() => _wayPointMovement.ReachedFinalWaypoint -= OnReachedLastWaypoint;
    }
}