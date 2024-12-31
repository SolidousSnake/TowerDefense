using _Project.Code.Utils;

namespace _Project.Code.Data.PersistentProgress
{
    [System.Serializable]
    public class SoundData
    {
        public float SfxVolume { get; set; }
        public float MusicVolume { get; set; }

        public SoundData()
        {
            SfxVolume = MusicVolume = Constants.Audio.DefaultValue;
        }
    }
}