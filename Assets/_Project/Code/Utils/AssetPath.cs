namespace _Project.Code.Utils
{
    public static class AssetPath
    {
        public static class Config
        {
            private const string MainFolder = "Config/";
            public const string LevelConfig = MainFolder + "LevelConfig";
            public const string TowerShopColors = MainFolder + "UI/TowerShopColors";
            public const string MenuShopColors = MainFolder + "UI/MenuShopColors";
            public const string TankConfig = MainFolder + "TankConfig";
            public const string SuvConfig = MainFolder + "SuvConfig";

            public const string HubCameraRotation = MainFolder + "CameraLobbyRotation/Hub";
            public const string ShopCameraRotation = MainFolder + "CameraLobbyRotation/Shop";
            public const string SettingsCameraRotation = MainFolder + "CameraLobbyRotation/Settings";
            public const string SelectLevelCameraRotation = MainFolder + "CameraLobbyRotation/SelectLevel";
            
            public const string Towers = MainFolder + "Tower";
            public const string MenuShop = MainFolder + "MenuShopConfig";
        }
        
        public static class Prefab
        {
            private const string MainFolder = "Prefab/";
            public const string TowerShopItem = MainFolder + "UI/Tower Shop Item Button";
            public const string Star = MainFolder + "UI/Star";
        }
    }
}