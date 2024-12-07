using _Project.Code.Config;
using _Project.Code.Gameplay.Repository;
using _Project.Code.Gameplay.Unit.Rotator;
using _Project.Code.Gameplay.Weapon;
using Alchemy.Inspector;
using UnityEngine;

namespace _Project.Code.Gameplay.Tower
{
    public class TowerFacade : MonoBehaviour
    {
        [SerializeField] private Transform _rotationPart;
        [SerializeField] private WeaponFacade _weaponFacade;

        private bool _initialized = false;
        private TowerConfig _config;
        private TowerRotator _rotator;
        private TargetSelector _targetSelector;

        [SerializeField] [ReadOnly] private Transform _currentTarget;

        public string Name => _config.Name;
        public int UpgradeCost => _config.Price;
        public int SellReward => _config.Price / 2;

        public Vector2Int GridPosition { get; private set; }
        
        public void Initialize(TowerConfig config, EnemyRepository enemyRepository, Vector2Int gridPosition)
        {
            GridPosition = gridPosition;
            _config = config;
            _rotator = new TowerRotator(transform, _rotationPart);
            _targetSelector = new TargetSelector(enemyRepository, transform);

            _weaponFacade.Initialize(config);
            _targetSelector.SetRange(config.Range);
            _initialized = true;
        }

        private void FixedUpdate()
        {
            if (!_initialized)
                return;

            var enemy = _targetSelector.GetNearestEnemy();

            if (enemy is null)
            {
                _weaponFacade.StopFire();
                return;
            }

            _currentTarget = enemy.transform;
            _rotator.Rotate(_currentTarget.position);
            _weaponFacade.Fire();
        }

        private void OnMouseDown()
        {
            
        }

        private void OnDrawGizmosSelected()
        {
            if (_config is null)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _config.Range);
        }
    }
}