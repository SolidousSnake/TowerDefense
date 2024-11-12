﻿using UnityEngine;

namespace _Project.Code.Config
{
    [CreateAssetMenu(menuName = "_Project/Config/Enemy")]
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public float MaxHealth { get; private set; }
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public int KillReward { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
    }
}