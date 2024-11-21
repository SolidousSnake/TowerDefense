using System;
using System.Threading;
using _Project.Code.Gameplay.Weapon.Attack;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Code.Gameplay.Weapon.FireMode
{
    public class FullAutoFire : IFireMode
    {
        private readonly IWeaponAttack _weaponAttack;
        private float _fireDelay;

        private bool _allowShooting;
        private CancellationTokenSource _cts;
        
        public FullAutoFire(IWeaponAttack weaponAttack)
        {
            _weaponAttack = weaponAttack;
            _allowShooting = true;
        }

        public event Action Fired;
        public event Action Stopped;
        
        public void SetFireDelay(float delay) => _fireDelay = delay;

        public void Fire()
        {
            _cts = new CancellationTokenSource();

            if (_allowShooting)
                ExecuteFire().Forget();
        }

        public void StopFire()
        {
            _cts?.Cancel();
            Stopped?.Invoke();
            _allowShooting = true;
        }

        private async UniTask ExecuteFire()
        {
            try
            {
                while (!_cts.Token.IsCancellationRequested)
                {
                    _weaponAttack.Attack();
                    Fired?.Invoke();
                    _allowShooting = false;
                    await UniTask.Delay(TimeSpan.FromSeconds(_fireDelay), cancellationToken: _cts.Token);
                }
            }
            catch (OperationCanceledException)
            {
                Stopped?.Invoke();
            }
            catch (Exception ex)
            {
                Debug.LogError($"An error occurred during firing: {ex.Message}");
            }
        }
    }
}