using Alchemy.Inspector;
using UnityEngine;

namespace _Project.Code.Config
{
    [System.Serializable]
    public class WaveConfig
    {
        [field: SerializeField] [field: InlineEditor]
        public EnemyConfig Enemy { get; private set; }

        [field: SerializeField]
        public int Count { get; private set; }
        
        [field: SerializeField]
        public float SpawnDelay { get; private set; }

        [field: SerializeField]
        public float TimeBetweenWaves { get; private set; }
    }
}