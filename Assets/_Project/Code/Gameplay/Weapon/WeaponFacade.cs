using _Project.Code.Config;
using _Project.Code.Gameplay.Sfx;
using _Project.Code.Gameplay.Weapon.Attack;
using _Project.Code.Gameplay.Weapon.FireMode;
using UnityEngine;

namespace _Project.Code.Gameplay.Weapon
{
    public class WeaponFacade : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private AudioSource _fireAudioSource;
        [SerializeField] private AudioSource _tailAudioSource;

        private IWeaponAttack _weaponAttack;
        private IFireMode _fireMode;
        private TowerSfx _sfx;
        
        public void Initialize(TowerConfig config)
        {
            _sfx = new TowerSfx(_fireAudioSource, _tailAudioSource, config.SfxConfig);

            InstallWeaponAttack(config);
            InstallFireMode(config);
            
            _fireMode.OnFire += _sfx.PlayFire;
            _fireMode.OnStop += _sfx.StopFire;
        }

        public void Fire() => _fireMode.Fire();

        public void StopFire() => _fireMode.StopFire();
        
        private void InstallFireMode(TowerConfig config)
        {
            _fireMode = config.FireModeType switch
            {
                FireModeType.Full => new FullAutoFire(_weaponAttack, config.FireRate),
                FireModeType.Semi => new SemiAutoFire(_weaponAttack, config.FireRate),
                _ => throw new System.ArgumentException("Invalid Attack type")
            };
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

        private void OnDestroy()
        {
            _fireMode.OnFire -= _sfx.PlayFire;
            _fireMode.OnStop -= _sfx.StopFire;
        }
    }
}