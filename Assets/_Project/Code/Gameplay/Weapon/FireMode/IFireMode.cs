using System;

namespace _Project.Code.Gameplay.Weapon.FireMode
{
    public interface IFireMode
    {
        public event Action Fired;
        public event Action Stopped;

        public void SetFireDelay(float delay);
        public void Fire();
        public void StopFire();
    }
}