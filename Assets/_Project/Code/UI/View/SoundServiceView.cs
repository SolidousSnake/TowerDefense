using _Project.Code.Services.SaveLoad;
using _Project.Code.Services.Sound;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Project.Code.UI.View
{
    public class SoundServiceView : MonoBehaviour
    {
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _sfxSlider;

        [Inject] private SoundService _soundService;
        [Inject] private ISaveLoadService _saveLoadService;
        
        private void Start()
        {
            LoadSoundData();
            _musicSlider.onValueChanged.AddListener(SetMusicVolume);
            _sfxSlider.onValueChanged.AddListener(SetSfxVolume);
        }
        
        private void SetMusicVolume(float value)
        {
            _soundService.SetMusicVolume(value);
            SaveSoundData();
        }

        private void SetSfxVolume(float value)
        {
            _soundService.SetSfxVolume(value);
            SaveSoundData();
        }
        
        
        private void SaveSoundData()
        {
            var progress = _saveLoadService.Load();
            progress.SoundData.MusicVolume = _musicSlider.value;
            progress.SoundData.SfxVolume = _sfxSlider.value;
            
            _saveLoadService.Save(progress);
        }

        private void LoadSoundData()
        {
            var progress = _saveLoadService.Load();
            
            _musicSlider.value = progress.SoundData.MusicVolume;
            _sfxSlider.value = progress.SoundData.SfxVolume;

            _soundService.SetMusicVolume(progress.SoundData.MusicVolume);
            _soundService.SetSfxVolume(progress.SoundData.SfxVolume);
        }
        
        private void OnDestroy()
        {
            _musicSlider.onValueChanged.RemoveAllListeners();
            _sfxSlider.onValueChanged.RemoveAllListeners();
        }
    }
}