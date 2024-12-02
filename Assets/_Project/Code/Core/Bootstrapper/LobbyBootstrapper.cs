using System;
using _Project.Code.Services.SaveLoad;
using _Project.Code.Services.Sound;
using _Project.Code.Utils;
using VContainer;
using VContainer.Unity;

namespace _Project.Code.Core.Bootstrapper
{
    public class LobbyBootstrapper : IInitializable, IDisposable
    {
        [Inject] private readonly SoundService _soundService;
        [Inject] private readonly ISaveLoadService _saveLoadService;
        
        public void Initialize()
        {
            var soundData = _saveLoadService.Load().SoundData;

            if (soundData.MusicVolume == 0)
                soundData.MusicVolume = Constants.Audio.MinSliderValue;

            if (soundData.SfxVolume == 0)
                soundData.SfxVolume = Constants.Audio.MinSliderValue;

            
            _soundService.SetMusicVolume(soundData.MusicVolume);
            _soundService.SetSfxVolume(soundData.SfxVolume);

        }

        public void Dispose()
        {
        }
    }
}