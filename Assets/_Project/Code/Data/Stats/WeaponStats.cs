using UnityEngine;

namespace _Project.Code.Data.Stats
{
    [System.Serializable]
    public class WeaponStats
    {
        [field: SerializeField] public float[] Damage { get; private set; }
        [field: SerializeField] public float[] Range { get; private set; }
        [field: SerializeField] public float[] FireRate { get; private set; }
        [field: SerializeField] public int[] Penetration { get; private set; }
    }
}