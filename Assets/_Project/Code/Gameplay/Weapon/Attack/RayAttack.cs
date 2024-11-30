using System;
using _Project.Code.Gameplay.Enemy;
using UnityEngine;

namespace _Project.Code.Gameplay.Weapon.Attack
{
    public class RayAttack : IWeaponAttack
    {
        private readonly Transform _shootPoint;
        private readonly LayerMask _enemyLayer;

        private float _damage;
        private float _range;

        public RayAttack(Transform shootPoint, LayerMask enemyLayer)
        {
            _shootPoint = shootPoint;
            _enemyLayer = enemyLayer;
        }

        public void Attack()
        {
            var ray = new Ray(_shootPoint.position, _shootPoint.forward);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, _range, _enemyLayer))
                if (hitInfo.collider.TryGetComponent(out EnemyFacade enemy))
                    enemy.Health.ApplyDamage(_damage);
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
    }
}