using UnityEngine;

namespace _Project.Code.Config
{
    [CreateAssetMenu(menuName = "_Project/UI/TowerShopColors")]
    public class TowerShopColors : ScriptableObject
    {
        [field: SerializeField] public Color AffordablePrice { get; private set; }
        [field: SerializeField] public Color UnaffordablePrice { get; private set; }
    }
}