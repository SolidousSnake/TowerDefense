using System;
using _Project.Code.Core.Timers;
using _Project.Code.Gameplay.Weapon.Attack;

namespace _Project.Code.Gameplay.Weapon.FireMode
{
    public class FullAutoFire : IFireMode
    {
        private readonly IWeaponAttack _weaponAttack;
        private readonly CountdownTimer _countdownTimer;

        private bool _allowShooting;

        public FullAutoFire(IWeaponAttack weaponAttack, float fireDelay)
        {
            _weaponAttack = weaponAttack;
            _allowShooting = true;

            _countdownTimer = new CountdownTimer(fireDelay);
            _countdownTimer.OnFinish += () => _allowShooting = true;
        }

        public event Action OnFire;
        public event Action OnStop;

        public void Fire()
        {
            if (!_allowShooting)
                return;
            
            OnFire?.Invoke();
            _weaponAttack.Attack();
            _countdownTimer.Start();
            _allowShooting = false;
        }

        public void StopFire() => OnStop?.Invoke();
    }
}