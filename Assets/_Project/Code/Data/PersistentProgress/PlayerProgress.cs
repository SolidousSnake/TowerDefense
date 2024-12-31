namespace _Project.Code.Data.PersistentProgress
{
    [System.Serializable]
    public class PlayerProgress
    {
        public WalletData WalletData;
        public SoundData SoundData;
        public SettingData SettingData;
        public UpgradeData UpgradeData;
        
        public PlayerProgress()
        {
            WalletData ??= new WalletData();
            SoundData ??= new SoundData();
            SettingData ??= new SettingData();
            UpgradeData ??= new UpgradeData();
        }
    }
}