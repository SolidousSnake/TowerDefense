using _Project.Code.Services.Sound;
using _Project.Code.Utils;
using UniRx;
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

        private void Start()
        {
            var musicValue = _soundService.GetMusicValue();

            // Преобразуем логарифмическое значение обратно в линейное для слайдера
            float linearMusicValue = Mathf.Pow(10, musicValue / Constants.Audio.MaxValue);

            // Ограничиваем значение слайдера, чтобы оно всегда было в пределах от 0 до 1
            _musicSlider.value = Mathf.Clamp(linearMusicValue, Constants.Audio.MinSliderValue, 1f);

            
            _musicSlider.OnValueChangedAsObservable().Skip(1).Subscribe(SetMusicVolume).AddTo(this);
            _sfxSlider.OnValueChangedAsObservable().Skip(1).Subscribe(SetSfxVolume).AddTo(this);
        }

        private void SetMusicVolume(float value) => _soundService.SetMusicVolume(value);
        private void SetSfxVolume(float value) => _soundService.SetSfxVolume(value);
    }
}