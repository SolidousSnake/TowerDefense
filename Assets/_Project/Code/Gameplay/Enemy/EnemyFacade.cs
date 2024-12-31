using System.Collections.Generic;
using _Project.Code.Data.Config;
using _Project.Code.Gameplay.Point;
using _Project.Code.Gameplay.Repository;
using _Project.Code.Gameplay.Unit;
using _Project.Code.Gameplay.Unit.Movement;
using _Project.Code.Gameplay.Unit.Rotator;
using Cysharp.Threading.Tasks;
using _Project.Code.Utils;
using UnityEngine;
using UniRx;
using VContainer;

namespace _Project.Code.Gameplay.Enemy
{
    public class EnemyFacade : MonoBehaviour
    {
        [SerializeField] private Transform _rotationPart;
        [SerializeField] private GameObject _originalPrefab;
        [SerializeField] private GameObject _destroyedPrefab;
        [SerializeField] private float _deathTime;
        [SerializeField] private float _rotationSpeed;

        [Inject] private EnemyRepository _repository;
        [Inject] private PlayerHealth _playerHealth;

        private WayPointMovement _wayPointMovement;
        private EnemyRotator _enemyRotator;

        public Health Health { get; private set; }

        public bool IsAlive => Health.Points.Value > 0;

        public void Initialize(EnemyConfig config
            , IReadOnlyList<WayPoint> wayPoints)
        {
            _originalPrefab.Show();
            _destroyedPrefab.Hide();

            Health = new Health(config.MaxHealth);
            _enemyRotator = new EnemyRotator(_rotationPart, _rotationSpeed);
            _wayPointMovement = new WayPointMovement(transform, wayPoints);

            _wayPointMovement.SetSpeed(config.MovementSpeed);

            _repository.Add(this);
            Health.Points.Where(hp => hp <= 0).Subscribe(_ => ApplyDeath().Forget()).AddTo(this);
            _wayPointMovement.ReachedFinalWaypoint += OnReachedLastWaypoint;
        }

        private void OnReachedLastWaypoint()
        {
            _playerHealth.ApplyDamage(1f);
            Destroy(gameObject);
        }

        private bool _died;

        private async UniTaskVoid ApplyDeath()
        {
            if (_died)
                return;

            _died = true;
            _originalPrefab.Hide();
            _destroyedPrefab.Show();
            _repository.Remove(this);

            await UniTask.Delay((int)(_deathTime * 1000));
            Destroy(gameObject);
        }

        private void Update()
        {
            var wayPoint = _wayPointMovement.GetCurrentWayPoint();

            if (wayPoint is null || IsAlive is false)
                return;

            _wayPointMovement.MoveTowardsWaypoint();
            _enemyRotator.RotateTowards(wayPoint.transform.position);
        }

        private void OnDestroy() => _wayPointMovement.ReachedFinalWaypoint -= OnReachedLastWaypoint;
    }
}