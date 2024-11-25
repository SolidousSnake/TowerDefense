using System;

namespace _Project.Code.Gameplay.Weapon.FireMode
{
    public interface IFireMode
    {
        public event Action OnFire;
        public event Action OnStop;

        public void Fire();
        public void StopFire();
    }
}