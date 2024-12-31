using System;
using _Project.Code.Core.Fsm;
using _Project.Code.Lobby.States;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Project.Code.UI.UIButton
{
    [RequireComponent(typeof(Button))]
    public class GameStateSwitchButton : MonoBehaviour
    {
        private enum TargetStates
        {
            None = 0,
            Lobby = 1,
            SelectLevel = 2,
            Shop = 3,
            Settings = 4,
            Quit = 5
        }

        [SerializeField] private TargetStates _targetState = 0;
        [SerializeField] private Button _button;

        [Inject] private LobbyStateMachine _fsm;

        private void OnValidate() => _button ??= GetComponent<Button>();
        private void OnEnable() => _button.onClick.AddListener(OnClick);
        private void OnDisable() => _button.onClick.RemoveListener(OnClick);

        private void OnClick()
        {
            switch (_targetState)
            {
                case TargetStates.Lobby: _fsm.Enter<HubState>(); break;
                case TargetStates.SelectLevel: _fsm.Enter<SelectLevelState>(); break;
                case TargetStates.Shop: _fsm.Enter<ShopState>(); break;
                case TargetStates.Settings: _fsm.Enter<SettingState>(); break;
                case TargetStates.Quit: Application.Quit(); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}