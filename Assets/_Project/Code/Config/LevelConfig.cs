﻿using System.Collections.Generic;
using Alchemy.Inspector;
using UnityEngine;


namespace _Project.Code.Config
{
    [CreateAssetMenu(menuName = "_Project/Config/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public int InitialMoneyCount { get; private set; }
        [field: SerializeField] public LayerMask PlacementLayer { get; private set; }
        [field: SerializeField] public Vector2Int GridSize { get; private set; }
        [field: Space] [field: SerializeField] [field: InlineEditor] public List<TowerConfig> TowersList { get; private set; }
        [field: Space] [field: SerializeField] [field: InlineEditor] public List<WaveConfig> Waves { get; private set; }
    }
}