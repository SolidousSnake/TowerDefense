namespace _Project.Code.Data.PersistentProgress
{
    [System.Serializable]
    public class UpgradeData
    {
        public int PickUpCount { get; set; }
        public int SuvCount { get; set; }
        public int TankCount { get; set; }

        public TowerUpgradeData AssaultUpgradeData { get; set; }
        public TowerUpgradeData DemoManUpgradeData { get; set; }
        public TowerUpgradeData SniperUpgradeData { get; set; }

        public UpgradeData()
        {
            AssaultUpgradeData ??= new TowerUpgradeData();
            DemoManUpgradeData ??= new TowerUpgradeData();
            SniperUpgradeData ??= new TowerUpgradeData();
        }
    }
}