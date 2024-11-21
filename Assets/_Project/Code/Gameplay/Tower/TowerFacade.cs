using _Project.Code.Config;
using _Project.Code.Gameplay.Enemy;
using _Project.Code.Gameplay.Sfx;
using _Project.Code.Gameplay.Unit.Rotator;
using _Project.Code.Gameplay.Weapon.Attack;
using _Project.Code.Gameplay.Weapon.FireMode;
using _Project.Code.Utils;
using UnityEngine;

namespace _Project.Code.Gameplay.Tower
{
    public class TowerFacade : MonoBehaviour
    {
        [SerializeField] private Transform _rotationPart;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private AudioSource _fireAudioSource;
        [SerializeField] private AudioSource _tailAudioSource;

        private readonly Collider[] _targets = new Collider[Constants.DefaultCapacity];
        private TowerConfig _config;
        private bool _initialized = false;

        private IFireMode _fireMode;
        private IWeaponAttack _weaponAttack;
        private TowerRotator _rotator;
        private TowerSfx _sfx;

        private Transform _currentTarget;

        public void Initialize(TowerConfig config)
        {
            _config = config;
            _rotator = new TowerRotator(transform, _rotationPart);
            _sfx = new TowerSfx(_fireAudioSource, _tailAudioSource, config.SfxConfig);

            InstallWeaponAttack(config);
            InstallFireMode(config);
            Subscribe();

            _fireMode.SetFireDelay(config.FireRate);
            _rotator.SetRotationSpeed(config.RotationSpeed);

            _initialized = true;
        }

        private void Subscribe()
        {
            _fireMode.Fired += _sfx.PlayFire;
            _fireMode.Stopped += _sfx.StopFire;
        }

        private void OnDestroy()
        {
            _fireMode.Fired -= _sfx.PlayFire;
            _fireMode.Stopped -= _sfx.StopFire;
        }

        private void FixedUpdate()
        {
            if (!_initialized)
                return;

            if (EnemySighted() == false)
            {
                _fireMode.StopFire();
                return;
            }

            _rotator.Rotate(_currentTarget.position);

            if (_rotator.IsAlignedWith(_currentTarget.position))
                _fireMode.Fire();
        }

        private bool EnemySighted()
        {
            var targets =
                Physics.OverlapSphereNonAlloc(transform.position, _config.Range, _targets, _config.EnemyLayer);

            for (int i = 0; i < targets; i++)
            {
                if (_targets[i].TryGetComponent(out EnemyFacade enemy) && enemy.IsAlive)
                {
                    _currentTarget = enemy.transform;
                    return true;
                }
            }

            _currentTarget = null;
            return false;
        }

        private void InstallWeaponAttack(TowerConfig config)
        {
            _weaponAttack = config.AttackType switch
            {
                WeaponAttackType.Ray => new RayAttack(_shootPoint, config.EnemyLayer),
                WeaponAttackType.Projectile => new ProjectileAttack(),
                _ => throw new System.ArgumentException("Invalid Attack type")
            };

            _weaponAttack.SetDamage(config.Damage);
            _weaponAttack.SetRange(config.Range);
        }

        private void InstallFireMode(TowerConfig config)
        {
            _fireMode = config.FireModeType switch
            {
                FireModeType.Full => new FullAutoFire(_weaponAttack),
                FireModeType.Semi => new SemiAutoFire(_weaponAttack),
                _ => throw new System.ArgumentException("Invalid Attack type")
            };
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _config.Range);
        }
    }
}