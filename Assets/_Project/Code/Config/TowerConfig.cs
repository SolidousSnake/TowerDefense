using _Project.Code.Gameplay.Tower;
using Alchemy.Inspector;
using UnityEngine;

namespace _Project.Code.Config
{
    [CreateAssetMenu(menuName = "_Project/Config/LevelConfig")]

    public class TowerConfig : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
        [field: SerializeField] public Sprite TowerIcon { get; private set; }

        [field: SerializeField] [field: AssetsOnly] public Building Prefab { get; private set;}
        [field: SerializeField] public Vector2Int Size { get; private set; }
    }
}