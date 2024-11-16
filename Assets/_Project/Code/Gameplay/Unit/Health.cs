using System;
using UniRx;

namespace _Project.Code.Gameplay.Unit
{
    public class Health
    {
        private readonly ReactiveProperty<float> _points;

        public Health(float hp)
        {
            _points = new ReactiveProperty<float>(hp);
            Points = _points.ToReadOnlyReactiveProperty();
        }

        public  IReadOnlyReactiveProperty<float> Points { get; }

        public void ApplyDamage(float damage)
        {
            if (damage < 0)
                throw new Exception($"Damage must be positive. Received: {damage}");

            _points.Value -= damage;
        }
    }
}