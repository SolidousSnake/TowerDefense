using System;
using _Project.Code.Core.Fsm;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Project.Code.UI.UIButton
{
    public class GameSwitchStateButton : MonoBehaviour
    {
        private enum TargetStates
        {
            None = 0,
            Lobby = 1,
            SelectLevel = 2,
            Shop = 3,
        }

        [SerializeField] private TargetStates _targetState = 0;
        [SerializeField] private Button _button;

        [Inject] private LobbyStateMachine _fsm;

        private void OnEnable() => _button.onClick.AddListener(OnClick);
        private void OnDisable() => _button.onClick.RemoveListener(OnClick);

        private void OnClick()
        {
            switch (_targetState)
            {
                case TargetStates.Lobby:
                    break;
                case TargetStates.SelectLevel:
                    break;
                case TargetStates.Shop:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}