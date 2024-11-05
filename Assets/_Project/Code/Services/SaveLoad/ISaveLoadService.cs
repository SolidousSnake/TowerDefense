using _Project.Code.Data;

namespace _Project.Code.Services.SaveLoad
{
    public interface ISaveLoadService
    {
        public void Save(WalletData playerProgress);
        public WalletData Load();
        public void Reset();
    }
}