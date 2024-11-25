namespace _Project.Code.Data.PersistentProgress
{
    [System.Serializable]
    public class PlayerProgress
    {
        public WalletData WalletData;
        public SoundData SoundData;

        public PlayerProgress()
        {
            WalletData ??= new WalletData();
            SoundData ??= new SoundData();
        }
    }
}