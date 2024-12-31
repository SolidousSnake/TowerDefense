using UnityEngine;

namespace _Project.Code.Data.Config
{
    [CreateAssetMenu(menuName = "_Project/UI/ShopColors")]
    public class ShopColors : ScriptableObject
    {
        [field: SerializeField] public Color AffordablePrice { get; private set; }
        [field: SerializeField] public Color UnaffordablePrice { get; private set; }
    }
}