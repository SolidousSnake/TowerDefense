using UnityEngine;

namespace _Project.Code.Data.Config
{
    [CreateAssetMenu(menuName = "_Project/Config/Menu/TowerUpgradeConfig")]
    public class TowerUpgradePriceConfig : ScriptableObject
    {
        [field: SerializeField] public int[] DamageUpgradePrices { get; private set; }
        [field: SerializeField] public int[] RangeUpgradePrices { get; private set; }
        [field: SerializeField] public int[] FireRateUpgradePrices { get; private set; }

    }
}