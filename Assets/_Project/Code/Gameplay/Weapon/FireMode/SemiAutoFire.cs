using System;
using _Project.Code.Gameplay.Weapon.Attack;
using Cysharp.Threading.Tasks;

namespace _Project.Code.Gameplay.Weapon.FireMode
{
    public class SemiAutoFire : IFireMode
    {
        private readonly IWeaponAttack _weaponAttack;
        private float _fireDelay;
        private bool _allowShooting;

        public SemiAutoFire(IWeaponAttack weaponAttack)
        {
            _weaponAttack = weaponAttack;
            _allowShooting = true;
        }

        public event Action Fired;
        public event Action Stopped;

        public void SetFireDelay(float fireDelay) => _fireDelay = fireDelay;

        public void Fire()
        {
            if (!_allowShooting)
                return;

            _weaponAttack.Attack();
            Fired?.Invoke();
            _allowShooting = false;
            HandleFireDelay().Forget();
        }

        public void StopFire()
        {
            
        }

        private async UniTask HandleFireDelay()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_fireDelay));
            Stopped?.Invoke();
            _allowShooting = true;
        }
    }
}