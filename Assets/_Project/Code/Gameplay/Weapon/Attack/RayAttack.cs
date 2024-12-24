using System;
using _Project.Code.Gameplay.Enemy;
using _Project.Code.Utils;
using UnityEngine;

namespace _Project.Code.Gameplay.Weapon.Attack
{
    public class RayAttack : IWeaponAttack
    {
        private readonly Transform _shootPoint;
        private readonly LayerMask _enemyLayer;
        private readonly RaycastHit[] _hitBuffer = new RaycastHit[Constants.DefaultCapacity];
        
        private float _damage;
        private float _range;
        private int _penetrationDepth;

        public RayAttack(Transform shootPoint, LayerMask enemyLayer)
        {
            _shootPoint = shootPoint;
            _enemyLayer = enemyLayer;
        }
        
        public void Attack()
        {
            var ray = new Ray(_shootPoint.position, _shootPoint.forward);
            int hitCount = Physics.RaycastNonAlloc(ray, _hitBuffer, _range, _enemyLayer);

            if (hitCount == 0)
                return;

            int enemiesHit = 0;

            for (int i = 0; i < hitCount; i++)
            {
                var hit = _hitBuffer[i];

                if (hit.collider.TryGetComponent(out EnemyFacade enemy))
                {
                    enemy.Health.ApplyDamage(_damage);
                    enemiesHit++;

                    if (enemiesHit >= _penetrationDepth)
                        break;
                }
            }
        }
        
        public void SetDamage(float damage)
        {
            if (damage < 0)
                throw new Exception($"Damage must be positive. Received: {damage}");
            _damage = damage;
        }

        public void SetRange(float range)
        {
            if (range < 0)
                throw new Exception($"Range must be positive. Received: {range}");
            _range = range;
        }

        public void SetPenetrationDepth(int value)
        {
            if (value < 1)
                throw new Exception($"Penetration must be positive. Received: {value}");
            _penetrationDepth = value;
        }
    }
}