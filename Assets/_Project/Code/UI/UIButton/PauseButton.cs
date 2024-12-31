using _Project.Code.Core.Fsm;
using _Project.Code.Gameplay.States;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Project.Code.UI.UIButton
{
    [RequireComponent(typeof(Button))]
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [Inject] private GameplayStateMachine _fsm;
        
        private void Start()
        {
            _button ??= GetComponent<Button>();
            _button.OnClickAsObservable().Subscribe(_ => _fsm.Enter<PauseState>()).AddTo(this);
        }
    }
}