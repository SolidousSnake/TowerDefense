using System.Collections.Generic;
using _Project.Code.Data.Enum;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace _Project.Code.Data.Config
{
    [CreateAssetMenu(menuName = "_Project/Config/Menu/ShopConfig")]
    public class MenuShopConfig : ScriptableObject
    {
        [SerializeField] private SerializedDictionary<TowerType, TowerUpgradePriceConfig> _items;
        public Dictionary<TowerType, TowerUpgradePriceConfig> Items => _items;
    }
}