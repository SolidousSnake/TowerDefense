using UnityEngine;

namespace _Project.Code.Data.Config
{
    [System.Serializable]
    public class WaveConfig
    {
        [field: SerializeField] 
        public EnemyConfig Enemy { get; private set; }

        [field: SerializeField]
        public int Count { get; private set; }
        
        [field: SerializeField]
        public float SpawnDelay { get; private set; }

        [field: SerializeField]
        public float TimeBetweenWaves { get; private set; }
    }
}