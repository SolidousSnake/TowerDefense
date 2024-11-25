using System;
using _Project.Code.Core.Timers;
using _Project.Code.Gameplay.Weapon.Attack;

namespace _Project.Code.Gameplay.Weapon.FireMode
{
    public class SemiAutoFire : IFireMode
    {
        private readonly IWeaponAttack _weaponAttack;
        private readonly CountdownTimer _countdownTimer;

        private bool _allowShooting;

        public SemiAutoFire(IWeaponAttack weaponAttack, float fireDelay)
        {
            _weaponAttack = weaponAttack;
            _allowShooting = true;

            _countdownTimer = new CountdownTimer(fireDelay);
            _countdownTimer.OnFinish += HandleFireDelay;
        }

        public event Action OnFire;
        public event Action OnStop;
        
        public void Fire()
        {
            if (!_allowShooting)
                return;

            _allowShooting = false;
            _countdownTimer.Start();
            _weaponAttack.Attack();
            OnFire?.Invoke();
        }

        private void HandleFireDelay()
        {
            OnStop?.Invoke();
            _allowShooting = true;
        }

        public void StopFire()
        {
            
        }
    }
}