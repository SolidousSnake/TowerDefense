using _Project.Code.Gameplay.Enemy;
using _Project.Code.Gameplay.Repository;
using UnityEngine;

namespace _Project.Code.Gameplay.Tower
{
    public class TargetSelector
    {
        private readonly EnemyRepository _enemyRepository;
        private readonly Transform _transform;
        private float _range;

        public TargetSelector(EnemyRepository enemyRepository, Transform transform)
        {
            _enemyRepository = enemyRepository;
            _transform = transform;
        }

        public void SetRange(float range) => _range = range;

        public EnemyFacade GetNearestEnemy()
        {
            foreach (var enemy in _enemyRepository.List)
            {
                if (enemy is null || !enemy.IsAlive)
                    continue;

                float distanceToEnemy = Vector3.Distance(_transform.position, enemy.transform.position);
                if (distanceToEnemy <= _range)
                    return enemy;
            }

            return null;
        }
    }
}