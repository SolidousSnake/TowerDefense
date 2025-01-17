﻿using _Project.Code.Data.Stats;
using _Project.Code.Gameplay.Tower;
using _Project.Code.Gameplay.Weapon.Attack;
using _Project.Code.Gameplay.Weapon.FireMode;
using NaughtyAttributes;
using UnityEngine;

namespace _Project.Code.Data.Config
{
    [CreateAssetMenu(menuName = "_Project/Config/Gameplay/TowerConfig")]
    public class TowerConfig : ScriptableObject
    {
        [field: BoxGroup("Config")] [field: SerializeField] public SfxConfig SfxConfig { get; private set; }
        [field: BoxGroup("Shop")] [field: SerializeField] public string Name { get; private set; }
        [field: BoxGroup("Shop")] [field: SerializeField] public int Price { get; private set; }
        [field: BoxGroup("Shop")] [field: SerializeField] public Sprite TowerIcon { get; private set; }
        [field: BoxGroup("Combat")] [field: SerializeField] public WeaponStats WeaponStats { get; private set; }
        [field: BoxGroup("Combat")] [field: SerializeField] public float Damage { get; private set; }
        [field: BoxGroup("Combat")] [field: SerializeField] public float Range { get; private set; }
        [field: BoxGroup("Combat")] [field: SerializeField] public float FireRate { get; private set; }
        [field: BoxGroup("Combat")] [field: SerializeField] public int Penetration { get; private set; }
        [field: BoxGroup("Combat")] [field: SerializeField] public WeaponAttackType AttackType { get; private set; }
        [field: BoxGroup("Combat")] [field: SerializeField] public FireModeType FireModeType { get; private set; }
        [field: BoxGroup("Combat")] [field: SerializeField] public LayerMask EnemyLayer { get; private set; }
        [field: BoxGroup("Building")] [field: SerializeField] public Vector2Int Size { get; private set; }
        [field: BoxGroup("Building")] [field: SerializeField]  public Building Prefab { get; private set;}
    }
}