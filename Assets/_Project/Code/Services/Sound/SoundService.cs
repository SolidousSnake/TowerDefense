using _Project.Code.Utils;
using UnityEngine;
using UnityEngine.Audio;
using VContainer;

namespace _Project.Code.Services.Sound
{
    public class SoundService
    {
        [Inject] private readonly AudioMixerGroup _audioMixerGroup;

        public void SetMusicVolume(float volume) =>
            _audioMixerGroup.audioMixer.SetFloat(Constants.Audio.Music, Mathf.Log10(volume) * Constants.Audio.MaxValue);

        public void SetSfxVolume(float volume) =>
            _audioMixerGroup.audioMixer.SetFloat(Constants.Audio.SFX, Mathf.Log10(volume) * Constants.Audio.MaxValue);
    }
}