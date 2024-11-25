using _Project.Code.Config;
using _Project.Code.Gameplay.Repository;
using _Project.Code.Gameplay.Unit.Rotator;
using _Project.Code.Gameplay.Weapon;
using _Project.Code.Utils;
using Alchemy.Inspector;
using UnityEngine;

namespace _Project.Code.Gameplay.Tower
{
    public class TowerFacade : MonoBehaviour
    {
        [SerializeField] private Transform _rotationPart;
        [SerializeField] private WeaponFacade _weaponFacade;
        
        private readonly Collider[] _targets = new Collider[Constants.DefaultCapacity];
        private bool _initialized = false;
        private TowerConfig _config;

        private TowerRotator _rotator;
        private TargetSelector _targetSelector;

        [SerializeField] [ReadOnly] private Transform _currentTarget;

        public void Initialize(TowerConfig config, EnemyRepository enemyRepository)
        {
            _config = config;
            _rotator = new TowerRotator(transform, _rotationPart);
            _targetSelector = new TargetSelector(enemyRepository, transform);
            
            _weaponFacade.Initialize(config);
            
            _targetSelector.SetRange(config.Range);
            _rotator.SetRotationSpeed(config.RotationSpeed);
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

            if (_rotator.IsAlignedWith(_currentTarget.position)) 
                _weaponFacade.Fire();
        }

        private void OnDrawGizmosSelected()
        {
            if(_config is null)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _config.Range);
        }
    }
}