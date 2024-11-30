using _Project.Code.Gameplay.Enemy;
using UnityEngine;

namespace _Project.Code.Core.Factory
{
    public class EnemyFactory
    {
        public EnemyFacade Create(EnemyFacade prefab, Vector3 spawnPosition)
        {
            var instance = Object.Instantiate(prefab, spawnPosition, Quaternion.identity);
            var enemy = instance.GetComponent<EnemyFacade>();
            return enemy;
        }
    }
}