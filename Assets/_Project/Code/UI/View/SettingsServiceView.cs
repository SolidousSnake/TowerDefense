using _Project.Code.Services.Settings;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Project.Code.UI.View
{
    public class SettingsServiceView : MonoBehaviour
    {
        [SerializeField] private Button _fps30Button;
        [SerializeField] private Button _fps60Button;    
        [Inject] private SettingService _settingService;

        private void Awake()
        {
            _fps30Button.OnClickAsObservable()
                .Subscribe(_ => _settingService.TargetFrameRate.Value = 30).AddTo(this);

            _fps60Button.OnClickAsObservable()
                .Subscribe(_ => _settingService.TargetFrameRate.Value = 60).AddTo(this);

            _settingService.TargetFrameRate.Subscribe(UpdateButtonStates).AddTo(this);
        }

        private void UpdateButtonStates(int frameRate)
        {
            _fps30Button.interactable = frameRate != 30;
            _fps60Button.interactable = frameRate != 60;
        }
    }
}