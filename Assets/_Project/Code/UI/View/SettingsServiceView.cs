using System.Collections.Generic;
using _Project.Code.Services.Settings;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Project.Code.UI.View
{
    public class SettingsServiceView : MonoBehaviour
    {
        [SerializeField] private Button _buttonPrefab;
        [SerializeField] private Transform _buttonParent;
        [SerializeField] private List<int> _fpsValues;
        [Inject] private SettingService _settingService;

        private readonly List<Button> _createdButtons = new();

        private void Awake()
        {
            CreateButtons();

            _settingService.TargetFrameRate.Subscribe(UpdateButtonStates).AddTo(this);
            UpdateButtonStates(_settingService.TargetFrameRate.Value);
        }

        private void CreateButtons()
        {
            foreach (var fpsValue in _fpsValues)
            {
                var button = Instantiate(_buttonPrefab, _buttonParent);
                button.GetComponentInChildren<TextMeshProUGUI>().text = $"{fpsValue}";
                _createdButtons.Add(button);

                button.OnClickAsObservable()
                    .Subscribe(_ => _settingService.TargetFrameRate.Value = fpsValue).AddTo(this);
            }
        }

        private void UpdateButtonStates(int frameRate)
        {
            for (int i = 0; i < _createdButtons.Count; i++)
                _createdButtons[i].interactable = _fpsValues[i] != frameRate;
        }
    }
}