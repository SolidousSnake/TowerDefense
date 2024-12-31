using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Data.Config
{
    [CreateAssetMenu(menuName = "_Project/Config/Gameplay/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public int InitialMoneyCount { get; private set; }
        [field: SerializeField] public int VictoryRewardPerHealth { get; private set; }
        [field: SerializeField] public int MaxPlayerHealth { get; private set; }
        [field: SerializeField] public float FirstSpawnDelay { get; private set; }
        [field: SerializeField] public LayerMask PlacementLayer { get; private set; }
        [field: Space] [field: SerializeField] public List<TowerConfig> TowersList { get; private set; }
        [field: Space] [field: SerializeField] public WaveConfig[] Waves { get; private set; }
    }
}