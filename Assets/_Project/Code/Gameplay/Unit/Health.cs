﻿using System;
using UniRx;

namespace _Project.Code.Gameplay.Unit
{
    public class Health
    {
        private readonly ReactiveProperty<float> _points;
        
        public Health(float hp = 0)
        {
            MaxHealth = hp;
            _points = new ReactiveProperty<float>(hp);
            Points = _points.ToReadOnlyReactiveProperty();
        }
        public float MaxHealth { get; private set; }
        
        public IReadOnlyReactiveProperty<float> Points { get; }

        public void ApplyDamage(float damage)
        {
            if (damage < 0)
                throw new Exception($"Damage must be positive. Received: {damage}");

            _points.Value = Math.Max(_points.Value - damage, 0);
        }

        public void ApplyHealth(float health)
        {
            if (health < 0)
                throw new Exception($"Health must be positive. Received: {health}");
          
            _points.Value = Math.Min(_points.Value + health, MaxHealth);
        }

        protected void SetMaxHealth(float value)
        {
            MaxHealth = value;
            _points.Value = Math.Min(_points.Value, value);
        }

        protected void SetHealth(float value) => _points.Value = value;
    }
}