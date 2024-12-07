using _Project.Code.Services.Sound;
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

        public void Start()
        {
            _musicSlider.value = _soundService.MusicVolume.Value;
            _sfxSlider.value = _soundService.SfxVolume.Value;

            _musicSlider.OnValueChangedAsObservable().Subscribe(_soundService.SetMusicVolume).AddTo(this);
            _sfxSlider.OnValueChangedAsObservable().Subscribe(_soundService.SetSfxVolume).AddTo(this);

            _soundService.MusicVolume.Subscribe(value => _musicSlider.SetValueWithoutNotify(value)).AddTo(this);
            _soundService.SfxVolume.Subscribe(value => _sfxSlider.SetValueWithoutNotify(value)).AddTo(this);
        }
    }
}