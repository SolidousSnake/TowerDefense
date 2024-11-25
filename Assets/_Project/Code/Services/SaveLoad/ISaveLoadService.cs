using _Project.Code.Data.PersistentProgress;

namespace _Project.Code.Services.SaveLoad
{
    public interface ISaveLoadService
    {
        public void Save(PlayerProgress playerProgress);
        public PlayerProgress Load();
        public void Reset();
    }
}