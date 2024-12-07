using _Project.Code.Utils;
using UniRx;
using UnityEngine;
using UnityEngine.Audio;
using VContainer;

namespace _Project.Code.Services.Sound
{
    public class SoundService
    {
        [Inject] private readonly AudioMixerGroup _audioMixerGroup;
        
        private readonly ReactiveProperty<float> _musicVolume = new();
        private readonly ReactiveProperty<float> _sfxVolume = new();
        private readonly CompositeDisposable _cd = new();

        public IReadOnlyReactiveProperty<float> MusicVolume => _musicVolume;
        public IReadOnlyReactiveProperty<float> SfxVolume => _sfxVolume;

        public void Initialize(float initialMusicVolume, float initialSfxVolume)
        {
            _musicVolume.Value = Mathf.Clamp(initialMusicVolume, Constants.Audio.MinSliderValue,
                Constants.Audio.MaxSliderValue);
            _sfxVolume.Value = Mathf.Clamp(initialSfxVolume, Constants.Audio.MinSliderValue,
                Constants.Audio.MaxSliderValue);

            _musicVolume.Subscribe(volume =>
                _audioMixerGroup.audioMixer.SetFloat(Constants.Audio.Music,
                    Mathf.Log10(volume) * Constants.Audio.MaxValue));
        }

        public void SetMusicVolume(float volume) =>
            _musicVolume.Value = Mathf.Clamp(volume, Constants.Audio.MinSliderValue, Constants.Audio.MaxSliderValue);

        public void SetSfxVolume(float volume) =>
            _sfxVolume.Value = Mathf.Clamp(volume, Constants.Audio.MinSliderValue, Constants.Audio.MaxSliderValue);

        public void Dispose() => _cd.Dispose();
    }
}