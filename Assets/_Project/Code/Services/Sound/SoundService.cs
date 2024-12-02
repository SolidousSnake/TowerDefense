using _Project.Code.Services.SaveLoad;
using _Project.Code.Utils;
using UnityEngine;
using UnityEngine.Audio;

namespace _Project.Code.Services.Sound
{
    public class SoundService
    {
        private readonly AudioMixerGroup _audioMixerGroup;
        private readonly ISaveLoadService _saveLoadService;

        public SoundService(AudioMixerGroup audioMixerGroup, ISaveLoadService saveLoadService)
        {
            _audioMixerGroup = audioMixerGroup;
            _saveLoadService = saveLoadService;
        }
        
        public void SetMusicVolume(float volume)
        {
            Debug.Log("A");
            if (volume == 0)
                volume = Constants.Audio.MinSliderValue;
            Debug.Log("B");

            _audioMixerGroup.audioMixer
                .SetFloat(Constants.Audio.Music, Mathf.Log10(volume) * Constants.Audio.MaxValue);
            Save();
        }

        public void SetSfxVolume(float volume)
        {
            if (volume == 0)
                volume = Constants.Audio.MinSliderValue;

            _audioMixerGroup.audioMixer
                .SetFloat(Constants.Audio.SFX, Mathf.Log10(volume) * Constants.Audio.MaxValue);
            Save();
        }

        private void Save()
        {
            var progress = _saveLoadService.Load();
            var musicValue = GetMusicValue();
            Debug.Log(musicValue);
            _audioMixerGroup.audioMixer.GetFloat(Constants.Audio.SFX, out float sfxValue);
            
            if (float.IsNaN(musicValue))
                musicValue =  Constants.Audio.MinSliderValue;
    
            if (float.IsNaN(sfxValue))
                sfxValue =  Constants.Audio.MinSliderValue;
            
            progress.SoundData.MusicVolume = musicValue;
            progress.SoundData.SfxVolume = sfxValue;

            _saveLoadService.Save(progress);
        }

        public float GetMusicValue()
        {
            _audioMixerGroup.audioMixer.GetFloat(Constants.Audio.Music, out var musicValue);
            return musicValue;
        }
    }
}