namespace _Project.Code.Utils
{
    public static class AssetPath
    {
        public static class Config
        {
            private const string MainFolder = "Config/";
            public const string LevelConfig = MainFolder + "LevelConfig";
            public const string TowerShopColors = MainFolder + "TowerShopColors";
            public const string TankConfig = MainFolder + "TankConfig";
            public const string SuvConfig = MainFolder + "SuvConfig";
        }
        
        public static class Prefab
        {
            private const string MainFolder = "Prefab/";
            public const string CursorIndicator = MainFolder + "CoursorindicatorParent";
            public const string TowerShopItem = MainFolder + "UI/Tower Shop Item Button";
        }
    }
}