using System;
using UniRx;

namespace _Project.Code.Gameplay.Unit
{
    public class Health
    {
        public Health(float hp)
        {
            Points = new ReactiveProperty<float>(hp);
        }

        public ReactiveProperty<float> Points { get; }

        public void ApplyDamage(float damage)
        {
            if (damage < 0)
                throw new Exception($"Damage must be positive. Received: {damage}");

            Points.Value -= damage;
        }
    }
}