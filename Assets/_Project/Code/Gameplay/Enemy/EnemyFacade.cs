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
        [SerializeField] private int _deathTime;
        [SerializeField] private float _rotationSpeed;

        private WayPointMovement _wayPointMovement;
        private EnemyRotator _enemyRotator;
        
        public Health Health { get; private set; }

        public bool IsAlive => Health.Points.Value > 0;

        public void Initialize(EnemyConfig config, WayPoint[] wayPoints)
        {
            _originalPrefab.Show();
            _destroyedPrefab.Hide();

            Health = new Health(config.MaxHealth);

            _enemyRotator = new EnemyRotator(transform, _rotationSpeed);
            _wayPointMovement = new WayPointMovement(transform, wayPoints);

            _wayPointMovement.SetSpeed(config.MovementSpeed);
            
            Health.Points.Where(hp => hp <= 0).Subscribe(_ => ApplyDeath().Forget()).AddTo(this);
        }

        private async UniTaskVoid ApplyDeath()
        {
            _originalPrefab.Hide();
            _destroyedPrefab.Show();

            await UniTask.Delay(_deathTime);
            Destroy(gameObject);
        }

        private void Update()
        {
            _wayPointMovement.MoveTowardsWaypoint();
            _enemyRotator.RotateTowards(_wayPointMovement.GetCurrentWayPoint().transform.position);
        }
    }
}